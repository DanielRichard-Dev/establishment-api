namespace establishment_models.Establishment
{
    public class EstablishmentAddressModel
    {
        public int EstablishmentAddressId { get; set; }

        public int EstablishmentId { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string State { get; set; }
    }
}
