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
        /// Return all food indices
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FoodIndexDto>>> Get()
        {
            var foodIndices = await this.foodIndexService.GetAll();

            return this.mapper.Map<List<FoodIndexDto>>(foodIndices);
        }
    }
}
