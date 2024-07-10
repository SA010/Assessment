using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Sag.Framework.Extensions;
using Sag.Framework.Web.ExceptionHandlers.ApiExceptionHandler;
using Sag.Framework.Web.ExceptionHandlers.GlobalExceptionHandler;
using Sag.Framework.Web.ExceptionHandlers.NotFoundExceptionHandler;
using Sag.Framework.Web.ExceptionHandlers.SagExceptionHandler;
using Sag.Framework.Web.ExceptionHandlers.UpdateExceptionHandler;
using Sag.Framework.Web.ExceptionHandlers.ValidationExceptionHandler;
using Sag.Framework.Web.Extensions;
using Sag.Service.Companies.Client.Extensions;
using System.Reflection;


namespace Sag.SampleBFF.Api.Api
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
            services
                .AddSagFramework<Startup>(Configuration.GetSection("SagFramework"))
                .AddSagFrameworkWeb<Startup>(Configuration.GetSection("SagFramework"));
            
            AddServices(services);
        }

        private void AddServices(IServiceCollection services)
        {
            services.AddSagCompaniesClient(Configuration);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseSagGlobalExceptionHandler();
                app.UseHsts();
            }

            app.UseSagExceptionHandler();
            app.UseSagNotFoundExceptionHandler();
            app.UseSagUpdateExceptionHandler();
            app.UseSagApiExceptionHandler();

            app.UseHttpsRedirection();
            app.UseResponseCompression();
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                var provider = app.ApplicationServices.GetRequiredService<IApiVersionDescriptionProvider>();

                foreach (var groupName in provider.ApiVersionDescriptions.Select(avd => avd.GroupName))
                {
                    options.SwaggerEndpoint($"/swagger/{groupName}/swagger.json", groupName.ToUpper());
                }
            });
            app.UseRouting();

            app.UseSagCors();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
