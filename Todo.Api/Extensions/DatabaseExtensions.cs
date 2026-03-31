using Microsoft.EntityFrameworkCore;
using Todo.Infrastructure.Data;

namespace Todo.Api.Extensions
{
    public static class DatabaseExtensions
    {
        /// <summary>
        /// Aplica as migrations e cria o banco de dados automaticamente
        /// </summary>
        public static async Task ApplyMigrationsAsync(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                
                try
                {
                    // Aplicar migrations pendentes
                    await dbContext.Database.MigrateAsync();
                    
                    app.Logger.LogInformation("✅ Database migrations applied successfully");
                }
                catch (Exception ex)
                {
                    app.Logger.LogError(ex, "❌ An error occurred while applying migrations");
                    throw;
                }
            }
        }
    }
}
