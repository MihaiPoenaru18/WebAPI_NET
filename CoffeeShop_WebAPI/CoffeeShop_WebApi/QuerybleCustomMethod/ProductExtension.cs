using CoffeeShop.ServicesLogic.EntiteModels.ModelsForProducts;
using System.Reflection;

namespace CoffeeShop_WebApi.QuerybleCustomMethod
{
    public static class ProductExtension
    {
        public static IEnumerable<ProductDto> OrderByProperty(this IEnumerable<ProductDto> items, string sortBy, string sortOrder)
        {
            PropertyInfo propertyInfo = typeof(ProductDto).GetProperty(sortBy);

            if (propertyInfo != null)
            {
                if (sortOrder == "desc")
                {
                    return items.OrderByDescending(s => propertyInfo.GetValue(s, null));
                }
                else
                {
                    return items.OrderBy(s => propertyInfo.GetValue(s, null));
                }
            }

            return items;
        }

        public static IEnumerable<ProductDto> SerachBy(this IEnumerable<ProductDto> items, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return items;
            }

            searchTerm = searchTerm.ToLower();

            return items.Where(product =>
                typeof(ProductDto)
                    .GetProperties()
                    .Any(property =>
                        property.GetValue(product)?.ToString()?.ToLower().Contains(searchTerm) == true || 
                        (property.PropertyType == typeof(int) && property.GetValue(product)?.ToString() == searchTerm)
                    )
            );


        }
    }
}
