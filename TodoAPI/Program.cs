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
}

app.UseRouting();

app.UseCors("CorsPolicy");
app
    .UseAuthorization()
    .UseAuthentication();

app.UseHttpsRedirection();
app.MapControllers();


app.Run();