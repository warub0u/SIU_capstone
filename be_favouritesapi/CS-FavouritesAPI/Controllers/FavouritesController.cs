using CS_FavouritesAPI.Models;
using CS_FavouritesAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CS_FavouritesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavouritesController : ControllerBase
    {
        private readonly IFavouritesService service;

        public FavouritesController(IFavouritesService service)
        {
            this.service = service;
        }

        [HttpGet]
        public IActionResult Get(string username)
        {
            return Ok(service.GetFavourites(username));
        }

        [HttpPost]
        public IActionResult Post(Favourites favourites)
        {
            return Ok(service.AddFavourites(favourites));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok(service.DeleteFavourites(id));
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Favourites favourites)
        {
            return Ok(service.UpdateFavourites(id, favourites));
        }
    }
}
