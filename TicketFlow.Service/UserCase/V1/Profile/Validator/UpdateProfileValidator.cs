using FluentValidation;

namespace TicketFlow.Service.UserCase.V1.Profile.Validator;

public class UpdateProfileValidator: AbstractValidator<Request.UpdateProfileRequest>
{
    public UpdateProfileValidator()
    {
        RuleFor(x => x.Username).NotEmpty().NotNull().MaximumLength(30);
    }
}