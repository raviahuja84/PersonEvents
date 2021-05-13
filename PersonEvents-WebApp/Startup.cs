using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PersonEvents_WebApp.ExtensionHelpers;
using PersonEvents_WebApp.Hubs;
using PersonEvents_WebApp.Repository.cs;
using PersonEvents_WebApp.SqlTableDependencies;

namespace PersonEvents_WebApp
{
    public class Startup
    {
        //private const string ConnectionString = @"Data Source=(LocalDb)\MSSQLLocalDB;Initial Catalog=PersonEventsDemoDB;User Id=demo;Password=demo;";
        private const string ConnectionString = @"Data Source=(LocalDb)\MSSQLLocalDB;Initial Catalog=PersonEventsDemoDB;Trusted_Connection=True";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddControllersWithViews();

            services.AddMvc();
            services.AddSignalR();

            // Dependency Injection
            services.AddDbContextFactory<PersonEventsContext>(ConnectionString);
            services.AddSingleton<IEventsRepository, EventsRepository>();
            services.AddSingleton<IDatabaseSubscription, EventsDatabaseSubscription>();
            //services.AddScoped<IHubContext<PersonEventsHub>, HubContext<PersonEventsHub>>();
            services.AddSingleton<PersonEventsHub, PersonEventsHub>();

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseCors("CorsPolicy");

            //app.UseHttpsRedirection();
            app.UseStaticFiles();


            app.UseSignalR(routes =>
            {
                routes.MapHub<PersonEventsHub>("/personevents");
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            app.UseSqlTableDependency<IDatabaseSubscription>(ConnectionString);

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World from Sever!");
            });

            //app.UseRouting();
            //app.UseAuthorization();
        }
    }
}
