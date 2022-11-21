using Microsoft.AspNetCore.HttpLogging;
using Microsoft.Net.Http.Headers;
using Npgsql;
using Serilog;
using SimpleRestApi.Common.Databases.CodeValue;

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
        services.AddHttpLogging(logging =>
        {
            logging.LoggingFields = HttpLoggingFields.All;
            logging.RequestHeaders.Add(HeaderNames.Accept);
            logging.RequestHeaders.Add(HeaderNames.ContentType);
            logging.RequestHeaders.Add(HeaderNames.ContentDisposition);
            logging.RequestHeaders.Add(HeaderNames.ContentEncoding);
            logging.RequestHeaders.Add(HeaderNames.ContentLength);

            logging.MediaTypeOptions.AddText("application/json");
            logging.MediaTypeOptions.AddText("multipart/form-data");

            logging.RequestBodyLogLimit = 4096;
            logging.ResponseBodyLogLimit = 4096;
        });
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
        app.UseHttpLogging().UseSerilogRequestLogging();
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