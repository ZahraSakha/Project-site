using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace SakhaSite.Extensions
{
    public static class IApplicationBuilderExtensions
    {
        public static void MigrateDatabase<TContext>(this IApplicationBuilder app) where TContext : DbContext
        {
            using (var scope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                using (var context = scope.ServiceProvider.GetService<TContext>())
                {
                    context.Database.Migrate();
                }
            }
        }
    }
}
