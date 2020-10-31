using establishment_models.Establishment;
using System.Collections.Generic;

namespace establishment_service.Interface
{
    public interface IEstablishmentCategoryService
    {
        int CreateEstablishmentCategory(EstablishmentCategoryModel establishmentCategory);

        List<EstablishmentCategoryModel> GetEstablishmentCategoryModels();

        bool SaveEstablishmentCategory(EstablishmentCategoryModel establishmentCategory);

        bool RemoveEstablishmentCategory(EstablishmentCategoryModel establishmentCategory);

        int InsertEstablishmentCategory(EstablishmentCategoryModel establishmentCategory);

        List<EstablishmentCategoryModel> SelectEstablishmentCategory();

        bool UpdateEstablishmentCategory(EstablishmentCategoryModel establishmentCategory);

        bool DeleteEstablishmentCategory(int establishmentCategoryId);
    }
}
