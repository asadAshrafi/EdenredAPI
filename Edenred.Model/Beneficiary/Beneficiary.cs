using System.ComponentModel.DataAnnotations;

namespace Edenred.Model
{
    public class Beneficiary
    {
        public int Id { get; set; }
        [StringLength(20)]
        public string NickName { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public ICollection<Expenditure> Expenditure { get; set; }
    }
}