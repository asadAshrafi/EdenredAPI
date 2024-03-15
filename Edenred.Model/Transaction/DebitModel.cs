using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edenred.Model
{
    public class DebitModel
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public int BeneficiaryId { get; set; }
        [Required]
        [Range(0, 999999999, ErrorMessage = "Please add value between 0-999999999")]
        public decimal Amount { get; set; }
    }
}
