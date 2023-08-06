using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTicker.Service.Data.Models
{
    public class Thesis
    {

        [Required]
        public int ThesisId { get; set; }
        public string TextName { get; set; }

        public string Text { get; set; }
    }
}
