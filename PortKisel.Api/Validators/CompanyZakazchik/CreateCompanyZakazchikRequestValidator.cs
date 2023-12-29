using FluentValidation;
using PortKisel.Api.ModelsRequest.CompanyZakazchik;

namespace PortKisel.Api.Validators.CompanyZakazchik
{
    public class CreateCompanyZakazchikRequestValidator : AbstractValidator<CreateCompanyZakazhikRequest>
    {
        public CreateCompanyZakazchikRequestValidator()
        {
            RuleFor(companyZakazchik => companyZakazchik.Name)
                .NotNull()
                .NotEmpty()
                .WithMessage("Название компании не должно пустым");
            RuleFor(companyZakazchik => companyZakazchik.Description)
                .NotNull()
                .NotEmpty()
                .WithMessage("Описание компании не должно пустым");
        }
    }
}
