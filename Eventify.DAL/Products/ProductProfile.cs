using AutoMapper;
using Eventify.Common.Classes.Products;
using Eventify.Domain;

namespace Eventify.DAL.Products
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<DbProduct, Product>()
                .ReverseMap();
        }
    }
}
