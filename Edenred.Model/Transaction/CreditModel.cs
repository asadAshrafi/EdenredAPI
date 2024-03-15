using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edenred.Model
{
    public class CreditModel
    {
        [Required]
        public int userId { get; set; }
        [Required]
        [Range(0, 999999999, ErrorMessage = "Please add value between 0-999999999")]
        public decimal Amount { get; set; }
    }
}
