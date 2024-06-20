using CashFlow.Application.UseCases.Expenses.Reports.Pdf.Fonts;
using CashFlow.Domain.Repositories.Expenses;
using PdfSharp.Fonts;

namespace CashFlow.Application.UseCases.Expenses.Reports.Pdf;
public class GenerateExpensesReportPdfUseCase : IGenerateExpensesReportPdfUseCase
{
    private readonly IExpensesReadOnlyRepository _expensesReadOnlyRepository;
    public GenerateExpensesReportPdfUseCase(IExpensesReadOnlyRepository expensesReadOnlyRepository)
    {
        _expensesReadOnlyRepository = expensesReadOnlyRepository;
        GlobalFontSettings.FontResolver = new ExpensesReportFontResolver();
    }
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
