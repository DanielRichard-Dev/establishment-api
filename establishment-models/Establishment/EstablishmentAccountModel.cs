using System;
using System.Collections.Generic;
using System.Text;

namespace establishment_models.Establishment
{
    public class EstablishmentAccountModel
    {
        public int EstablishmentAccountId { get; set; }

        public int EstablishmentId { get; set; }

        public string Agency { get; set; }

        public string Account { get; set; }
    }
}
