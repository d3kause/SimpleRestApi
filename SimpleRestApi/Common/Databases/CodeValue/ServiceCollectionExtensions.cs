using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure;
using SimpleRestApi.Common.Databases.CodeValue.Contracts;

namespace SimpleRestApi.Common.Databases.CodeValue;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCodeValuesDb(this IServiceCollection services,
        string connectionString,
        Action<NpgsqlDbContextOptionsBuilder> configureOptions)
    {
        Guard.NotNull(connectionString, nameof(connectionString));
        
        services.AddTransient<ICodeValueTypeDataStorage, CodeValueTypeDataStorage>();
        services.AddDbContext<CodeValueDbContext>(opt => opt.UseNpgsql(connectionString, configureOptions));

        return services;
    }
}