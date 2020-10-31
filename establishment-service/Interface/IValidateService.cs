using System;
using System.Collections.Generic;
using System.Text;

namespace establishment_service.Interface
{
    public interface IValidateService
    {
        bool ValidCNPJ(string cnpj);

        bool ValidEmail(string email);
    }
}
