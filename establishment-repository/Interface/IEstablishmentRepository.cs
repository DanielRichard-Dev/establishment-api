using establishment_models.Establishment;
using System.Collections.Generic;

namespace establishment_repository.Interface
{
    public interface IEstablishmentRepository
    {
        int InsertEstablishment(EstablishmentModel request);

        EstablishmentModel SelectEstablishmentByCNPJ(string cnpj);

        List<EstablishmentModel> SelectEstablishmentByCategory(int category);

        bool UpdateEstablishment(EstablishmentModel request);

        bool DeleteEstablishment(int establishmentId);

        int EstablishmentExist(string cnpj);
    }
}
