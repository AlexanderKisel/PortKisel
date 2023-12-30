using FluentValidation;
using PortKisel.Api.ModelsRequest.Vessel;
using PortKisel.Repositories.Contracts.Interface;

namespace PortKisel.Api.Validators.Vessel
{
    public class CreateVesselRequestValidator : AbstractValidator<CreateVesselRequest>
    {
        public CreateVesselRequestValidator(ICompanyPerReadRepository companyPerReadRepository)
        {
            RuleFor(vessel => vessel.Name)
                .NotNull()
                .NotEmpty()
                .WithMessage("Название судна не должно быть пустым");
            RuleFor(vessel => vessel.Description)
                .NotNull()
                .NotEmpty()
                .WithMessage("Описание судна не должно быть пустым");
            RuleFor(vessel => vessel.LoadCapacity)
                .NotNull()
                .NotEmpty()
                .WithMessage("Грузоподъемность судна не должна быть пустой");
            RuleFor(vessel => vessel.CompanyPerId)
                .NotNull()
                .NotEmpty()
                .WithMessage("Компания перевозчик не должна быть пустым")
                .MustAsync(async (id, cancellationToken) =>
                {
                    var companyPer = await companyPerReadRepository.AnyByIdAsync(id, cancellationToken);
                    return companyPer;
                })
                .WithMessage("Такого заказчика не существует");
        }
    }
}
