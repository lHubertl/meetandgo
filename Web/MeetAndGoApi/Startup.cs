using MeetAndGoApi.BusinessLayer.Contracts;
using MeetAndGoApi.BusinessLayer.DataRepositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MeetAndGoApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            RegisterDependencyContainer(services);
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
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }

        #region Private methods

        /// <summary>
        /// This method is for registering all dependency injection objects
        /// </summary>
        /// <param name="services">Dependency container service</param>
        private void RegisterDependencyContainer(IServiceCollection services)
        {
            services.AddScoped<IEventRepository, EventRepository>();
        }

        #endregion
    }
}
