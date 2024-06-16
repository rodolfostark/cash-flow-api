using CashFlow.Domain.Enums;
using CashFlow.Domain.Reports;
using CashFlow.Domain.Repositories.Expenses;
using ClosedXML.Excel;
using System.Globalization;

namespace CashFlow.Application.UseCases.Expenses.Reports.Excel;

public class GenerateExpensesReportExcelUseCase(IExpensesReadOnlyRepository expensesReadOnlyRepository) : IGenerateExpensesReportExcelUseCase
{
    private readonly IExpensesReadOnlyRepository _expensesReadOnlyRepository = expensesReadOnlyRepository;
    public async Task<byte[]> Execute(DateOnly month)
    {
        var expenses = await _expensesReadOnlyRepository.GetExpensesByMonth(month);
        
        if (expenses.Count == 0)
        {
            return [];
        }

        using var workbook = new XLWorkbook();
        workbook.Author = "Jonatas Santos";
        workbook.Style.Font.FontSize = 12;
        workbook.Style.Font.FontName = "Times New Roman";

        var worksheet = workbook.Worksheets.Add(month.ToString("Y"));
        
        InsertHeader(worksheet);

        var row = 2;
        foreach (var expense in expenses) 
        {
            worksheet.Cell($"A{row}").Value = expense.Title;
            worksheet.Cell($"B{row}").Value = expense.Date;
            worksheet.Cell($"C{row}").Value = ConvertPaymentType(expense.PaymentType);
            worksheet.Cell($"D{row}").Value = CultureInfo.CurrentCulture.NumberFormat.CurrencySymbol + expense.Amount;
            worksheet.Cell($"E{row}").Value = expense.Description;
            row++;
        }

        worksheet.Columns().AdjustToContents();

        var file = new MemoryStream();
        workbook.SaveAs(file);

        return file.ToArray();
    }
    private string ConvertPaymentType(PaymentType paymentType)
    {
        return paymentType switch
        {
            PaymentType.Cash => "Dinheiro",
            PaymentType.CreditCard => "Cartão de Crédito",
            PaymentType.DebitCard => "Cartão de Débito",
            PaymentType.EletronicTransfer => "Transferência Bancária",
            _ => string.Empty
        };
    }
    private void InsertHeader(IXLWorksheet worksheet)
    {
        worksheet.Cell("A1").Value = ResourceReportGenerationMessages.TITLE;
        worksheet.Cell("B1").Value = ResourceReportGenerationMessages.DATE;
        worksheet.Cell("C1").Value = ResourceReportGenerationMessages.PAYMENT_TYPE;
        worksheet.Cell("D1").Value = ResourceReportGenerationMessages.AMOUNT;
        worksheet.Cell("E1").Value = ResourceReportGenerationMessages.DESCRIPTION;

        worksheet.Cells("A1:E1").Style.Font.Bold = true;

        worksheet.Cells("A1:E1").Style.Fill.BackgroundColor = XLColor.FromHtml("#F5C2B6");

        worksheet.Cell("A1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        worksheet.Cell("B1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        worksheet.Cell("C1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        worksheet.Cell("D1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        worksheet.Cell("E1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Right);
    }
}
