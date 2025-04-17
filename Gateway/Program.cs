var builder = WebApplication.CreateBuilder(args);

builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("https://localhost:7051/swagger/v1/swagger.json", "AuthService - 1.0");
    options.SwaggerEndpoint("https://localhost:7035/swagger/v1/swagger.json", "UserService - 1.0");
});

app.MapReverseProxy();

app.Run();