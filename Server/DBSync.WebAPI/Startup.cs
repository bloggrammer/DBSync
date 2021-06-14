using Dotmim.Sync.SqlServer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DBSync.WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            // [Required] Get a connection string for your server data source
            var sqlConnectionString = Configuration.GetConnectionString("SQLConnection");

            services.AddControllers();

            //  [Required] Mandatory to be able to handle multiple sessions
            services.AddMemoryCache();           

            // [Required] Add your database tables used for the sync process
            var tables = new string[] { "People" };

            // Add a SqlSyncProvider acting as the server hub
            services.AddSyncServer<SqlSyncProvider>(sqlConnectionString, tables);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
