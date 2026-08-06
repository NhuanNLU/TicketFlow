using AutoMapper;
using Dapper;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using TicketFlow.Repository;
using TicketFlow.Repository.Abstractions;
using TicketFlow.Repository.Entities;
using TicketFlow.Service.Dapper;

namespace TicketFlow.Service.UserCase.V1.Profile;

public class Service: IService
{
    private readonly AppDbContext _dbContext;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IMapper _mapper;
    private readonly IValidator<Request.UpdateProfileRequest> _validator;
    private readonly IDapperContext _dapperContext;

    public Service(AppDbContext dbContext, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, IMapper mapper, IValidator<Request.UpdateProfileRequest> validator, IDapperContext dapperContext)
    {
        _dbContext = dbContext;
        _unitOfWork = unitOfWork;
        _httpContextAccessor = httpContextAccessor;
        _mapper = mapper;
        _validator = validator;
        _dapperContext = dapperContext;
    }

    public async Task<Response.GetProfileResponse> GetProfile(CancellationToken cancellationToken)
    {
        var userId = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserId")?.Value;
        var guidId = Guid.Parse(userId!);
        //Dùng Dapper
        var sql = 
            @"select u.""Username"", u.""Email"", u.""Avatar"", u.""Gender"", u.""PhoneNumber"", u.""Address"", u.""Bio"", u.""DateOfBirth"" 
                from ""Users"" u 
                where u.""Id"" = @UserId";
        using var connection = _dapperContext.CreateConnection();
        var user = await connection.QueryFirstOrDefaultAsync<User>(sql, new  { UserId = guidId });
        var response = _mapper.Map<Response.GetProfileResponse>(user);
        return response;
    }

    public async Task UpdateProfile(Request.UpdateProfileRequest request, CancellationToken cancellationToken)
    {
        var userId = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserId")?.Value;
        var guidId = Guid.Parse(userId!);
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == guidId, cancellationToken);
        if (user == null)
        {
            throw new KeyNotFoundException();
        }
        _mapper.Map(request, user);
        await _unitOfWork.CommitAsync(cancellationToken);
    }
}