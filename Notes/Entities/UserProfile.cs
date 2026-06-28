using FinFlow.Domain.Enums;

namespace FinFlow.Domain.Entities
{
    public class UserProfile : BaseEntity
    {
        public string ProfilePicture { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string ZipCode { get; set; } = string.Empty;
        public string Country { get; set; }
        public decimal? AnnualIncome { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public RiskTolerance RiskTolerance { get; set; }
        public FinancialExperience FinancialExperience { get; set; }
        public int UserId {get;set;}
        public User User { get; set; }
    }
}
