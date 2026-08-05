using FluentValidation;

namespace TicketFlow.Service.UserCase.V1.Identity.Validator;

public class LoginRequestValidator: AbstractValidator<Request.LoginRequest>
{
    public LoginRequestValidator()
    {
        RuleFor(request => request.Email).NotEmpty().EmailAddress();
        RuleFor(request => request.Password).NotEmpty().NotNull();
    }
}