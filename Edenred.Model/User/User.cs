using System.ComponentModel.DataAnnotations;

namespace Edenred.Model
{
    public class User
    {
        public int Id { get; set; }
        [StringLength(20)]
        public string Name { get; set; }
        public bool IsVerified { get; set; }
        public decimal Balance { get; set; }
        public ICollection<Beneficiary> Beneficiaries { get; set; }
        public ICollection<Expenditure> Expenditure { get; set; }
    }
}