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
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<HotelDto>> GetHotels()
        {
            return Ok(HotelStore.HotelList);
        }

        [HttpGet("id:int")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
