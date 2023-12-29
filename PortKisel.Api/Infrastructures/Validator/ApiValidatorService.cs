using FluentValidation;
using PortKisel.Api.Validators.Cargo;
using PortKisel.Api.Validators.CompanyPer;
using PortKisel.Api.Validators.CompanyZakazchik;
using PortKisel.Api.Validators.Documenti;
using PortKisel.Api.Validators.Staff;
using PortKisel.Api.Validators.Vessel;
using PortKisel.Repositories.Contracts.Interface;
using PortKisel.Services.Contracts.Exceptions;
using PortKisel.Shared;

namespace PortKisel.Api.Infrastructures.Validator
{
    internal sealed class ApiValidatorService : IApiValidatorService
    {
        private readonly Dictionary<Type, IValidator> validators = new Dictionary<Type, IValidator>();

        public ApiValidatorService(ICompanyPerReadRepository companyPerReadRepository, 
            ICompanyZakazchikReadRepository companyZakazchikReadRepository,
            IStaffReadRepository staffReadRepository,
            ICargoReadRepository cargoReadRepository,
            IVesselReadRepository vesselReadRepository)
        {
            Register<CreateCompanyPerRequestValidator>();
            Register<EditCompanyPerRequestValidator>();

            Register<CreateCompanyZakazchikRequestValidator>();
            Register<EditCompanyZakazchikRequestValidator>();

            Register<CreateStaffRequestValidator>();
            Register<EditStaffRequestValidator>();

            Register<CreateVesselRequestValidator>(companyPerReadRepository);
            Register<EditVesselRequestValidator>(companyPerReadRepository);

            Register<CreateCargoRequestValidator>(companyZakazchikReadRepository);
            Register<EditCargoRequestValidator>(companyZakazchikReadRepository);

            Register<CreateDocumentiRequestValidator>(cargoReadRepository, vesselReadRepository, staffReadRepository);
            Register<EditDocumentiRequestValidator>(cargoReadRepository, vesselReadRepository, staffReadRepository);

        }

        ///<summary>
        /// Регистрирует валидатор в словаре
        /// </summary>
        public void Register<TValidator>(params object[] constructorParams)
            where TValidator : IValidator
        {
            var validatorType = typeof(TValidator);
            var innerType = validatorType.BaseType?.GetGenericArguments()[0];
            if (innerType == null)
            {
                throw new ArgumentNullException($"Указанный валидатор {validatorType} должен быть generic от типа IValidator");
            }

            if (constructorParams?.Any() == true)
            {
                var validatorObject = Activator.CreateInstance(validatorType, constructorParams);
                if (validatorObject is IValidator validator)
                {
                    validators.TryAdd(innerType, validator);
                }
            }
            else
            {
                validators.TryAdd(innerType, Activator.CreateInstance<TValidator>());
            }
        }

        public async Task ValidateAsync<TModel>(TModel model, CancellationToken cancellationToken)
            where TModel : class
        {
            var modelType = model.GetType();
            if (!validators.TryGetValue(modelType, out var validator))
            {
                throw new InvalidOperationException($"Не найден валидатор для модели {modelType}");
            }

            var context = new ValidationContext<TModel>(model);
            var result = await validator.ValidateAsync(context, cancellationToken);

            if (!result.IsValid)
            {
                throw new PortValidationException(result.Errors.Select(x =>
                InvalidateItemModel.New(x.PropertyName, x.ErrorMessage)));
            }
        }
    }
}
