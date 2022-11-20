using SimpleRestApi;
using SimpleRestApi.Common.Database;
using SimpleRestApi.Common.Database.CodeValue;

var webHost  = Host.CreateDefaultBuilder(args).ConfigureWebHostDefaults(webBuilder =>
    webBuilder.UseStartup<Startup>()).Build();

MigrateDatabase(webHost);

await webHost.RunAsync();


void MigrateDatabase(IHost webHost)
{
    webHost.WithMigratedDatabase<CodeValueDbContext>(); 
}
