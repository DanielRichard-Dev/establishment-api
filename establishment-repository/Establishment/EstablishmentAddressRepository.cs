﻿using establishment_models.Establishment;
using establishment_repository.Master;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace establishment_repository.Establishment
{
    public class EstablishmentAddressRepository : MasterRepository
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
                    FROM [dbo].[EstablishmentAddres]
                        WHERE [EstablishmentId] = @establishmentId";
            #endregion
            var establishmentAddress = ExecuteGetObj<EstablishmentAddressModel>(query, ConnectionString, new { establishmentId });

            return establishmentAddress;
        }
    }
}