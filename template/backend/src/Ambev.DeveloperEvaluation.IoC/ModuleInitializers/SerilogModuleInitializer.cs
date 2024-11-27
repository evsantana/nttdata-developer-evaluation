using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace Ambev.DeveloperEvaluation.IoC.ModuleInitializers
{
    public class SerilogModuleInitializer
    {
        public void Initialize(WebApplicationBuilder builder)
        {
            var mongoDbConnectionString = builder.Configuration.GetConnectionString("MongoDb");
            var databaseName = "LogsDatabase";
            var collectionName = "SaleEventLogs";

            Serilog.Debugging.SelfLog.Enable(Console.Out);

            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Console() //Logs in console
                .WriteTo.Logger(lc => lc
                    .WriteTo.MongoDB(
                        $"{mongoDbConnectionString}/{databaseName}?authSource=admin",
                        collectionName: collectionName
                    )
                    .Filter.ByIncludingOnly(logEvent =>
                        logEvent.MessageTemplate.Text.Contains("@SaleEvent"))) //Only for SaleEvent logs
                .CreateLogger();

            //Replace default logger with Serilog
            builder.Host.UseSerilog();
        }
    }
}
