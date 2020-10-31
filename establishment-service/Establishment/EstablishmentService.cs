using establishment_models.Establishment;
using establishment_repository.Establishment;
using establishment_service.Validate;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace establishment_service.Establishment
{
    public class EstablishmentService
    {
        public EstablishmentRepository _establishmentRepository { get; set; }
        public EstablishmentAddressRepository _establishmentAddressRepository { get; set; }
        public EstablishmentAccountRepository _establishmentAccountRepository { get; set; }
        public ValidateService _validateService { get; set; }

        public IConfiguration _configuration { get; set; }

        public EstablishmentService(IConfiguration _configuration)
        {
            this._configuration = _configuration;

            _establishmentRepository = new EstablishmentRepository(_configuration);
            _establishmentAddressRepository = new EstablishmentAddressRepository(_configuration);
            _establishmentAccountRepository = new EstablishmentAccountRepository(_configuration);
            _validateService = new ValidateService();
        }

        public int CreateEstablishment(EstablishmentModel establishment)
        {
            establishment.EstablishmentId = CheckEstablishment(establishment.CNPJ);

            if (establishment.EstablishmentId == 0)
            {
                establishment.EstablishmentId = InsertEstablishment(establishment);
                establishment.Address.EstablishmentAddressId = InsertEstablishmentAddress(establishment.Address, establishment.EstablishmentId);
                establishment.Account.EstablishmentAccountId = InsertEstablishmentAccount(establishment.Account, establishment.EstablishmentId);
            }
            else
                throw new Exception("Estabelecimento já cadastrado!");

            return establishment.EstablishmentId;
        }

        public EstablishmentModel GetEstablishmentByCNPJ(string cnpj)
        {
            var establishment = SelectEstablishmentByCNPJ(cnpj);

            return establishment;
        }

        public List<EstablishmentModel> GetEstablishmentByCategory(int category)
        {
            var establishment = SelectEstablishmentByCategory(category);

            return establishment;
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

        public bool RemoveEstablishment(EstablishmentModel establishment)
        {
            DeleteEstablishment(establishment.EstablishmentId);
            DeleteEstablishmentAddress(establishment.Address.EstablishmentAddressId);
            DeleteEstablishmentAccount(establishment.Account.EstablishmentAccountId);

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

        public List<EstablishmentModel> SelectEstablishmentByCategory(int category)
        {
            var _establishment = _establishmentRepository.SelectEstablishmentByCategory(category);

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

        public bool DeleteEstablishmentAddress(int establishmentAddressId)
        {
            if (establishmentAddressId != 0)
                _establishmentAddressRepository.DeleteEstablishmentAddress(establishmentAddressId);

            return true;
        }

        public bool DeleteEstablishmentAccount(int establishmentAccountId)
        {
            if (establishmentAccountId != 0)
                _establishmentAccountRepository.DeleteEstablishmentAccount(establishmentAccountId);

            return true;
        }

        public int CheckEstablishment(string cnpj)
        {
            var establishmentId = _establishmentRepository.EstablishmentExist(cnpj);

            return establishmentId;
        }
    }
}
