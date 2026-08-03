using FluentValidation;

namespace TicketFlow.Service.UserCase.V1.Queries.Identity.Login;

public class LoginValidatorQuery: AbstractValidator<LoginRequestQuery>
{
    public LoginValidatorQuery()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Password).MaximumLength(50);
    }
}