namespace CoffeeShop.DataAccess.DataAccess.ModelDB.Order
{
    public class Address
    {
        public Guid Id { get; set; }
        public string Streed { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
    }
}
