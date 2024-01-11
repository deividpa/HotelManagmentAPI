using HotelAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelController : ControllerBase
    {
        public IEnumerable<Hotel> GetHotels()
        {
            return new List<Hotel>
            {
                new Hotel{Id = 1, Name = "Hilton"},
                new Hotel{Id = 2, Name = "Selene"},
                new Hotel{Id = 3, Name = "Clock's"}
            };
        }
    }
}
