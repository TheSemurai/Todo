using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Todo.DataAccess;
using Todo.DataAccess.Configuration;
using Todo.DataAccess.Entities;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.ConfigureDbContext(builder.Configuration);
//builder.Services.AddIdentityConfiguration();

builder.Services.AddDbContext<ApplicationContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

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