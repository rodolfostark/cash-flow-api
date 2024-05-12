using CashFlow.Communication.Enums;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;

namespace CashFlow.Application.UseCases.Expenses.Register;
public class RegisterExpenseUseCase
{
    public ResponseRegisteredExpenseJson Execute(RequestRegisterExpenseJson request)
    {
        Validate(request);
        return new ResponseRegisteredExpenseJson();
    }
    private void Validate(RequestRegisterExpenseJson request)
    {
        if (string.IsNullOrWhiteSpace(request.Title))
        {
            throw new ArgumentException("The title is required");
        }
        if (request.Amount <= 0)
        {
            throw new ArgumentException("The amount must be greater than zero");
        }
        if (DateTime.Compare(request.Date, DateTime.UtcNow) > 0)
        {
            throw new ArgumentException("Expenses cannot be from the future");
        }
        if (!Enum.IsDefined<PaymentType>(request.PaymentType))
        {
            throw new ArgumentException($"Invalid payment type {request.PaymentType}");
        }
    }
}
