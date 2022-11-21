using SimpleRestApi;
using SimpleRestApi.Common.Databases;
using SimpleRestApi.Common.Databases.CodeValue;

var webHost  = Host.CreateDefaultBuilder(args).ConfigureWebHostDefaults(webBuilder =>
    webBuilder.UseStartup<Startup>()).Build();

MigrateDatabase(webHost);

await webHost.RunAsync();


void MigrateDatabase(IHost webHost)
{
    webHost.WithMigratedDatabase<CodeValueDbContext>(); 
}
