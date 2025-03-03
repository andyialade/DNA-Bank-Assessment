using DNABank.Application.Persistence;
using DNABank.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DNABank.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddDbContext<DNABankDbContext>(opts => opts.UseInMemoryDatabase("DnaBankDb"));
        services.AddScoped<IDNABankDbContext, DNABankDbContext>();

        return services;
    }
}
