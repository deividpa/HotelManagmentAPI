using HotelAPI.Models.Dto;

namespace HotelAPI.Data
{
    public static class HotelStore
    {
        public static List<HotelDto> HotelList = new List<HotelDto>
        {
            new HotelDto{Id = 1, Name = "Hilton", Capacity = 200, City = "Miami"},
            new HotelDto{Id = 2, Name = "Selene", Capacity = 160, City = "Orlando"},
            new HotelDto{Id = 3, Name = "Clock's", Capacity = 300, City = "Tampa"}
        };
    }
}
