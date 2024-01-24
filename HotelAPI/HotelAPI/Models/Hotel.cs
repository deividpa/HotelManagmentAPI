using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelAPI.Models
{
    public class Hotel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public String Name { get; set; }
        public string City { get; set; }
        public string Detail { get; set; }
        public int Capacity { get; set; }
        [Required]
        public double Price { get; set; }
        public string ImageURL {  get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime UpdateDate { get; set;}
    }
}
