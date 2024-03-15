using System.ComponentModel.DataAnnotations;

namespace Edenred.Model
{
    public class UserDTO
    {
        [Required]
        public int Id { get; set; }
        [StringLength(20)]
        [Required]
        public string Name { get; set; }
        [Required]
        public bool IsVerified { get; set; }
    }
}