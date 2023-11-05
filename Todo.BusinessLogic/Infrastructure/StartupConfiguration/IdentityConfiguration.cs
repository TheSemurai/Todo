using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Todo.DataAccess.Configuration;
using Todo.DataAccess.Entities;

namespace Todo.BusinessLogic.Infrastructure.StartupConfiguration;

public static class IdentityConfiguration
{
    public static IServiceCollection AddIdentityConfiguration(this IServiceCollection services)
    {
        services
            .AddIdentity<User, Role>(opt =>
            {
                opt.Password.RequireDigit = true;
                opt.Password.RequireUppercase = true;
                opt.Password.RequireLowercase = true;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequiredLength = 8;

                opt.SignIn.RequireConfirmedAccount = false;
            })
            .AddEntityFrameworkStores<DataAccess.ApplicationContext>()
            .AddDefaultTokenProviders();
    

        return services;
    }
}