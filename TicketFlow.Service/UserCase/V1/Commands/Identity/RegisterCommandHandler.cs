using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TicketFlow.Repository;
using TicketFlow.Repository.Abstractions;
using TicketFlow.Repository.Abstractions.Message;
using TicketFlow.Repository.Entities;
using TicketFlow.Repository.Enum.User;

namespace TicketFlow.Service.UserCase.V1.Commands.Identity;

public class RegisterCommandHandler: ICommandHandler<RegisterCommand>
{
    private readonly AppDbContext _dbContext;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public RegisterCommandHandler(AppDbContext dbContext, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _dbContext = dbContext;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var user = await _dbContext.Users.AnyAsync(x => x.Email == request.Email, cancellationToken);
        if (user)
        {
            throw new InvalidOperationException($"User with email '{request.Email}' does exist.");
        }
        var newUser = _mapper.Map<User>(request);
        newUser.Role = RoleEnum.Customer;
        newUser.Status = StatusUserEnum.Active;
        newUser.EmailConfirmed = false;
        newUser.Password = BCrypt.Net.BCrypt.HashPassword(request.Password);
        _dbContext.Users.Add(newUser);
        //Gửi mail
        await _unitOfWork.CommitAsync(cancellationToken);
    }
}