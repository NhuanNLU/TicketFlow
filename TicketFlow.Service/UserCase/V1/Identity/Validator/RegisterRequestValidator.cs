using FluentValidation;

namespace TicketFlow.Service.UserCase.V1.Identity.Validator;

public class RegisterRequestValidator: AbstractValidator<Request.RegisterRequest>
{
    public RegisterRequestValidator()
    {
        RuleFor(x => x.Username).NotEmpty().MaximumLength(50);
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Password).NotEmpty().MinimumLength(6).MaximumLength(20);
        RuleFor(x => x.ConfirmPassword).NotEmpty().Equal(x => x.Password);
    }
}