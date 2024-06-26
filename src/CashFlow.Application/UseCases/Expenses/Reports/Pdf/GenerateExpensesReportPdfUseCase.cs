using CashFlow.Application.UseCases.Expenses.Reports.Pdf.Colors;
using CashFlow.Application.UseCases.Expenses.Reports.Pdf.Fonts;
using CashFlow.Domain.Reports;
using CashFlow.Domain.Repositories.Expenses;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;
using PdfSharp.Fonts;
using System.Globalization;
using System.Reflection;

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

        var document = CreateDocument(month);
        var page = CreatePage(document);
        CreateHeaderWithProfileImageAndName(page);
        
        var totalExpensed = expenses.Sum(expense => expense.Amount);
        CreateTotalSpentSection(page, month, totalExpensed);

        foreach (var expense in expenses) 
        {
            var table = CreateExpenseTable(page);
            var row = table.AddRow();

            row.Height = 25;
            row.Cells[0].AddParagraph(expense.Title);
            row.Cells[0].Format.Font = new Font { Name = FontHelper.RALEWAY_BLACK, Size = 14, Color = ColorHelper.BLACK };
            row.Cells[0].Shading.Color = ColorHelper.RED_LIGHT;
            row.Cells[0].VerticalAlignment = VerticalAlignment.Center;
            row.Cells[0].MergeRight = 2;
            row.Cells[0].Format.LeftIndent = 2;

            row.Height = 25;
            row.Cells[3].AddParagraph(ResourceReportGenerationMessages.AMOUNT);
            row.Cells[3].Format.Font = new Font { Name = FontHelper.RALEWAY_BLACK, Size = 14, Color = ColorHelper.WHITE };
            row.Cells[3].Shading.Color = ColorHelper.RED_DARK;
            row.Cells[3].VerticalAlignment = VerticalAlignment.Center;

            var blankRow = table.AddRow();
            blankRow.Height = 30;
            blankRow.Borders.Visible = false;
        }

        return RenderDocument(document);
    }
    private static Document CreateDocument(DateOnly month) 
    { 
        var document = new Document();
        
        document.Info.Title = $"{ResourceReportGenerationMessages.EXPENSES_FOR} {month:Y)}";
        document.Info.Author = "Jonatas Santos";

        var style = document.Styles["Normal"];
        style!.Font.Name = FontHelper.RALEWAY_REGULAR;

        return document;
    }
    private static Section CreatePage(Document document)
    {
        var section = document.AddSection();
        section.PageSetup = document.DefaultPageSetup.Clone();
        
        section.PageSetup.PageFormat = PageFormat.A4;
        section.PageSetup.LeftMargin = 40;
        section.PageSetup.RightMargin = 40;
        section.PageSetup.TopMargin = 80;
        section.PageSetup.BottomMargin = 80;

        return section;
    }

    private static void CreateHeaderWithProfileImageAndName(Section page)
    {
        var table = page.AddTable();

        table.AddColumn();
        table.AddColumn("300");

        var row = table.AddRow();

        var image = row.Cells[0].AddImage(GetImageFileName());

        row.Cells[1].AddParagraph("Hey, Jonatas Santos");
        row.Cells[1].Format.Font = new Font { Name = FontHelper.RALEWAY_BLACK, Size = 16 };
        row.Cells[1].VerticalAlignment = MigraDoc.DocumentObjectModel.Tables.VerticalAlignment.Center;
    }
    private static void CreateTotalSpentSection(Section page, DateOnly month, decimal totalExpensed)
    {
        var paragraph = page.AddParagraph();
        paragraph.Format.SpaceBefore = "40";
        paragraph.Format.SpaceAfter = "40";

        var title = string.Format(ResourceReportGenerationMessages.TOTAL_SPENT_IN, month.ToString("Y"));

        paragraph.AddFormattedText(title, new Font { Name = FontHelper.RALEWAY_REGULAR, Size = 15 });
        paragraph.AddLineBreak();

        paragraph.AddFormattedText($"{totalExpensed} {CultureInfo.CurrentCulture.NumberFormat.CurrencySymbol}", new Font { Name = FontHelper.WORKSANS_BLACK, Size = 50 });
    }
    private static byte[] RenderDocument(Document document)
    {
        var renderer = new PdfDocumentRenderer
        {
            Document = document,
        };

        renderer.RenderDocument();

        using var file = new MemoryStream();
        renderer.PdfDocument.Save(file);
        
        return file.ToArray();
    }
    private static string GetImageFileName()
    {
        var assembly = Assembly.GetExecutingAssembly();
        var directory = Path.GetDirectoryName(assembly.Location);
        var fileName = Path.Combine(directory!, "UseCases\\Expenses\\Reports\\Pdf\\Images", "avatar.png");

        return fileName;
    }
    private static Table CreateExpenseTable(Section page)
    {
        var table = page.AddTable();

        table.AddColumn("195")
            .Format.Alignment = ParagraphAlignment.Left;
        table.AddColumn("80")
            .Format.Alignment = ParagraphAlignment.Center;
        table.AddColumn("120")
            .Format.Alignment = ParagraphAlignment.Center;
        table.AddColumn("120")
            .Format.Alignment = ParagraphAlignment.Right;

        return table;
    }
}
