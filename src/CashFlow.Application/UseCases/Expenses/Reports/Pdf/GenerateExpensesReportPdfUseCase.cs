using CashFlow.Application.UseCases.Expenses.Reports.Pdf.Fonts;
using CashFlow.Domain.Reports;
using CashFlow.Domain.Repositories.Expenses;
using MigraDoc.DocumentObjectModel;
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
    private static Document CreateDocumente(DateOnly month) 
    { 
        var document = new Document();
        
        document.Info.Title = $"{ResourceReportGenerationMessages.EXPENSES_FOR} {month:Y)}";
        document.Info.Author = "Jonatas Santos";

        var style = document.Styles["Normal"];
        style.Font.Name = FontHelper.RALEWAY_REGULAR;

        return document;
    }
}
