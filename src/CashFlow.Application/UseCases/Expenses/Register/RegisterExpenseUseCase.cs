using AutoMapper;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Exception.ExceptionsBase;

namespace CashFlow.Application.UseCases.Expenses.Register;
public class RegisterExpenseUseCase : IRegisterExpenseUseCase
{
    private readonly IExpensesRepository _expensesRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public RegisterExpenseUseCase(IExpensesRepository expensesRepository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _expensesRepository = expensesRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ResponseRegisteredExpenseJson> Execute(RequestRegisterExpenseJson request)
    {
        Validate(request);
        var expense = _mapper.Map<Expense>(request);
        await _expensesRepository.Add(expense);
        await _unitOfWork.Commit();
        return _mapper.Map<ResponseRegisteredExpenseJson>(expense);
    }
    private void Validate(RequestRegisterExpenseJson request)
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
