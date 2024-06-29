using CashFlow.Communication.Requests;
using FluentValidation;

namespace CashFlow.Application.UseCases.Users.Register;
public class RegisterUserValidator : AbstractValidator<RequestRegisterUserJson>
{
    public RegisterUserValidator()
    {
        RuleFor(user => user.Name)
            .NotEmpty()
            .WithMessage("Name Empty");

        RuleFor(user => user.Email)
            .NotEmpty()
            .WithMessage("Email Empty")
            .EmailAddress()
            .WithMessage("Email Invalid");
        
        RuleFor(user => user.Password)
            .NotEmpty()
            .WithMessage("Passaword Empty")
            .SetValidator(new PasswordValidator<RequestRegisterUserJson>());
    }
}
