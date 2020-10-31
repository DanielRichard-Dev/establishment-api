using establishment_models.Establishment;
using System;
using System.Collections.Generic;
using System.Text;

namespace establishment_repository.Interface
{
    public interface IEstablishmentAddressRepository
    {
        int InsertEstablishmentAddress(EstablishmentAddressModel request);

        EstablishmentAddressModel SelectEstablishment(int establishmentId);

        bool UpdateEstablishmentAddress(EstablishmentAddressModel request);

        bool DeleteEstablishmentAddress(int establishmentAddressId);
    }
}
