using Microsoft.Extensions.DependencyInjection;
using Todo.BusinessLogic.Interfaces;
using Todo.BusinessLogic.Services;
using Todo.BusinessLogic.Services.User;
using Todo.BusinessLogic.Services.User.Identity;

namespace Todo.BusinessLogic.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection ConfigureBusinessLogicServices(this IServiceCollection services)
    {
        services.AddTransient<ITaskService, TaskService>();
        services.AddTransient<IUserCommunicationService, UserCommunicationService>();
        services.AddTransient<IRoleService, RoleService>();
        //services.AddScoped<IIdentityService, Identit>();
        
        return services;
    }
}