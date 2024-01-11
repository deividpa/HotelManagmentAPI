using HotelAPI.Data;
using HotelAPI.Models;
using HotelAPI.Models.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<HotelDto>> GetHotels()
        {
            return Ok(HotelStore.HotelList);
        }

        [HttpGet("id:int")]
        public ActionResult<HotelDto> GetHotel(int id)
        {
            if(id <= 0)
            {
                return BadRequest("Incorrect id Hotel");
            }

            var hotel = HotelStore.HotelList.FirstOrDefault(x => x.Id == id);

            if(hotel == null)
            {
                return NotFound("Hotel not found");
            }

            return Ok(hotel);
        }
    }
}
