using FoodPrices.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace FoodPrices.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FoodIndexController : ControllerBase
    {
        /// <summary>
        /// Return all food indices
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FoodIndexDto>>> Get()
        {
            throw new NotImplementedException();
        }
    }
}
