using AutoMapper;
using DigitalMenu.Application.Model.Products;
using DigitalMenu.Domain.Products;

namespace DigitalMenu.Application.Core
{
    public class AutoMapperModule : Profile
    {
        public AutoMapperModule()
        {
            CreateMap<Product, ProductDto>().ReverseMap();
        }
    }
}
