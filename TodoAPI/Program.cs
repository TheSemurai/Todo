using Microsoft.OpenApi.Models;
using Todo.BusinessLogic.Infrastructure;
using Todo.BusinessLogic.Infrastructure.StartupConfiguration;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.ConfigureDbContext(config);
builder.Services.AddIdentityConfiguration();

builder.Services.ConfigureBusinessLogicServices();
builder.Services.ConfigureAuthentication(config);

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app
        .UseSwagger()
        .UseSwaggerUI();
}

app.UseRouting();

app
    .UseAuthorization()
    .UseAuthentication();

app.UseHttpsRedirection();
app.MapControllers();

app.UseCors(builder =>
    builder.WithOrigins("http://localhost:4200")  // or your Angular app URL
        .AllowAnyHeader()
        .AllowAnyMethod()
);

app.Run();