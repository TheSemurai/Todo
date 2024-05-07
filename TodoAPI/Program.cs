using Microsoft.EntityFrameworkCore;
using Todo.BusinessLogic.Infrastructure;
using Todo.BusinessLogic.Infrastructure.StartupConfiguration;
using Todo.DataAccess;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.ConfigureDbContext(config);
builder.Services.AddIdentityConfiguration();

builder.Services.ConfigureBusinessLogicServices();
builder.Services.ConfigureAuthentication(config);

builder.Services.AddCors(o => o.AddPolicy("CorsPolicy", builder =>
{
    builder
        .WithOrigins("http://localhost:5173") // local front-end port
        .AllowAnyMethod()
        .AllowAnyHeader();
}));

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app
        .UseSwagger()
        .UseSwaggerUI();

    using var serviceScope = app.Services.CreateScope();
    using var dbContext = serviceScope.ServiceProvider.GetService<ApplicationContext>();
    // using var dbContext = serviceScope.ServiceProvider.GetDbContext();
    dbContext?.Database.Migrate();
}

app.UseRouting();

app.UseCors("CorsPolicy");
app
    .UseAuthorization()
    .UseAuthentication();

app.UseHttpsRedirection();
app.MapControllers();


app.Run();