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

        [HttpGet("{id:int}", Name = "GetHotel")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<HotelDto> GetHotel(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Incorrect id Hotel");
            }

            var hotel = HotelStore.HotelList.FirstOrDefault(x => x.Id == id);

            if (hotel == null)
            {
                return NotFound("Hotel not found");
            }

            return Ok(hotel);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<HotelDto> CreateHotel([FromBody] HotelDto hotelDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (HotelStore.HotelList.FirstOrDefault(h => h.Name.ToLower() == hotelDto.Name.ToLower()) != null)
            {
                ModelState.AddModelError("ExistingName", "There is a hotel with the same name!");
                return BadRequest(ModelState);
            }

                if (hotelDto == null)
            {
                return BadRequest(hotelDto);
            }

            if (hotelDto.Name == "")
            {
                return BadRequest(new {errorMessage = "Name can't be an empty value", hotelDto});
            }

            if (hotelDto.Id < 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            // Gets the last Hotel ID, and the adds 1 for the new hotel
            hotelDto.Id = HotelStore.HotelList.OrderByDescending(x => x.Id).FirstOrDefault().Id + 1;

            HotelStore.HotelList.Add(hotelDto);

            return CreatedAtRoute("GetHotel", new {id = hotelDto.Id}, hotelDto);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateHotel(int id, [FromBody] HotelDto hotelDto)
        {
            if (hotelDto == null || id != hotelDto.Id)
            {
                return BadRequest();
            }

            var hotel = HotelStore.HotelList.FirstOrDefault(h => h.Id == id);

            if (hotel == null)
            {
                return NotFound();
            }

            hotel.Name = hotelDto.Name;
            hotel.Capacity = hotelDto.Capacity;
            hotel.City = hotelDto.City;

            return NoContent();
        }



        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteHotel(int id)
        {
            if(id<=0)
            {
                return BadRequest("Incorrect id number");
            }

            var hotel = HotelStore.HotelList.FirstOrDefault(h => h.Id == id);

            if (hotel == null)
            {
                return NotFound();
            }

            HotelStore.HotelList.Remove(hotel);

            return NoContent();
        }
    }
}
