using System.Reflection;
using CompuTech.Application.Common.Behaviors;
using CompuTech.Application.Common.Interfaces;
using CompuTech.Application.MaintenanceSchedules.Services;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace CompuTech.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        services.AddScoped<IGenerateNextMaintenanceOrderService, GenerateNextMaintenanceOrderService>();

        return services;
    }
}
