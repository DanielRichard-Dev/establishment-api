using establishment_models.Establishment;
using establishment_repository.Master;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace establishment_repository.Establishment
{
    public class EstablishmentAccountRepository : MasterRepository
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
    }
}
