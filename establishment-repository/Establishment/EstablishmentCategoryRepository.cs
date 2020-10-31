using establishment_models.Establishment;
using establishment_repository.Interface;
using establishment_repository.Master;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace establishment_repository.Establishment
{
    public class EstablishmentCategoryRepository : MasterRepository, IEstablishmentCategoryRepository
    {
        public EstablishmentCategoryRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public int InsertCategory(EstablishmentCategoryModel request)
        {
            #region .: Query :.
            var query = @"
                INSERT INTO [dbo].[EstablishmentCategory] 
                    ([CategoryDescription])
                        VALUES (@CategoryDescription)
                            SELECT CAST(SCOPE_IDENTITY() AS INT) [EstablishmentCategoryId]";
            #endregion
            var establishmentCategoryId = ExecuteGetObj<int>(query, ConnectionString, request);

            return establishmentCategoryId;
        }

        public List<EstablishmentCategoryModel>SelectEstablishmentCategory()
        {
            #region .: Query :.
            var query = @"
                SELECT *
                    FROM [dbo].[EstablishmentCategory]";
            #endregion
            var _establishmentCategory = ExecuteGetList<EstablishmentCategoryModel>(query, ConnectionString);

            return _establishmentCategory;
        }

        public bool UpdateEstablishmentCategory(EstablishmentCategoryModel request)
        {
            #region .: Query :.
            var query = @"
                UPDATE [dbo].[EstablishmentCategory]
                    SET [CategoryDescription] = @CategoryDescription
                        WHERE [EstablishmentCategoryId] = @EstablishmentCategoryId";
            #endregion
            ExecuteQuery(query, ConnectionString, request);

            return true;
        }

        public bool DeleteEstablishmentCategory(int establishmentCategoryId)
        {
            #region .: Query :.
            var query = @"
                DELETE [dbo].[EstablishmentCategory]
                    WHERE [EstablishmentCategoryId] = @EstablishmentCategoryId";
            #endregion
            ExecuteQuery(query, ConnectionString, new { establishmentCategoryId });

            return true;
        }
    }
}
