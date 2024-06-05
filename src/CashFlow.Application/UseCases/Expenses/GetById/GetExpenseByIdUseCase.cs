using AutoMapper;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Exception;

namespace CashFlow.Application.UseCases.Expenses.GetById;
public class GetExpenseByIdUseCase(IExpensesRepository expensesRepository, IMapper mapper) : IGetExpenseByIdUseCase
{
    private readonly IExpensesRepository _expensesRepository = expensesRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<ResponseExpenseJson> Execute(long id)
    {
        var expense = await _expensesRepository.GetById(id);
        if (expense is null)
        {
            throw new DirectoryNotFoundException(ResourceErrorMessages.);
        }
        
        var response = _mapper.Map<ResponseExpenseJson>(expense);
        return response;
    }
}
