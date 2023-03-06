using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using taxiapi.Models;
using taxiapi.Repository;
using taxiapi.Services;

namespace taxiapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaxiController : ControllerBase
    {
        private readonly IFareService service;

        public TaxiController(IFareService service)
        {
            this.service = service;
        }

        [HttpGet("Convert")]
        public async Task<ActionResult<OneMapCoordinates>> GetCoordinates(string postalCode)
        {
            var coordinates = await service.GetCoordinates(postalCode);
            if (coordinates == null)
            {
                return NotFound();
            }

            return coordinates;
        }

        [HttpGet("TimeDistance")]
        public async Task<ActionResult<FaresInformation>> GetTotalTimeDistance(string startLang, string startLong, string endLang, string endLong)
        {
            var routeResponse = await service.GetTotalTimeDistance(startLang, startLong, endLang, endLong);
            if (routeResponse == null)
            {
                return StatusCode(404, "Something went wrong!");
            }
            return routeResponse;
        }

        [HttpGet("CDGFare")]
        public async Task<IActionResult> GetFareForCDGApp(string startLang, string startLong, string endLang, string endLong)
        {
            var fare = await service.GetFareForCDGApp(startLang, startLong, endLang, endLong);
            if (fare == null)
            {
                return StatusCode(404, "Something went wrong!");
            }
            return Ok(fare);
           

        }

        [HttpGet("Meter")]
        public async Task<IActionResult> GetFareForMetered(string startLang, string startLong, string endLang, string endLong)
        {
            var fare = await service.GetFareForMetered(startLang, startLong, endLang, endLong);
            if (fare == null)
            {
                return StatusCode(404, "Something went wrong!");
            }
            return Ok(fare);
        }



    }
}
