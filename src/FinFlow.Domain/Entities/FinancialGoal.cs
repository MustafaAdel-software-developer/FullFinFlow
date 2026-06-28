using FinFlow.Domain.Enums;

namespace FinFlow.Domain.Entities
{
    public class FinancialGoal : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal TargetAmount { get; set; }
        public decimal CurrentAmount { get; set; }
        public DateTime TargetDate { get; set; }
        public GoalTypes GoalType { get; set; }
        public bool IsCompleted { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
