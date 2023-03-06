using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using OneMapAPI.Models;
using OneMapAPI.Services;

namespace OneMapAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OneMapController : ControllerBase
    {
        private readonly IOneMapService service;

        public OneMapController(IOneMapService service)
        {
            this.service = service;
        }

        [HttpGet("Convert")]
        public async Task<ActionResult<Coordinates>> GetCoordinates(string postalCode)
        {
            var coordinates = await service.GetCoordinates(postalCode);
            if (coordinates == null)
            {
                return NotFound();
            }

            return coordinates;
        }

        [HttpGet("Route")]
        public async Task<ActionResult<List<LegInformation>>> Get(string startLang, string startLong, string endLang, string endLong, string mode)
        {
            var routeResponse = await service.GetRoute(startLang, startLong, endLang, endLong, mode);

            if (routeResponse == null)
            {
                return NotFound();
            }

            return routeResponse;
        }
    }
}
