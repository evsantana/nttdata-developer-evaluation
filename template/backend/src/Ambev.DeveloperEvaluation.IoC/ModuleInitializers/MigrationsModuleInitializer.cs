using Ambev.DeveloperEvaluation.ORM;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.IoC.ModuleInitializers
{
    /// <summary>
    /// Entity Framework configurations
    /// </summary>
    public class MigrationsModuleInitializer
    {
        /// <summary>
        /// Executes dotnet ef update
        /// </summary>
        /// <param name="builder"></param>
        public void Initialize(WebApplication builder)
        {
            var services = builder.Services.CreateScope().ServiceProvider;

            try
            {
                var dataContext = services.GetRequiredService<DefaultContext>();
                dataContext.Database.Migrate();
            }
            catch (Exception ex)
            {
                //Logging
                var logger = services.GetRequiredService<ILogger<InfrastructureModuleInitializer>>();
                logger.LogError(ex, "An error occurred while applying database migrations.");
                throw;
            }
        }
    }
}
