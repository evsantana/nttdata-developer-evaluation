using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Ambev.DeveloperEvaluation.ORM.Configuration
{
    public static class Database
    {
        public static void StartMigration(this WebApplication app)
        {
            var services = app.Services.CreateScope().ServiceProvider;
            var dataContext = services.GetRequiredService<DefaultContext>();
            dataContext.Database.Migrate();
        }
    }
}
