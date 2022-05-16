using Eventify.Common.Classes.AutoMapper;
using Eventify.Common.Classes.Products;
using Eventify.Domain;

namespace Eventify.DAL.Products
{
    internal static class ProductExtensions
    {
        public static Product ToProduct(this DbProduct dbProduct)
        {
            return MapperWrapper.Mapper.Map<Product>(dbProduct);
        }
    }
}
