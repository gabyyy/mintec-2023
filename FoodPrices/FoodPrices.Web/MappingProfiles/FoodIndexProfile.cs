using AutoMapper;
using FoodPrices.Services.Models;
using FoodPrices.Web.Models;

namespace FoodPrices.Web.MappingProfiles
{
    public class FoodIndexProfile : Profile
    {
        public FoodIndexProfile()
        {
            CreateMap<FoodIndex, FoodIndexDto>();
            CreateMap<Quote, QuoteDto>();
        }
    }
}
