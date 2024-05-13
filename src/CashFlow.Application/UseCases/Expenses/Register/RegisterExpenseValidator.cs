using CashFlow.Communication.Requests;
using FluentValidation;

namespace CashFlow.Application.UseCases.Expenses.Register;
public class RegisterExpenseValidator : AbstractValidator<RequestRegisterExpenseJson>
{
    public RegisterExpenseValidator()
    {
        RuleFor(expense => expense.Title).NotNull().NotEmpty().WithMessage("The title is required");
        RuleFor(expense => expense.Description).NotNull();
        RuleFor(expense => expense.Amount).GreaterThan(0).WithMessage("The Amount must be greater than zero");
        RuleFor(expense => expense.Date).LessThanOrEqualTo(DateTime.UtcNow).WithMessage("Expenses cannot be from the future");
        RuleFor(expense => expense.PaymentType).IsInEnum().WithMessage("Invalid Payment Type");
    }
}
