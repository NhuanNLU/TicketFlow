using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TicketFlow.Repository;
using TicketFlow.Repository.Abstractions;
using TicketFlow.Repository.Abstractions.Message;
using TicketFlow.Repository.Abstractions.Shared;
using TicketFlow.Repository.Entities;
using TicketFlow.Repository.Enum.User;
using TicketFlow.Service.UserCase.V1.Events.User;

namespace TicketFlow.Service.UserCase.V1.Commands.Identity.Register;

public class RegisterCommandHandler: ICommandHandler<RegisterCommand>
{
    private readonly AppDbContext _dbContext;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPublisher _publisher;
    public RegisterCommandHandler(AppDbContext dbContext, IUnitOfWork unitOfWork, IPublisher publisher)
    {
        _dbContext = dbContext;
        _unitOfWork = unitOfWork;
        _publisher = publisher;
    }

    public async Task<Result> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var user = await _dbContext.Users.AnyAsync(x => x.Email == request.Email, cancellationToken);
        if (user)
        {
            throw new InvalidOperationException($"User with email '{request.Email}' does exist.");
        }

        var newUser = new User()
        {
            Id = Guid.NewGuid(),
            Username = request.Username,
            Email = request.Email,
            Password = BCrypt.Net.BCrypt.HashPassword(request.Password),
            Role = RoleEnum.Customer,
            Status = StatusUserEnum.Active,
            EmailConfirmed = false,
            CreatedDate =  DateTimeOffset.UtcNow,
            CreatedBy = request.Email,
        };
        _dbContext.Users.Add(newUser);
        await _unitOfWork.CommitAsync(cancellationToken);
        // await _publisher.Publish(new RegisteredEvent{
        //     Id = newUser.Id, 
        //     Email = newUser.Email, 
        //     UserName = newUser.Username
        // }, cancellationToken);
        return Result.Success();
    }
}