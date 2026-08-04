using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TicketFlow.API.Extensions;
using TicketFlow.API.Middleware;
using TicketFlow.Repository;
using TicketFlow.Repository.Abstractions;
using TicketFlow.Service.Behaviors;
using TicketFlow.Service.Caching;
using TicketFlow.Service.Mapper;
using TicketFlow.Service.UserCase.V1.Commands.Identity.Register;
using JwtTokenService = TicketFlow.Service.JwtService;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddRedisService(builder.Configuration);
builder.Services.AddTransient<JwtTokenService.IJwtTokenService, JwtTokenService.JwtTokenService>();
builder.Services.AddTransient<ICacheService, CacheService>();
builder.Services.AddTransient<ExceptionHandlingMiddleware>();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(RegisterCommand).Assembly));
builder.Services.AddAutoMapper(typeof(ServiceProfile).Assembly);
builder.Services.AddValidatorsFromAssembly(typeof(RegisterCommand).Assembly);
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
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