using kojira.WebApi.Identity.Data;
using Microsoft.EntityFrameworkCore;

namespace kojira.WebApi.Extensions;

public static class MigrationExtensions
{ 
    public static async Task ApplyMigrationsAsync(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        using var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var services = scope.ServiceProvider;
        var logger = services.GetRequiredService<ILogger<ApplicationDbContext>>();

        try
        {
            var pendingMigrations = await context.Database.GetPendingMigrationsAsync();
            var count = pendingMigrations.Count();

            if (count > 0)
            {
                logger.LogInformation("Found {Count} pending migration(s) to apply. Proceeding with migration...", count);
                await context.Database.MigrateAsync();
                logger.LogInformation("Successfully applied {Count} pending migration(s).", count);
            }
            else
            {
                logger.LogInformation("No pending migrations found.");
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while migrating the database.");
        }
    }
}
