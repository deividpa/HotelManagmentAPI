using System.ComponentModel.DataAnnotations;

namespace HotelAPI.Models.Dto
{
    public class HotelDto
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        
        [Required]
        [MaxLength(30)]
        public string City { get; set; }
        
        public string Detail { get; set; }
        
        [Required]
        public int Capacity { get; set; }
        
        [Required]
        public double Price { get; set; }
        
        public string ImageURL { get; set; }
        


    }
}
