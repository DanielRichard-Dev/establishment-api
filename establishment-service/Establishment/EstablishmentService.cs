using establishment_models.Establishment;
using establishment_repository.Establishment;
using establishment_service.Validate;
using Microsoft.Extensions.Configuration;
using System;

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
                establishment.Address.EstablishmentAdressId = InsertEstablishmentAddress(establishment.Address, establishment.EstablishmentId);
                establishment.Account.EstablishmentAccountId = InsertEstablishmentAccount(establishment.Account, establishment.EstablishmentId);
            }
            else
                throw new Exception("Estabelecimento já cadastrado!");

            return establishment.EstablishmentId;
        }

        public EstablishmentModel GetEstablishment(string cnpj)
        {
            var establishment = SelectEstablishmentByCNPJ(cnpj);

            if (establishment != null)
            {
                establishment.Address = SelectEstablishmentAddres(establishment.EstablishmentId);
                establishment.Account = SelectEstablishmentAccount(establishment.EstablishmentId);

                return establishment;
            }

            throw new Exception("Estabelecimento não encontrado!");
        }

        public int InsertEstablishment(EstablishmentModel establishment)
        {
            int establishmentId = 0;

            if (establishment.CNPJ != null || establishment.CompanyName != null)
            {
                _validateService.ValidCNPJ(establishment.CNPJ);
                _validateService.ValidEmail(establishment.Email);

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

        public EstablishmentModel SelectEstablishmentByCNPJ(string cnpj)
        {
            var establishment = _establishmentRepository.SelectEstablishmentByCNPJ(cnpj);

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

        public int CheckEstablishment(string cnpj)
        {
            var establishmentId = _establishmentRepository.EstablishmentExist(cnpj);

            return establishmentId;
        }
    }
}
