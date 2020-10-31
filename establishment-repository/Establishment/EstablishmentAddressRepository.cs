using establishment_models.Establishment;
using establishment_repository.Interface;
using establishment_repository.Master;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace establishment_repository.Establishment
{
    public class EstablishmentAddressRepository : MasterRepository, IEstablishmentAddressRepository
    {
        public EstablishmentAddressRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public int InsertEstablishmentAddress(EstablishmentAddressModel request)
        {
            #region .: Query :.
            var query = @"
                INSERT INTO [dbo].[EstablishmentAddress]
                    ([EstablishmentId], [Address], [City], [State])
                        VALUES (@EstablishmentId, @Address, @City, @State)
                            SELECT CAST(SCOPE_IDENTITY() AS INT) [EstablishmentAddressId]";
            #endregion
            var establishmentAddressId = ExecuteGetObj<int>(query, ConnectionString, request);

            return establishmentAddressId;
        }

        public EstablishmentAddressModel SelectEstablishment(int establishmentId)
        {
            #region .: Query :.
            var query = @"
                SELECT *
                    FROM [dbo].[EstablishmentAddress]
                        WHERE [EstablishmentId] = @establishmentId";
            #endregion
            var establishmentAddress = ExecuteGetObj<EstablishmentAddressModel>(query, ConnectionString, new { establishmentId });

            return establishmentAddress;
        }

        public bool UpdateEstablishmentAddress(EstablishmentAddressModel request)
        {
            #region .: Query :.
            var query = @"
                UPDATE [dbo].[EstablishmentAddress]
                    SET [Address] = @Address, [City] = @City, [State] = @State
                        WHERE [EstablishmentAddressId] = @EstablishmentAddressId";
            #endregion
            ExecuteQuery(query, ConnectionString, request);

            return true;
        }

        public bool DeleteEstablishmentAddress(int establishmentAddressId)
        {
            #region .: Query :.
            var query = @"
                DELETE [dbo].[EstablishmentAddress]
                    WHERE [EstablishmentAddressId] = @establishmentAddressId";
            #endregion
            ExecuteQuery(query, ConnectionString, new { establishmentAddressId });

            return true;
        }
    }
}
