using establishment_models.Establishment;
using establishment_repository.Establishment;
using establishment_repository.Interface;
using establishment_service.Interface;
using establishment_service.Validate;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace establishment_service.Establishment
{
    public class EstablishmentService : IEstablishmentService
    {
        public IEstablishmentRepository _establishmentRepository { get; set; }
        public IEstablishmentAddressRepository _establishmentAddressRepository { get; set; }
        public IEstablishmentAccountRepository _establishmentAccountRepository { get; set; }
        public IValidateService _validateService { get; set; }

        public EstablishmentService(IEstablishmentRepository _establishmentRepository, IEstablishmentAddressRepository _establishmentAddressRepository,
            IEstablishmentAccountRepository _establishmentAccountRepository, IValidateService _validateService)
        {
            this._establishmentRepository = _establishmentRepository;
            this._establishmentAddressRepository = _establishmentAddressRepository;
            this._establishmentAccountRepository = _establishmentAccountRepository;
            this._validateService = _validateService;
        }

        public int CreateEstablishment(EstablishmentModel establishment)
        {
            establishment.EstablishmentId = CheckEstablishment(establishment.CNPJ);

            if (establishment.EstablishmentId == 0 && establishment.CompanyName != null && establishment.CNPJ != null)
            {
                establishment.EstablishmentId = InsertEstablishment(establishment);
                establishment.Address.EstablishmentAddressId = InsertEstablishmentAddress(establishment.Address, establishment.EstablishmentId);
                establishment.Account.EstablishmentAccountId = InsertEstablishmentAccount(establishment.Account, establishment.EstablishmentId);
            }
            else if (establishment.EstablishmentId == 0 && establishment.CompanyName == null && establishment.CNPJ == null)
                throw new ArgumentException("Nome da empresa ou CNPJ vazio!");
            else
                throw new ArgumentException("Estabelecimento já cadastrado!");

            return establishment.EstablishmentId;
        }

        public EstablishmentModel GetEstablishmentByCNPJ(string cnpj)
        {
            var establishment = SelectEstablishmentByCNPJ(cnpj);

            return establishment;
        }

        public List<EstablishmentModel> GetEstablishmentByCategory(int categoryId)
        {
            var _establishment = SelectEstablishmentByCategory(categoryId);

            return _establishment;
        }

        public List<EstablishmentModel> GetEstablishmentByCompanyName(string companyName)
        {
            var _establishment = SelectEstablishmentByCompanyName(companyName);

            return _establishment;
        }

        public EstablishmentModel GetEstablishmentAddressAndAccount(EstablishmentModel establishment)
        {
            establishment.Address = SelectEstablishmentAddres(establishment.EstablishmentId);
            establishment.Account = SelectEstablishmentAccount(establishment.EstablishmentId);

            return establishment;
        }

        public List<EstablishmentModel> GetEstablishmentAddressAndAccountList(List<EstablishmentModel> _establishment)
        {
            var _newEstabilishment = new List<EstablishmentModel>();

            foreach (var establishment in _establishment)
            {
                establishment.Address = SelectEstablishmentAddres(establishment.EstablishmentId);
                establishment.Account = SelectEstablishmentAccount(establishment.EstablishmentId);

                _newEstabilishment.Add(establishment);
            }

            return _newEstabilishment;
        }

        public bool SaveEstablishment(EstablishmentModel establishment)
        {
            UpdateEstablishment(establishment);
            UpdateEstablishmentAddress(establishment.Address);
            UpdateEstablishAccount(establishment.Account);

            return true;
        }

        public bool RemoveEstablishment(int establishmentId)
        {
            DeleteEstablishment(establishmentId);
            DeleteEstablishmentAddress(establishmentId);
            DeleteEstablishmentAccount(establishmentId);

            return true;
        }

        public int InsertEstablishment(EstablishmentModel establishment)
        {
            int establishmentId = 0;

            if (establishment.CNPJ != null || establishment.CompanyName != null)
            {
                _validateService.ValidCNPJ(establishment.CNPJ);
                _validateService.ValidEmail(establishment.Email);

                establishment.DateOfRegistration = DateTime.Now;

                establishmentId = _establishmentRepository.InsertEstablishment(establishment);
            }

            return establishmentId;
        }

        public int InsertEstablishmentAddress(EstablishmentAddressModel establishmentAddress, int establishmentId)
        {
            int establishmentAddressId = 0;

            if (establishmentAddress != null)
            {
                establishmentAddress.EstablishmentId = establishmentId;
                establishmentAddressId = _establishmentAddressRepository.InsertEstablishmentAddress(establishmentAddress);
            }


            return establishmentAddressId;
        }

        public int InsertEstablishmentAccount(EstablishmentAccountModel establishmentAccount, int establishmentId)
        {
            int establishmentAccountId = 0;

            if (establishmentAccount != null)
            {
                establishmentAccount.EstablishmentId = establishmentId;
                establishmentAccountId = _establishmentAccountRepository.InsertEstablishmentAccount(establishmentAccount);
            }


            return establishmentAccountId;
        }

        public List<EstablishmentModel> SelectEstablishmentByCategory(int categoryId)
        {
            var _establishment = _establishmentRepository.SelectEstablishmentByCategory(categoryId);

            if (_establishment.Count > 0)
                _establishment = GetEstablishmentAddressAndAccountList(_establishment);
            else
                throw new Exception("Não possui estabelecimentos nessa categoria!");

            return _establishment;
        }

        public EstablishmentModel SelectEstablishmentByCNPJ(string cnpj)
        {
            var establishment = _establishmentRepository.SelectEstablishmentByCNPJ(cnpj);

            if (establishment != null)
                establishment = GetEstablishmentAddressAndAccount(establishment);
            else
                throw new Exception("Estabelecimento não encontrado!");

            return establishment;
        }

        public List<EstablishmentModel> SelectEstablishmentByCompanyName(string companyName)
        {
            var _establishment = _establishmentRepository.SelectEstablishmentByCompanyName(companyName);

            if (_establishment.Count > 0)
                _establishment = GetEstablishmentAddressAndAccountList(_establishment);
            else
                throw new Exception("Não possui estabelecimentos com esse nome!");

            return _establishment;
        }

        public EstablishmentAddressModel SelectEstablishmentAddres(int establishmentId)
        {
            var establishmentAddress = _establishmentAddressRepository.SelectEstablishment(establishmentId);

            if (establishmentAddress == null)
                establishmentAddress = new EstablishmentAddressModel();

            return establishmentAddress;
        }

        public EstablishmentAccountModel SelectEstablishmentAccount(int establishmentId)
        {
            var establishmentAccount = _establishmentAccountRepository.SelectEstablishmentAccount(establishmentId);

            if (establishmentAccount == null)
                establishmentAccount = new EstablishmentAccountModel();

            return establishmentAccount;
        }

        public bool UpdateEstablishment(EstablishmentModel establishment)
        {
            return _establishmentRepository.UpdateEstablishment(establishment);
        }

        public bool UpdateEstablishmentAddress(EstablishmentAddressModel establishmentAddress)
        {
            if (establishmentAddress != null)
                _establishmentAddressRepository.UpdateEstablishmentAddress(establishmentAddress);

            return true;
        }

        public bool UpdateEstablishAccount(EstablishmentAccountModel establishmentAccount)
        {
            if (establishmentAccount != null)
                _establishmentAccountRepository.UpdateEstablishmentAccount(establishmentAccount);

            return true;
        }

        public bool DeleteEstablishment(int establishmentId)
        {
            return _establishmentRepository.DeleteEstablishment(establishmentId);
        }

        public bool DeleteEstablishmentAddress(int establishmentId)
        {
            if (establishmentId != 0)
                _establishmentAddressRepository.DeleteEstablishmentAddress(establishmentId);

            return true;
        }

        public bool DeleteEstablishmentAccount(int establishmentId)
        {
            if (establishmentId != 0)
                _establishmentAccountRepository.DeleteEstablishmentAccount(establishmentId);

            return true;
        }

        public int CheckEstablishment(string cnpj)
        {
            var establishmentId = _establishmentRepository.EstablishmentExist(cnpj);

            return establishmentId;
        }
    }
}
