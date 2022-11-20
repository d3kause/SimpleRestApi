using Npgsql;
using SimpleRestApi.Common.Database.CodeValue;

namespace SimpleRestApi;

public sealed class Startup
{
    private readonly IConfiguration _configuration;
    
    public Startup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddHealthChecks();
        services.AddHttpContextAccessor();
        var connectionStringBuilder =
            new NpgsqlConnectionStringBuilder(_configuration.GetConnectionString("SimpleDataConnectionString"))
            {
                Password = _configuration["SimpleDataDbPassword"]
            };
        
        services.AddCodeValuesDb(connectionStringBuilder.ConnectionString, builder => builder.EnableRetryOnFailure());
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env,
        IHostApplicationLifetime applicationLifetime)
    {
        app.UseRouting();
        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.UseExceptionHandler("/rest/Error");
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapHealthChecks("/health");
            endpoints.MapControllers();
        });
        
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
    }
}