
using CashFlow.Domain.Repositories.Expenses;

namespace CashFlow.Application.UseCases.Expenses.Reports.Pdf;
public class GenerateExpensesReportPdfUseCase(IExpensesReadOnlyRepository expensesReadOnlyRepository) : IGenerateExpensesReportPdfUseCase
{
    private readonly IExpensesReadOnlyRepository _expensesReadOnlyRepository = expensesReadOnlyRepository;
    public async Task<byte[]> Execute(DateOnly month)
    {
        var expenses = await _expensesReadOnlyRepository.GetExpensesByMonth(month);
        if (expenses.Count == 0)
        {
            return [];
        }
        return [];
    }
}
