using FluentValidation;
using PortKisel.Api.ModelsRequest.CompanyZakazchik;

namespace PortKisel.Api.Validators.CompanyZakazchik
{
    public class EditCompanyZakazchikRequestValidator : AbstractValidator<EditCompanyZakazhikRequest>
    {
        public EditCompanyZakazchikRequestValidator()
        {
            RuleFor(companyZakazchik => companyZakazchik.Id)
                .NotNull()
                .NotEmpty()
                .WithMessage("Id компании не должно пустым");
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
