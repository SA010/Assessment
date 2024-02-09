using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Sag.Service.Companies.Infrastructure.EntityFramework;
using Sag.Framework.EntityFramework.Application.Common;
using Sag.Framework.EntityFramework.CrudService;
using Sag.Framework.EntityFramework.CrudService.Interfaces;
using Sag.Framework.EntityFramework.Domain.Common;
using Sag.Framework.EntityFramework.Extensions;
using Sag.Framework.EntityFramework.Specifications;
using Sag.Framework.ESB.Extensions;
using Sag.Framework.Extensions;
using Sag.Framework.Web.ExceptionHandlers.ApiExceptionHandler;
using Sag.Framework.Web.ExceptionHandlers.GlobalExceptionHandler;
using Sag.Framework.Web.ExceptionHandlers.NotFoundExceptionHandler;
using Sag.Framework.Web.ExceptionHandlers.SagExceptionHandler;
using Sag.Framework.Web.ExceptionHandlers.UpdateExceptionHandler;
using Sag.Framework.Web.ExceptionHandlers.ValidationExceptionHandler;
using Sag.Framework.Web.Extensions;
using Sag.Framework.Web.Versioning;
using Sag.Service.Companies.Application.Services.Interfaces;
using Sag.Service.Companies.Application.Services;
using Sag.Service.Companies.Application.Dtos;
using Sag.Service.Companies.Application.EventHandlers;
using Sag.Service.Companies.Domain;
using Sag.Service.Companies.Application.Specifications;
using FluentValidation;
using Sag.Service.Companies.Application.Validators.Generic.Interfaces;
using Sag.Service.Companies.Application.Validators.Generic;
using Sag.Service.Companies.Application.Validators;
using Sag.Service.Companies.Application.Mappings;
using System.Reflection;

namespace Sag.Service.Companies.Api
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
                .AddSagFramework<AddressMapping>(Configuration.GetSection("SagFramework"))
                .AddSagFrameworkWeb<Startup>(Configuration.GetSection("SagFramework"))
                .AddSagESB(Configuration)
                .AddSagDatabase<ApplicationDbContext>(options => options.UseSqlServer(
                    Configuration.GetConnectionString("SagCompany")!,
                    sqlServerOptions => sqlServerOptions.EnableRetryOnFailure(5, TimeSpan.FromSeconds(5), null)
                        .UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)
                        .MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)
                ));

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

            AddValidators(services);

            AddEventHandlers(services);

        }

        private static void AddServices(IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Startup).GetTypeInfo().Assembly));

            services.TryAddScoped<ICompanyService, CompanyService>();
        }

        private static void AddCrudServices(IServiceCollection services)
        {
            services.TryAddScoped<ICrudService<AuditEntry, AuditEntryDto>, CrudService<AuditEntry, AuditEntryDto, WithId<AuditEntry>>>();
            services.TryAddScoped<ICrudService<Company, CompanyDto>, CrudService<Company, CompanyDto, CompanyWithId>>();
        }

        private static void AddValidators(IServiceCollection services)
        {
            services.AddScoped<IGenericValidator, GenericValidator>();

            services.AddScoped<IValidator<CompanyDto>, CompanyDtoValidator>();
            services.AddScoped<IValidator<AddressDto>, AddressDtoValidator>();
            services.AddScoped<IValidator<ContactPersonDto>, ContactPersonDtoValidator>();
        }

        private static void AddEventHandlers(IServiceCollection services)
        {
            services.TryAddScoped<IEventHandler<Company, CompanyDto>, EventHandler<Company, CompanyDto>>();
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
                    options.RoutePrefix = string.Empty;  // Set Swagger UI at apps root
                }
            });
            app.UseRouting();

            app.UseSagCors();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
