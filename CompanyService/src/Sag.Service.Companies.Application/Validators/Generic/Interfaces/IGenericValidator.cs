using FluentValidation.Internal;
using FluentValidation.Results;

namespace Sag.Service.Companies.Application.Validators.Generic.Interfaces
{
    public interface IGenericValidator
    {
        ValidationResult Validate<T>(IValidationContext context);

        ValidationResult Validate<T>(T instance, Action<ValidationStrategy<T>> options);

        void ValidateAndThrow<T>(T instance);

        Task<ValidationResult> ValidateAsync<T>(IValidationContext context, CancellationToken cancellationToken = default);

        Task<ValidationResult> ValidateAsync<T>(T instance, Action<ValidationStrategy<T>> options, CancellationToken cancellationToken = default);

        Task ValidateAndThrowAsync<T>(T instance, CancellationToken cancellationToken = default);

        bool CanValidateInstancesOfType<T>(Type type);

        void CreateDescriptor<T>();
    }
}
