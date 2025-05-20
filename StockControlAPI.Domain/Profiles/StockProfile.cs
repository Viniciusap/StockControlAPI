using AutoMapper;
using StockControlAPI.Domain.Dtos;
using StockControlAPI.Domain.Model;

namespace StockControlAPI.Domain.Profiles
{
    public class StockProfile : Profile
    {
        public StockProfile()
        {
            CreateMap<StockDto, Stock>()
                .ForMember(dest => dest.Product, opt => opt.MapFrom(src => new Product { Id = src.ProductId }));
        }
    }
}