using AutoMapper;
using CashFlow.Application.UseCases.Expenses.Register;
using CashFlow.Communication.Requests;
using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionsBase;

namespace CashFlow.Application.UseCases.Expenses.Update;
public class UpdateExpenseUseCase(IExpensesUpdateOnlyRepository expensesUpdateOnlyRepository, IMapper mapper, IUnitOfWork unitOfWork) : IUpdateExpenseUseCase
{
    private readonly IExpensesUpdateOnlyRepository _expensesUpdateOnlyRepository = expensesUpdateOnlyRepository;
    private readonly IMapper _mapper = mapper;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task Execute(long id, RequestExpenseJson request)
    {
        Validate(request);
        var expense = await _expensesUpdateOnlyRepository.GetById(id);
        if (expense is null) 
        {
            throw new NotFoundException(ResourceErrorMessages.EXPENSE_NOT_FOUND);
        }
        _mapper.Map(request, expense);
        _expensesUpdateOnlyRepository.Update(expense);
        await _unitOfWork.Commit();
    }
    private void Validate(RequestExpenseJson request)
    {
        var validator = new RegisterExpenseValidator();
        var result = validator.Validate(request);
        if (!result.IsValid)
        {
            var errorMessages = result.Errors.Select(error => error.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorMessages);
        }
    }
}
