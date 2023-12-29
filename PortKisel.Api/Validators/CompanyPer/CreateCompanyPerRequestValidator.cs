using FluentValidation;
using PortKisel.Api.ModelsRequest.CompanyPer;

namespace PortKisel.Api.Validators.CompanyPer
{
    public class CreateCompanyPerRequestValidator : AbstractValidator<CreateCompanyPerRequest>
    {
        public CreateCompanyPerRequestValidator()
        {
            RuleFor(companyPer => companyPer.Name)
                .NotNull()
                .NotEmpty()
                .WithMessage("Название компании не должно пустым");
            RuleFor(companyPer => companyPer.Description)
                .NotNull()
                .NotEmpty()
                .WithMessage("Описание компании не должно пустым");
        }
    }
}
