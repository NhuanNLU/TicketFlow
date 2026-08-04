using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using TicketFlow.Repository;
using TicketFlow.Repository.Abstractions.Message;
using TicketFlow.Repository.Abstractions.Shared;
using TicketFlow.Service.Caching;
using TicketFlow.Service.JwtService;

namespace TicketFlow.Service.UserCase.V1.Queries.Identity.Login;


public class LoginQueryHandler: ICommandHandler<LoginRequestQuery>
{
    private readonly IJwtTokenService _jwtTokenService;
    private readonly AppDbContext _dbContext;
    private readonly ICacheService  _cacheService;
    public LoginQueryHandler(IJwtTokenService jwtTokenService,  AppDbContext dbContext, ICacheService cacheService)
    {
        _jwtTokenService = jwtTokenService;
        _dbContext = dbContext;
        _cacheService = cacheService;
    }

    public async Task<Result> Handle(LoginRequestQuery request, CancellationToken cancellationToken)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == request.Email, cancellationToken);
        if (user == null)
        {
            return Result.Failure<LoginResponseQuery>(new Error("404", "User not found"));
        }
        var clams = new List<Claim>
        {
            new Claim("UserId",  user.Id.ToString()),
            new Claim(ClaimTypes.Email, request.Email),
            new Claim(ClaimTypes.Role, user.Role.ToString()),
        };
        var accessToken = _jwtTokenService.GenerateAccessToken(clams);
        var refreshToken = _jwtTokenService.GenerateRefreshToken();
        var response = new LoginResponseQuery()
        {
            AccessToken =  accessToken,
            RefreshToken = refreshToken,
            RefreshTokenExpiryTime =  DateTime.Now.AddMinutes(5)
        };
        await _cacheService.SetAsync(user.Email, response, cancellationToken);
        return Result.Success(response);
    }
}