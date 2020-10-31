using establishment_models.Establishment;
using establishment_repository.Master;
using Microsoft.Extensions.Configuration;

namespace establishment_repository.Establishment
{
    public class EstablishmentRepository : MasterRepository
    {
        public EstablishmentRepository(IConfiguration configuration) : base(configuration)
        {
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

        public int InsertEstablishment(EstablishmentModel request)
        {
            #region .: Query :.
            var query = @"
                INSERT INTO [dbo].[Establishment]
                    ([CompanyName], [FantasyName], [CNPJ], [Email], [Telephone], [DateOfRegistration], [Status], [Category])
                        VALUES (@CompanyName, @FantasyName, @CNPJ, @Email, @Telephone, @DateOfRegistration, @Status, @Category)
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

        public EstablishmentModel SelectEstablishmentByCategory(string category)
        {
            #region .: Query :.
            var query = @"
                SELECT *
                    FROM [dbo].[Establishment]
                        WHERE [Category] = @category";
            #endregion
            var establishment = ExecuteGetObj<EstablishmentModel>(query, ConnectionString, new { category });

            return establishment;
        }
    }
}
