using establishment_models.Establishment;
using System;
using System.Collections.Generic;
using System.Text;

namespace establishment_repository.Interface
{
    public interface IEstablishmentAccountRepository
    {
        int InsertEstablishmentAccount(EstablishmentAccountModel request);

        EstablishmentAccountModel SelectEstablishmentAccount(int establishmentId);

        bool UpdateEstablishmentAccount(EstablishmentAccountModel request);

        bool DeleteEstablishmentAccount(int establishmentAccountId);
    }
}
