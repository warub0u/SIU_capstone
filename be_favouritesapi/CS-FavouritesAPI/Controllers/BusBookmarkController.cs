using CS_FavouritesAPI.Models;
using CS_FavouritesAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CS_FavouritesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BusBookmarkController : ControllerBase
    {
        private readonly IBusBookmarkService service;

        public BusBookmarkController(IBusBookmarkService service)
        {
            this.service = service;
        }

        [HttpGet("{username}")]
        public IActionResult Get(string username)
        {
            return Ok(service.GetBusBookmarks(username));
        }

        [HttpPost]
        public IActionResult Post(BusBookmark busBookmark)
        {
            return Ok(service.AddBookmark(busBookmark));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id) 
        {
            return Ok(service.DeleteBookmark(id));
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, BusBookmark busBookmark)
        {
            return Ok(service.UpdateBookmark(id, busBookmark));
        }
    }
}
