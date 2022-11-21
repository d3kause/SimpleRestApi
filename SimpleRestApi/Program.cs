using Npgsql;
using SimpleRestApi;
using SimpleRestApi.Common.Databases;
using SimpleRestApi.Common.Databases.CodeValue;
using Serilog;
using Serilog.Events;

var webHost = Host.CreateDefaultBuilder(args)
    .ConfigureWebHostDefaults(webBuilder =>
        webBuilder.UseStartup<Startup>().UseSerilog(ConfigureLogger))
            .Build();

MigrateDatabase(webHost);

await webHost.RunAsync();




void ConfigureLogger(WebHostBuilderContext context, LoggerConfiguration loggerConfiguration)
{
    var logLevel = context.Configuration.GetValue<string>("Logging:MinimumLevel"); // read level from appsettings.json
    if (!Enum.TryParse<LogEventLevel>(logLevel, true, out var level))
    {
        level = LogEventLevel.Information; // or set default value
    }

    loggerConfiguration.Enrich
        .FromLogContext()
        .MinimumLevel.Is(level);
    
    var outputFormat = context.Configuration.GetValue<string>("Logging:OutputFormat");
    switch (outputFormat)
    {
        case "postgres":
            var connectionStringBuilder =
                new NpgsqlConnectionStringBuilder(context.Configuration.GetConnectionString("SimpleDataConnectionString"))
                {
                    Password = context.Configuration["SimpleDataDbPassword"]
                };
            loggerConfiguration.WriteTo.PostgreSQL(connectionStringBuilder.ConnectionString, "Logs", needAutoCreateTable: true);
            break;
        default:
            loggerConfiguration.WriteTo.Console(
                outputTemplate: "[{Timestamp:yy-MM-dd HH:mm:ss.sssZ}] [{Level:u4}] [{Environment}{Application}{SourceContext}] {Message:lj}{NewLine}{Exception}");
            break;
    }
}

void MigrateDatabase(IHost webHost)
{
    webHost.WithMigratedDatabase<CodeValueDbContext>(); 
}