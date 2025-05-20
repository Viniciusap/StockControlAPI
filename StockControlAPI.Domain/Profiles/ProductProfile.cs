using AutoMapper;
using StockControlAPI.Domain.Dtos;
using StockControlAPI.Domain.Model;

namespace StockControlAPI.Domain.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<ProductDto, Product>();
        }
    }
}