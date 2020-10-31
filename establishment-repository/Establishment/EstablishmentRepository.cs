﻿using establishment_models.Establishment;
using establishment_repository.Master;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace establishment_repository.Establishment
{
    public class EstablishmentRepository : MasterRepository
    {
        public EstablishmentRepository(IConfiguration configuration) : base(configuration)
        {
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

        public List<EstablishmentModel> SelectEstablishmentByCategory(int category)
        {
            #region .: Query :.
            var query = @"
                SELECT *
                    FROM [dbo].[Establishment]
                        WHERE [Category] = @category";
            #endregion
            var establishment = ExecuteGetList<EstablishmentModel>(query, ConnectionString, new { category });

            return establishment;
        }

        public bool UpdateEstablishment(EstablishmentModel request)
        {
            #region .: Query :.
            var query = @"
                UPDATE [dbo].[Establishment]
                    SET [CompanyName] = @CompanyName, [FantasyName] = @FantasyName, [CNPJ] = @CNPJ, [Email] = @Email,
                            [Telephone] = @Telephone, [DateOfRegistration] = @DateOfRegistration, [Status] = @Status, [Category] = @Category
                                WHERE [EstablishmentId] = @EstablishmentId";
            #endregion
            ExecuteQuery(query, ConnectionString, request);

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
