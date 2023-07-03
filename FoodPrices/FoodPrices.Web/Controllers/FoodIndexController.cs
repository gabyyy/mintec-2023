using AutoMapper;
using FoodPrices.Services.Services;
using FoodPrices.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace FoodPrices.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FoodIndexController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IFoodIndexService foodIndexService;

        public FoodIndexController(IMapper mapper, IFoodIndexService foodIndexService)
        {
            this.mapper = mapper;
            this.foodIndexService = foodIndexService;
        }

        /// <summary>
        /// Return all food indices in a given currency
        /// </summary>
        /// <param name="currencyCode" example="ARS"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FoodIndexDto>>> Get(string currencyCode)
        {
            var foodIndices = await this.foodIndexService.GetAll(currencyCode);

            return this.mapper.Map<List<FoodIndexDto>>(foodIndices);
        }
    }
}
