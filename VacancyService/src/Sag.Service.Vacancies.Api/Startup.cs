using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Sag.Service.Vacancies.Infrastructure.EntityFramework;
using Sag.Framework.EntityFramework.Application.Common;
using Sag.Framework.EntityFramework.CrudService;
using Sag.Framework.EntityFramework.CrudService.Interfaces;
using Sag.Framework.EntityFramework.Domain.Common;
using Sag.Framework.EntityFramework.Extensions;
using Sag.Framework.EntityFramework.Specifications;
using Sag.Framework.Extensions;
using Sag.Framework.Web.ExceptionHandlers.ApiExceptionHandler;
using Sag.Framework.Web.ExceptionHandlers.GlobalExceptionHandler;
using Sag.Framework.Web.ExceptionHandlers.NotFoundExceptionHandler;
using Sag.Framework.Web.ExceptionHandlers.SagExceptionHandler;
using Sag.Framework.Web.ExceptionHandlers.UpdateExceptionHandler;
using Sag.Framework.Web.ExceptionHandlers.ValidationExceptionHandler;
using Sag.Framework.Web.Extensions;
using Sag.Framework.Web.Versioning;
using System.Reflection;
using Sag.Framework.ESB.Extensions;
using Sag.Framework.Validation.Extensions;
using Sag.Service.Vacancies.Domain;
using Sag.Service.Vacancies.Application;
using Sag.Service.Vacancies.Application.Services;
using Sag.Service.Vacancies.Application.Interfaces;
using Sag.Service.Vacancies.Models.Dtos;

namespace Sag.Service.Vacancies.Api
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
            services
                .AddSagFramework<Startup>(Configuration.GetSection("SagFramework"))
                .AddSagFrameworkWeb<Startup>(Configuration.GetSection("SagFramework"))
                .AddSagESB(Configuration)
                .AddSagDatabase<ApplicationDbContext>(options => options.UseSqlServer(
                    Configuration.GetConnectionString("SagVacancy")!,
                    sqlServerOptions => sqlServerOptions.EnableRetryOnFailure(5, TimeSpan.FromSeconds(5), null)
                        .UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)
                        .MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)
                ));

            services.AddValidators<Anchor>();
            services.AddAutoMapper<Anchor>();

            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                var convention = new StandardApiVersionsConvention
                {
                    SupportedVersions = { new ApiVersion(1, 0) },
                };

                options.Conventions.Add(convention);
            });

            AddCrudServices(services);

            AddServices(services);

        }

        private static void AddServices(IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Startup).GetTypeInfo().Assembly));

            services.AddScoped<IVacancyService, VacancyService>();

            services.AddScoped<IUpdateVacancyByEventService, UpdateVacancyByEventService>();
        }

        private static void AddCrudServices(IServiceCollection services)
        {
            services.TryAddScoped<ICrudService<AuditEntry, AuditEntryDto>, CrudService<AuditEntry, AuditEntryDto, WithId<AuditEntry>>>();
            services.TryAddScoped<ICrudService<Vacancy, VacancyDto>, CrudService<Vacancy, VacancyDto, WithId<Vacancy>>>();
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
                // Uncomment on Prod. app.UseHsts();
            }

            app.UseSagExceptionHandler();
            app.UseSagNotFoundExceptionHandler();
            app.UseSagUpdateExceptionHandler();
            app.UseSagValidationExceptionHandler();
            app.UseSagApiExceptionHandler();

            app.UseResponseCompression();
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                var provider = app.ApplicationServices.GetRequiredService<IApiVersionDescriptionProvider>();

                foreach (var groupName in provider.ApiVersionDescriptions.Select(avd => avd.GroupName))
                {
                    options.SwaggerEndpoint($"/swagger/{groupName}/swagger.json", groupName.ToUpper());
                    options.RoutePrefix = string.Empty;   // Set Swagger UI at apps root
                }
            });
            app.UseRouting();

            app.UseSagCors();

            // Uncomment on Prod. 
            // app.UseAuthentication();
            // app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
