using ExoCortex.Web.Framework;
using ExoCortex.Web.Framework.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ExoCortex.Web
{
    public enum StorageSolution
    {
        FireStore,
        MongoDB
    }
    public class Startup
    {
        //Remade to MongoDB but kept FireStore as a solution
        StorageSolution Storage = StorageSolution.MongoDB;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            if(Storage == StorageSolution.FireStore)
            {
                services.AddSingleton<IFirestoreFactory, FirestoreFactory>();
                services.AddSingleton<IInputStorage, FirestoreInputStorage>();
            }
            else if(Storage == StorageSolution.MongoDB)
            {
                services.AddSingleton<IInputStorage, MongoDBInputStorage>();
            }

           
            services.AddControllers();
            services.AddSpaStaticFiles(configuration =>
           {
               configuration.RootPath = "wwwroot";
           });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHttpsRedirection();
            }
            app.UseStaticFiles();
            app.UseSpaStaticFiles();
            app.UseRouting();

            //TODO: Insert simple authentication by
            /*
              app.Use(async (context, next) =>
        {
            // Do work that doesn't write to the Response.
            await next.Invoke();
            // Do logging or other work that doesn't write to the Response.
        });*/

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "wwwroot";
            });
        }
    }
}
