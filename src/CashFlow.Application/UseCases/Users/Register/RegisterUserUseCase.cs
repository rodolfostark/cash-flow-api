using AutoMapper;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Entities;

namespace CashFlow.Application.UseCases.Users.Register;
public class RegisterUserUseCase(IMapper mapper) : IRegisterUserUseCase
{
    private readonly IMapper _mapper = mapper;
    public async Task<ResponseRegisteredUserJson> Execute(RequestRegisterUserJson request)
    {
        Validate(request);
        var user = _mapper.Map<User>(request);
        return _mapper.Map<ResponseRegisteredUserJson>(user);
    }
    private void Validate(RequestRegisterUserJson request)
    {

    }
}
