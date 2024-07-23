using Microsoft.EntityFrameworkCore;
using skinet.Data;
using skinet.Repositories;

namespace skinet
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //Controller and Data Services
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddDbContext<StoreContext>(opt =>
            {
                opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddSwaggerGen();

            //Repositories
            builder.Services.AddScoped<IProductRepository, ProductRepository>();

            //Build App
            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
                    c.RoutePrefix = string.Empty;
                });
            }

            app.MapControllers();

            //Database Migrations
            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            var context = services.GetRequiredService<StoreContext>();
            var logger = services.GetRequiredService<ILogger<Program>>();

            try
            {
                await context.Database.MigrateAsync();
                await StoreContextSeed.SeedAsynch(context);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occured during migration");
            }

            app.Run();
        }
    }
}
