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
        public async Task<ActionResult<IEnumerable<HotelDto>>> GetHotels()
        {
            _logger.LogInformation("The hotels were gotten succesfully");
            return Ok(await _db.Hotels.ToListAsync());
        }

        [HttpGet("{id:int}", Name = "GetHotel")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<HotelDto>> GetHotel(int id)
        {
            if (id <= 0)
            {
                _logger.LogError("Error: the id is lower or equal to zero");
                return BadRequest("Incorrect id Hotel");
            }

            //var hotel = HotelStore.HotelList.FirstOrDefault(x => x.Id == id);
            var hotel = await _db.Hotels.FirstOrDefaultAsync(h => h.Id == id);

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
        public async Task<ActionResult<HotelDto>> CreateHotel([FromBody] HotelCreateDto hotelCreateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (await _db.Hotels.FirstOrDefaultAsync(h => h.Name.ToLower() == hotelCreateDto.Name.ToLower()) != null)
            {
                ModelState.AddModelError("ExistingName", "There is a hotel with the same name!");
                return BadRequest(ModelState);
            }

            if (hotelCreateDto == null)
            {
                return BadRequest(hotelCreateDto);
            }

            //Gets the last Hotel ID, and the adds 1 for the new hotel
            //hotelDto.Id = HotelStore.HotelList.OrderByDescending(x => x.Id).FirstOrDefault().Id + 1;
            //HotelStore.HotelList.Add(hotelDto);

            Hotel model = new()
            {
                Name = hotelCreateDto.Name,
                City = hotelCreateDto.City,
                Detail = hotelCreateDto.Detail,
                Capacity = hotelCreateDto.Capacity,
                Price = hotelCreateDto.Price,
                ImageURL = hotelCreateDto.ImageURL,
                CreationDate = DateTime.Now
            };

            await _db.Hotels.AddAsync(model);
            await _db.SaveChangesAsync();

            return CreatedAtRoute("GetHotel", new { id = model.Id }, model);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async  Task<IActionResult> UpdateHotel(int id, [FromBody] HotelUpdateDto hotelDto)
        {
            if (hotelDto == null || id != hotelDto.Id)
            {
                return BadRequest();
            }

            //var hotel = HotelStore.HotelList.FirstOrDefault(h => h.Id == id);
            var hotel = await _db.Hotels.FirstOrDefaultAsync(h => h.Id == id);

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
            await _db.SaveChangesAsync();

            //hotel.Name = hotelDto.Name;
            //hotel.Capacity = hotelDto.Capacity;
            //hotel.City = hotelDto.City;

            return NoContent();
        }



        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteHotel(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Incorrect id number");
            }

            //var hotel = HotelStore.HotelList.FirstOrDefault(h => h.Id == id);
            var hotel = await _db.Hotels.FirstOrDefaultAsync(h => h.Id == id);

            if (hotel == null)
            {
                return NotFound();
            }

            //HotelStore.HotelList.Remove(hotel);
            _db.Hotels.Remove(hotel);
            await _db.SaveChangesAsync();

            return NoContent();
        }

        [HttpPatch("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdatePartialHotel(int id, JsonPatchDocument<HotelUpdateDto> patchDto)
        {
            if (patchDto == null || id <= 0)
            {
                return BadRequest();
            }

            //var hotel = HotelStore.HotelList.FirstOrDefault(h => h.Id == id);
            var hotel = await _db.Hotels.AsNoTracking().FirstOrDefaultAsync(h => h.Id == id);

            if (hotel == null) return NotFound();

            HotelUpdateDto hotelDto = new()
            {
                Id = hotel.Id,
                Name = hotel.Name,
                City = hotel.City,
                Detail = hotel.Detail,
                Capacity = hotel.Capacity,
                Price = hotel.Price,
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
                Price = hotelDto.Price,
                ImageURL = hotelDto.ImageURL
            };

            _db.Hotels.Update(model);
            await _db.SaveChangesAsync();

            return NoContent();
        }

    }
}
