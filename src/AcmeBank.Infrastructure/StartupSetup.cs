using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AcmeBank.Persistence;

public static class StartupSetup
{
    public static void AddDbContext(this IServiceCollection services, string connectionString) =>
        services.AddDbContext<AcmeBankDbContext>(options =>
            options.UseSqlServer(connectionString, options =>
            options.MigrationsAssembly("AcmeBank.Persistence")));
}
