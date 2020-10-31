using establishment_models.Establishment;
using System;
using System.Collections.Generic;
using System.Text;

namespace establishment_repository.Interface
{
    public interface IEstablishmentCategoryRepository
    {
        int InsertCategory(EstablishmentCategoryModel request);

        List<EstablishmentCategoryModel> SelectEstablishmentCategory();

        bool UpdateEstablishmentCategory(EstablishmentCategoryModel request);

        bool DeleteEstablishmentCategory(int establishmentCategoryId);
    }
}
