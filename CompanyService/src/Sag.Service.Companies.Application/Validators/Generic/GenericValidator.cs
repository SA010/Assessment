using FluentValidation.Internal;
using FluentValidation.Results;
using Microsoft.Extensions.DependencyInjection;
using Sag.Service.Companies.Application.Validators.Generic.Interfaces;

namespace Sag.Service.Companies.Application.Validators.Generic
{
    public class GenericValidator : IGenericValidator
    {
        private readonly IServiceProvider _serviceProvider;

        public GenericValidator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public ValidationResult Validate<T>(IValidationContext context)
        {
            var validator = GetValidator<T>();
            return validator.Validate(context);
        }

        public ValidationResult Validate<T>(T instance, Action<ValidationStrategy<T>> options)
        {
            var validator = GetValidator<T>();
            return validator.Validate(instance, options);
        }

        public void ValidateAndThrow<T>(T instance)
        {
            var validator = GetValidator<T>();
            validator.ValidateAndThrow(instance);
        }

        public async Task<ValidationResult> ValidateAsync<T>(IValidationContext context, CancellationToken cancellationToken = default)
        {
            var validator = GetValidator<T>();
            return await validator.ValidateAsync(context, cancellationToken);
        }

        public async Task<ValidationResult> ValidateAsync<T>(T instance, Action<ValidationStrategy<T>> options, CancellationToken cancellationToken = default)
        {
            var validator = GetValidator<T>();
            return await validator.ValidateAsync(instance, options, cancellationToken);
        }

        public async Task ValidateAndThrowAsync<T>(T instance, CancellationToken cancellationToken = default)
        {
            var validator = GetValidator<T>();
            await validator.ValidateAndThrowAsync(instance, cancellationToken);
        }

        public bool CanValidateInstancesOfType<T>(Type type)
        {
            var validator = GetValidator<T>();
            return validator.CanValidateInstancesOfType(type);
        }

        public void CreateDescriptor<T>()
        {
            var validator = GetValidator<T>();
            validator.CreateDescriptor();
        }

        private IValidator<T> GetValidator<T>()
        {
            return _serviceProvider.GetRequiredService<IValidator<T>>();
        }
    }
}
