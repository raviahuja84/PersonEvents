using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using PersonEvents_WebApp.Hubs;
using PersonEvents_WebApp.SqlTableDependencies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonEvents_WebApp.ExtensionHelpers
{
    public static class UseSqlTableDependencyHelpers
    {
        public static void UseSqlTableDependency<T>(this IApplicationBuilder services, string connectionString) where T : IDatabaseSubscription
        {
            var serviceProvider = services.ApplicationServices;
            var subscription = serviceProvider.GetRequiredService<T>();
            subscription.Configure(connectionString);
        }
    }
}
