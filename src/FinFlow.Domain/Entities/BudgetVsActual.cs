namespace FinFlow.Domain.Entities;

public class BudgetVsActual
{
    public int? BudgetId { get; set; }

    public string BudgetName { get; set; }

    public string UserName { get; set; }

    public string Category { get; set; }

    public decimal AllocatedAmount { get; set; }

    public decimal SpentAmount { get; set; }

    public decimal RemainingAmount { get; set; }

    public bool IsOverBudget { get; set; }

    public decimal PercentageUsed { get; set; }
}
