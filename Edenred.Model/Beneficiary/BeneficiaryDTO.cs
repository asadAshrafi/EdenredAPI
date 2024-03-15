using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edenred.Model
{
    public class BeneficiaryDTO
    {
        public int Id { get; set; }
        [StringLength(20)]
        public string Nickname { get; set; }
        public int UserId { get; set; }
    }
}
