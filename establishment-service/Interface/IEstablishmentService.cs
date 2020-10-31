using establishment_models.Establishment;
using System;
using System.Collections.Generic;
using System.Text;

namespace establishment_service.Interface
{
    public interface IEstablishmentService
    {
        int CreateEstablishment(EstablishmentModel establishment);

        EstablishmentModel GetEstablishmentByCNPJ(string cnpj);

        List<EstablishmentModel> GetEstablishmentByCategory(int category);

        EstablishmentModel GetEstablishmentAddressAndAccount(EstablishmentModel establishment);

        List<EstablishmentModel> GetEstablishmentAddressAndAccountList(List<EstablishmentModel> _establishment);

        bool SaveEstablishment(EstablishmentModel establishment);

        bool RemoveEstablishment(EstablishmentModel establishment);

        int InsertEstablishment(EstablishmentModel establishment);

        int InsertEstablishmentAddress(EstablishmentAddressModel establishmentAddress, int establishmentId);

        int InsertEstablishmentAccount(EstablishmentAccountModel establishmentAccount, int establishmentId);

        List<EstablishmentModel> SelectEstablishmentByCategory(int category);

        EstablishmentModel SelectEstablishmentByCNPJ(string cnpj);

        EstablishmentAddressModel SelectEstablishmentAddres(int establishmentId);

        EstablishmentAccountModel SelectEstablishmentAccount(int establishmentId);

        bool UpdateEstablishment(EstablishmentModel establishment);

        bool UpdateEstablishmentAddress(EstablishmentAddressModel establishmentAddress);

        bool UpdateEstablishAccount(EstablishmentAccountModel establishmentAccount);

        bool DeleteEstablishment(int establishmentId);

        bool DeleteEstablishmentAddress(int establishmentAddressId);

        bool DeleteEstablishmentAccount(int establishmentAccountId);

        int CheckEstablishment(string cnpj);
    }
}
