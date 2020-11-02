using establishment_models.Establishment;
using establishment_repository.Interface;
using establishment_repository.Master;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace establishment_repository.Establishment
{
    public class EstablishmentRepository : MasterRepository, IEstablishmentRepository
    {
        public EstablishmentRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public int InsertEstablishment(EstablishmentModel request)
        {
            #region .: Query :.
            var query = @"
                INSERT INTO [dbo].[Establishment]
                    ([CompanyName], [FantasyName], [CNPJ], [Email], [Telephone], [DateOfRegistration], [Status], [CategoryId])
                        VALUES (@CompanyName, @FantasyName, @CNPJ, @Email, @Telephone, @DateOfRegistration, @Status, @CategoryId)
                SELECT CAST(SCOPE_IDENTITY() AS INT) [EstablishmentId]";
            #endregion
            var establishmentId = ExecuteGetObj<int>(query, ConnectionString, request);

            return establishmentId;
        }

        public EstablishmentModel SelectEstablishmentByCNPJ(string cnpj)
        {
            #region .: Query :.
            var query = @"
                SELECT *
                    FROM [dbo].[Establishment]
                        WHERE [CNPJ] = @cnpj";
            #endregion
            var establishment = ExecuteGetObj<EstablishmentModel>(query, ConnectionString, new { cnpj });

            return establishment;
        }

        public List<EstablishmentModel> SelectEstablishmentByCategory(int categoryId)
        {
            #region .: Query :.
            var query = @"
                SELECT *
                    FROM [dbo].[Establishment]
                        WHERE [CategoryId] = @categoryId";
            #endregion
            var _establishment = ExecuteGetList<EstablishmentModel>(query, ConnectionString, new { categoryId });

            return _establishment;
        }

        public List<EstablishmentModel> SelectEstablishmentByCompanyName(string companyName)
        {
            #region .: Query :.
            var query = $@"
                SELECT *
                    FROM [dbo].[Establishment]
                        WHERE [CompanyName] LIKE ('%{companyName}%')";
            #endregion
            var _establishment = ExecuteGetList<EstablishmentModel>(query, ConnectionString, new { companyName });

            return _establishment;
        }

        public bool UpdateEstablishment(EstablishmentModel request)
        {
            #region .: Query :.
            var query = @"
                UPDATE [dbo].[Establishment]
                    SET [CompanyName] = @CompanyName, [FantasyName] = @FantasyName, [CNPJ] = @CNPJ, [Email] = @Email,
                            [Telephone] = @Telephone, [DateOfRegistration] = @DateOfRegistration, [Status] = @Status, [CategoryId] = @CategoryId
                                WHERE [EstablishmentId] = @EstablishmentId";
            #endregion
            ExecuteQuery(query, ConnectionString, request);

            return true;
        }

        public bool DeleteEstablishment(int establishmentId)
        {
            #region .: Query :.
            var query = @"
                DELETE [dbo].[Establishment]
                    WHERE [EstablishmentId] = @establishmentId";
            #endregion
            ExecuteQuery(query, ConnectionString, new { establishmentId });

            return true;
        }

        public int EstablishmentExist(string cnpj)
        {
            #region .: Query :.
            var query = @"
                SELECT [EstablishmentId]
                    FROM [dbo].[Establishment]
                        WHERE [CNPJ] = @cnpj";
            #endregion
            var establishmentId = ExecuteGetObj<int>(query, ConnectionString, new { cnpj });

            return establishmentId;
        }
    }
}
