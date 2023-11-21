namespace CoffeeShop.DataAccess.DataAccess.ModelDB.ProductModel
{
    public class Category
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public virtual List<Product> Products { get; set; }
    }
}
