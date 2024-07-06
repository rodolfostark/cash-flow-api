using AutoMapper;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Users;
using CashFlow.Domain.Security.Cryptography;
using CashFlow.Domain.Security.Tokens;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionsBase;
using FluentValidation.Results;

namespace CashFlow.Application.UseCases.Users.Register;
public class RegisterUserUseCase(IMapper mapper
    , IPasswordEncrypter passwordEncrypter
    , IUserReadOnlyRepository userReadOnlyRepository
    , IUserWriteOnlyRepository userWriteOnlyRepository
    , IUnitOfWork unitOfWork
    , IAccessTokenGenerator tokenGenerator) : IRegisterUserUseCase
{
    private readonly IMapper _mapper = mapper;
    private readonly IPasswordEncrypter _passwordEncrypter = passwordEncrypter;
    private readonly IUserReadOnlyRepository _userReadOnlyRepository = userReadOnlyRepository;
    private readonly IUserWriteOnlyRepository _userWriteOnlyRepository = userWriteOnlyRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IAccessTokenGenerator _tokenGenerator = tokenGenerator;

    public async Task<ResponseRegisteredUserJson> Execute(RequestRegisterUserJson request)
    {
        await Validate(request);
        
        var user = _mapper.Map<User>(request);
        user.Password = _passwordEncrypter.Encrypt(request.Password);
        user.UserIdentifier = Guid.NewGuid();

        await _userWriteOnlyRepository.Add(user);
        await _unitOfWork.Commit();

        var response = _mapper.Map<ResponseRegisteredUserJson>(user);
        response.Token = _tokenGenerator.Generate(user);

        return response;
    }
    private async Task Validate(RequestRegisterUserJson request)
    {
        var validator = new RegisterUserValidator();
        var result = validator.Validate(request);

        var emailExist = await _userReadOnlyRepository.ExistActiveUserWithEmail(request.Email);
        if (emailExist)
        {
            result.Errors.Add(new ValidationFailure(string.Empty, ResourceErrorMessages.EMAIL_ALREADY_REGISTERED));
        }

        if (!result.IsValid)
        {
            var errorMessages = result.Errors
                .Select(error => error.ErrorMessage)
                .ToList();
            throw new ErrorOnValidationException(errorMessages);
        }

    }
}
