using Microsoft.EntityFrameworkCore;

namespace SimpleRestApi.Common.Database;

public static class WebHostExtensions
{
    public static void WithMigratedDatabase<TContext>(this IHost host) where TContext : DbContext
    {
        using var scope = host.Services.CreateScope();
        using var service = scope.ServiceProvider.GetRequiredService<TContext>();

        service.Database.Migrate();
    }
}