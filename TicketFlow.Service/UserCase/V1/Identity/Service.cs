using System.Security.Claims;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using TicketFlow.Repository;
using TicketFlow.Repository.Abstractions;
using TicketFlow.Repository.Entities;
using TicketFlow.Repository.Enum.User;
using TicketFlow.Service.Caching;
using TicketFlow.Service.JwtService;

namespace TicketFlow.Service.UserCase.V1.Identity;

public class Service: IService
{
    private readonly IJwtTokenService _jwtTokenService;
    private readonly AppDbContext _dbContext;
    private readonly ICacheService _cacheService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<Request.RegisterRequest> _registerValidator;
    private readonly IValidator<Request.LoginRequest> _loginValidator;

    public Service(IJwtTokenService jwtTokenService, AppDbContext dbContext, ICacheService cacheService, IUnitOfWork unitOfWork, IValidator<Request.RegisterRequest> registerValidator, IValidator<Request.LoginRequest> loginValidator)
    {
        _jwtTokenService = jwtTokenService;
        _dbContext = dbContext;
        _cacheService = cacheService;
        _unitOfWork = unitOfWork;
        _registerValidator = registerValidator;
        _loginValidator = loginValidator;
    }

    public async Task Register(Request.RegisterRequest request, CancellationToken cancellationToken)
    {
        var validationResult = await _registerValidator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
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
    }

    public async Task<Response.LoginResponse> Login(Request.LoginRequest request, CancellationToken cancellationToken)
    {
        var validationResult = await _loginValidator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == request.Email, cancellationToken);
        if (user == null)
        {
            throw new UnauthorizedAccessException("Email hoặc Password không chính xác");
        }
        var clams = new List<Claim>
        {
            new Claim("UserId",  user.Id.ToString()),
            new Claim(ClaimTypes.Email, request.Email),
            new Claim(ClaimTypes.Role, user.Role.ToString()),
        };
        var accessToken = _jwtTokenService.GenerateAccessToken(clams);
        var refreshToken = _jwtTokenService.GenerateRefreshToken();
        var response = new Response.LoginResponse()
        {
            AccessToken =  accessToken,
            RefreshToken = refreshToken,
            RefreshTokenExpiryTime =  DateTime.Now.AddMinutes(5)
        };
        await _cacheService.SetAsync(user.Email, response, cancellationToken);
        return response;;
    }
}