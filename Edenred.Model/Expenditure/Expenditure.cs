namespace Edenred.Model
{
    public class Expenditure
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int MonthYear { get; set; }
        public int BeneficiaryId { get; set; }
        public decimal Amount { get; set; }
        public Beneficiary Beneficiary { get; set; }
        public User User { get; set; }
    }
}