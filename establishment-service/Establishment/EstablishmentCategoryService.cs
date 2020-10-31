using establishment_models.Establishment;
using establishment_repository.Interface;
using establishment_service.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace establishment_service.Establishment
{
    public class EstablishmentCategoryService : IEstablishmentCategoryService
    {
        public IEstablishmentCategoryRepository _establishmentCategory { get; set; }

        public EstablishmentCategoryService(IEstablishmentCategoryRepository _establishmentCategory)
        {
            this._establishmentCategory = _establishmentCategory;
        }

        public int CreateEstablishmentCategory(EstablishmentCategoryModel establishmentCategory)
        {
            establishmentCategory.EstablishmentCategoryId = InsertEstablishmentCategory(establishmentCategory);

            return establishmentCategory.EstablishmentCategoryId;
        }

        public List<EstablishmentCategoryModel> GetEstablishmentCategoryModels()
        {
            var _establishmentCategory = SelectEstablishmentCategory();

            return _establishmentCategory;
        }

        public bool SaveEstablishmentCategory(EstablishmentCategoryModel establishmentCategory)
        {
            UpdateEstablishmentCategory(establishmentCategory);

            return true;
        }

        public bool RemoveEstablishmentCategory(EstablishmentCategoryModel establishmentCategory)
        {
            DeleteEstablishmentCategory(establishmentCategory.EstablishmentCategoryId);

            return true;
        }

        public int InsertEstablishmentCategory(EstablishmentCategoryModel establishmentCategory)
        {
            if (establishmentCategory.CategoryDescription == null)
                throw new Exception("Erro ao processar, categoria vazia!");

            var establishmentCategoryId = _establishmentCategory.InsertCategory(establishmentCategory);

            return establishmentCategoryId;
        }

        public List<EstablishmentCategoryModel> SelectEstablishmentCategory()
        {
            var establishmentCategory = _establishmentCategory.SelectEstablishmentCategory();

            if (establishmentCategory == null)
                return establishmentCategory = new List<EstablishmentCategoryModel>();


            return establishmentCategory;
        }

        public bool UpdateEstablishmentCategory(EstablishmentCategoryModel establishmentCategory)
        {
            if (establishmentCategory.CategoryDescription == null)
                throw new Exception("Erro ao processar, categoria vazia!");

            _establishmentCategory.UpdateEstablishmentCategory(establishmentCategory);

            return true;
        }

        public bool DeleteEstablishmentCategory(int establishmentCategoryId)
        {
            if (establishmentCategoryId != 0)
                _establishmentCategory.DeleteEstablishmentCategory(establishmentCategoryId);

            return true;
        }
    }
}
