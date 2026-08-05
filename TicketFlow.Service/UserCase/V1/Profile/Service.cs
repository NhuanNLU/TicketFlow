using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using TicketFlow.Repository;
using TicketFlow.Repository.Abstractions;

namespace TicketFlow.Service.UserCase.V1.Profile;

public class Service: IService
{
    private readonly AppDbContext _dbContext;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IMapper _mapper;
    private readonly IValidator<Request.UpdateProfileRequest> _validator;

    public Service(AppDbContext dbContext, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, IMapper mapper, IValidator<Request.UpdateProfileRequest> validator)
    {
        _dbContext = dbContext;
        _unitOfWork = unitOfWork;
        _httpContextAccessor = httpContextAccessor;
        _mapper = mapper;
        _validator = validator;
    }

    public Task<Response.GetProfileResponse> GetProfile(CancellationToken cancellationToken)
    {
        var userId = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserId")?.Value;
        var guidId = Guid.Parse(userId!);
        //Dùng Dapper
        throw new NotImplementedException();
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