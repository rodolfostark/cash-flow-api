using AutoMapper;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Repositories.Expenses;

namespace CashFlow.Application.UseCases.Expenses.GetAll;
public class GetAllExpenseUseCase(IExpensesReadOnlyRepository expensesReadOnlyRepository, IMapper mapper) : IGetAllExpensesUseCase
{
    private readonly IExpensesReadOnlyRepository _expensesReadOnlyRepository = expensesReadOnlyRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<ResponseExpensesJson> Execute()
    {
        var expenses = await _expensesReadOnlyRepository.GetAll();
        var response = new ResponseExpensesJson
        {
            Expenses = _mapper.Map<List<ResponseShortExpenseJson>>(expenses)
        };
        return response;
    }
}
