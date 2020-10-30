using establishment_models.Enum;
using System;

namespace establishment_models.Establishment
{
    public class EstablishmentModel
    {
        public string EstablishmentId { get; set; }

        public string CompanyName { get; set; }

        public string FantasyName { get; set; }

        public string CNPJ { get; set; }

        public string Email { get; set; }

        public string Telephone { get; set; }

        public DateTime DateOfRegistration { get; set; }

        public bool Status { get; set; }

        public EstablishmentAddressModel Adress { get; set; }

        public EstablishmentCategoryEnum Category { get; set; }

        public EstablishmentAccountModel Account { get; set; }
    }
}
