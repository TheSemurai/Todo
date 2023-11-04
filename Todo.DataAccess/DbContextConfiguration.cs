using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Todo.DataAccess;

public static class DbContextConfiguration
{
    public static IServiceCollection ConfigureDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContextPool<ApplicationContext>(
            (optionsBuilder) => optionsBuilder
                .UseSqlServer(@"Server=localhost;Database=TodoApp;Trusted_Connection=True;TrustServerCertificate=True")
        );

        return services;
    }
}

public class DesignTimeBMDbContext : IDesignTimeDbContextFactory<ApplicationContext>
{
    public ApplicationContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
        // pass your design time connection string here
        optionsBuilder.UseSqlServer(@"Server=localhost;Database=TodoApp;Trusted_Connection=True;TrustServerCertificate=True");
        return new ApplicationContext(optionsBuilder.Options);
        // 
    }
}