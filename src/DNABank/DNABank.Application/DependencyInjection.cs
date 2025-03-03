using DNABank.Application.Factory;
using DNABank.Application.Features.Accounts;
using DNABank.Application.Helper;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace DNABank.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        var applicationAssembly = Assembly.GetExecutingAssembly();
        services.AddValidatorsFromAssembly(applicationAssembly).AddFluentValidationAutoValidation();

        services.AddScoped<IAccountFactory, AccountFactory>();
        services.AddScoped<IAccountHelper, AccountHelper>();
        services.AddScoped<IAccountService, AccountService>();

        return services;
    }
}
