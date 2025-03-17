using Microsoft.EntityFrameworkCore;
using AuthService.Application.Services;
using AuthService.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AuthServiceDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<UserPasswordService>();
builder.Services.AddSingleton<JwtService>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies()); // auto use all mapping profiles, that enherited from Profile class

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
