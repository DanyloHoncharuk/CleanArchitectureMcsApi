using Microsoft.EntityFrameworkCore;
using UserService.API.Middlewares;
using UserService.Application.Interfaces;
using UserService.Infrastructure.Data;
using UserService.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<UserServiceDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService.Application.Services.UserService>();
builder.Services.AddScoped<IDbContextTransactionManager, DbContextTransactionManager>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies()); // auto use all mapping profiles, that inherited from Profile class

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSwaggerGateway", policy =>
    {
        policy
            .WithOrigins("https://localhost:7000")
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCors("AllowSwaggerGateway");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<ExceptionMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
