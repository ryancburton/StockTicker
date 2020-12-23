using System.ComponentModel.DataAnnotations;

namespace CarParts.Service.DAL.Models
{
    public class CarPart
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        public int Duration { get; set; }
        [Required]
        public int Price { get; set; }        
    }
}
