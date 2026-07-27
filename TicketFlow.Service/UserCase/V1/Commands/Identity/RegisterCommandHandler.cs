using TicketFlow.Repository.Abstractions.Message;

namespace TicketFlow.Service.UserCase.V1.Commands.Identity;

public class RegisterCommandHandler: ICommandHandler<RegisterCommand>
{
    public Task Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}