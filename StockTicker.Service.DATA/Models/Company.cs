using System.ComponentModel.DataAnnotations;

namespace StockTicker.Service.Data.Models
{
    public class Company
    {
        [Required]
        public int CompanyId { get; set; }
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        [Required]
        [MaxLength(20)]
        public string Exchange { get; set; }
        [Required]
        [MaxLength(5)]
        public string Ticker { get; set; }
        [Required]
        [MaxLength(12)]
        [RegularExpression("[A-Za-z]{2}(.*)", ErrorMessage = "First two characters of Isin must be letters only.")]
        public string Isin { get; set; }
        [MaxLength(50)]
        public string website { get; set; }
    }
}