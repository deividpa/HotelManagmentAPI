using HotelAPI.Data;
using HotelAPI.Models;
using HotelAPI.Models.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelController : ControllerBase
    {
        private readonly ILogger<HotelController> _logger;
        private readonly ApplicationDbContext _db;
        public HotelController(ILogger<HotelController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<HotelDto>> GetHotels()
        {
            _logger.LogInformation("The hotels were gotten succesfully");
            return Ok(_db.Hotels.ToList());
        }

        [HttpGet("{id:int}", Name = "GetHotel")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<HotelDto> GetHotel(int id)
        {
            if (id <= 0)
            {
                _logger.LogError("Error: the id is lower or equal to zero");
                return BadRequest("Incorrect id Hotel");
            }

            //var hotel = HotelStore.HotelList.FirstOrDefault(x => x.Id == id);
            var hotel = _db.Hotels.FirstOrDefault(h => h.Id == id);

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

            if (_db.Hotels.FirstOrDefault(h => h.Name.ToLower() == hotelDto.Name.ToLower()) != null)
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
                return BadRequest(new { errorMessage = "Name can't be an empty value", hotelDto });
            }

            if (hotelDto.Id < 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            //Gets the last Hotel ID, and the adds 1 for the new hotel
            //hotelDto.Id = HotelStore.HotelList.OrderByDescending(x => x.Id).FirstOrDefault().Id + 1;
            //HotelStore.HotelList.Add(hotelDto);

            Hotel model = new()
            {
                Name = hotelDto.Name,
                City = hotelDto.City,
                Detail = hotelDto.Detail,
                Capacity = hotelDto.Capacity,
                Price = hotelDto.Price,
                ImageURL = hotelDto.ImageURL,
                CreationDate = DateTime.Now
            };

            _db.Hotels.Add(model);
            _db.SaveChanges();

            return CreatedAtRoute("GetHotel", new { id = hotelDto.Id }, hotelDto);
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

            //var hotel = HotelStore.HotelList.FirstOrDefault(h => h.Id == id);
            var hotel = _db.Hotels.FirstOrDefault(h => h.Id == id);

            if (hotel == null)
            {
                return NotFound();
            }

            Hotel model = new()
            {
                Id = hotelDto.Id,
                Name = hotelDto.Name,
                City = hotelDto.City,
                Detail = hotelDto.Detail,
                Capacity = hotelDto.Capacity,
                Price = hotelDto.Price,
                ImageURL = hotelDto.ImageURL,
                UpdateDate = DateTime.Now
            };

            _db.Hotels.Update(model);
            _db.SaveChanges();

            //hotel.Name = hotelDto.Name;
            //hotel.Capacity = hotelDto.Capacity;
            //hotel.City = hotelDto.City;

            return NoContent();
        }



        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteHotel(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Incorrect id number");
            }

            //var hotel = HotelStore.HotelList.FirstOrDefault(h => h.Id == id);
            var hotel = _db.Hotels.FirstOrDefault(h => h.Id == id);

            if (hotel == null)
            {
                return NotFound();
            }

            //HotelStore.HotelList.Remove(hotel);
            _db.Hotels.Remove(hotel);
            _db.SaveChanges();

            return NoContent();
        }

        [HttpPatch("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdatePartialHotel(int id, JsonPatchDocument<HotelDto> patchDto)
        {
            if (patchDto == null || id <= 0)
            {
                return BadRequest();
            }

            //var hotel = HotelStore.HotelList.FirstOrDefault(h => h.Id == id);
            var hotel = _db.Hotels.AsNoTracking().FirstOrDefault(h => h.Id == id);

            if (hotel == null) return NotFound();

            HotelDto hotelDto = new()
            {
                Id = hotel.Id,
                Name = hotel.Name,
                City = hotel.City,
                Detail = hotel.Detail,
                Capacity = hotel.Capacity,
                ImageURL = hotel.ImageURL,
            };


            patchDto.ApplyTo(hotelDto, ModelState);

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Hotel model = new()
            {
                Id=hotelDto.Id,
                Name=hotelDto.Name,
                City = hotelDto.City,
                Detail = hotelDto.Detail,
                Capacity = hotelDto.Capacity,
                ImageURL = hotelDto.ImageURL
            };

            _db.Hotels.Update(model);
            _db.SaveChanges();

            return NoContent();
        }

    }
}
