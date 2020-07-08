using System.ComponentModel.DataAnnotations;

namespace StockTicker.Web.Models
{
    public class AuthenticationViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }        
    }
}