using FluentValidation;
using Microsoft.EntityFrameworkCore;
using TicketFlow.API.Extensions;
using TicketFlow.API.Middleware;
using TicketFlow.Repository;
using TicketFlow.Repository.Abstractions;
using TicketFlow.Service.Caching;
using TicketFlow.Service.Dapper;
using TicketFlow.Service.Mapper;
using JwtTokenService = TicketFlow.Service.JwtService;
using Identity = TicketFlow.Service.UserCase.V1.Identity;
using Profile = TicketFlow.Service.UserCase.V1.Profile;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerServices();
builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();
builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddRedisService(builder.Configuration);
builder.Services.AddTransient<JwtTokenService.IJwtTokenService, JwtTokenService.JwtTokenService>();
builder.Services.AddTransient<ICacheService, CacheService>();
builder.Services.AddTransient<ExceptionHandlingMiddleware>();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddAutoMapper(typeof(ServiceProfile).Assembly);
builder.Services.AddValidatorsFromAssembly(typeof(Identity.Validator.LoginRequestValidator).Assembly);
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IDapperContext, DapperContext>();
builder.Services.AddScoped<Profile.IService, Profile.Service>();
builder.Services.AddScoped<Identity.IService, Identity.Service>();
var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();