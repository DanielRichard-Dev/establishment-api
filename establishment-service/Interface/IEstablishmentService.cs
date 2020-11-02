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

        List<EstablishmentModel> GetEstablishmentByCompanyName(string companyName);

        EstablishmentModel GetEstablishmentAddressAndAccount(EstablishmentModel establishment);

        List<EstablishmentModel> GetEstablishmentAddressAndAccountList(List<EstablishmentModel> _establishment);

        bool SaveEstablishment(EstablishmentModel establishment);

        bool RemoveEstablishment(int establishmentId);

        int InsertEstablishment(EstablishmentModel establishment);

        int InsertEstablishmentAddress(EstablishmentAddressModel establishmentAddress, int establishmentId);

        int InsertEstablishmentAccount(EstablishmentAccountModel establishmentAccount, int establishmentId);

        List<EstablishmentModel> SelectEstablishmentByCategory(int categoryId);

        EstablishmentModel SelectEstablishmentByCNPJ(string cnpj);

        List<EstablishmentModel> SelectEstablishmentByCompanyName(string companyName);

        EstablishmentAddressModel SelectEstablishmentAddres(int establishmentId);

        EstablishmentAccountModel SelectEstablishmentAccount(int establishmentId);

        bool UpdateEstablishment(EstablishmentModel establishment);

        bool UpdateEstablishmentAddress(EstablishmentAddressModel establishmentAddress, int establishmentId);

        bool UpdateEstablishAccount(EstablishmentAccountModel establishmentAccount, int establishmentId);

        bool DeleteEstablishment(int establishmentId);

        bool DeleteEstablishmentAddress(int establishmentAddressId);

        bool DeleteEstablishmentAccount(int establishmentAccountId);

        int CheckEstablishment(string cnpj);
    }
}
