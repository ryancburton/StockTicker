using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace StockTicker.Service.Data.Models
{
    public class TextSearchResults
    {
        [Required]
        [MaxLength(2000)]
        public string Results { get; set; }
    }
}
