using HotelAPI.Models.Dto;

namespace HotelAPI.Data
{
    public static class HotelStore
    {
        public static List<HotelDto> HotelList = new List<HotelDto>
        {
            new HotelDto{Id = 1, Name = "Hilton"},
            new HotelDto{Id = 2, Name = "Selene"},
            new HotelDto{Id = 3, Name = "Clock's"}
        };
    }
}
