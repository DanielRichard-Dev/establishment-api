using establishment_models.Establishment;
using establishment_repository.Interface;
using establishment_repository.Master;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace establishment_repository.Establishment
{
    public class EstablishmentAccountRepository : MasterRepository, IEstablishmentAccountRepository
    {
        public EstablishmentAccountRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public int InsertEstablishmentAccount(EstablishmentAccountModel request)
        {
            #region .: Query :.
            var query = @"
                INSERT INTO [dbo].[EstablishmentAccount]
                    ([EstablishmentId], [Agency], [Account])
                        VALUES (@EstablishmentId, @Agency, @Account)
                            SELECT CAST(SCOPE_IDENTITY() AS INT) [EstablishmentAccountId]";
            #endregion
            var establishmentAccountId = ExecuteGetObj<int>(query, ConnectionString, request);

            return establishmentAccountId;
        }

        public EstablishmentAccountModel SelectEstablishmentAccount(int establishmentId)
        {
            #region .: Query :.
            var query = @"
                SELECT *
                    FROM [dbo].[EstablishmentAccount]
                        WHERE [EstablishmentId] = @establishmentId";
            #endregion
            var establishmentAccount = ExecuteGetObj<EstablishmentAccountModel>(query, ConnectionString, new { establishmentId });

            return establishmentAccount;
        }

        public bool UpdateEstablishmentAccount(EstablishmentAccountModel request)
        {
            #region .: Query :.
            var query = @"
                UPDATE [dbo].[EstablishmentAccount]
                    SET [Agency] = @Agency, [Account] = @Account
                        WHERE [EstablishmentAccountId] = @EstablishmentAccountId";
            #endregion
            ExecuteQuery(query, ConnectionString, request);

            return true;
        }

        public bool DeleteEstablishmentAccount(int establishmentId)
        {
            #region .: Query :.
            var query = @"
                DELETE [dbo].[EstablishmentAccount]
                    WHERE [EstablishmentId] = @establishmentId";
            #endregion
            ExecuteQuery(query, ConnectionString, new { establishmentId });

            return true;
        }
    }
}
