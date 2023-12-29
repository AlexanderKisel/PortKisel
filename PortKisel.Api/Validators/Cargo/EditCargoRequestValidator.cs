﻿using FluentValidation;
using PortKisel.Api.ModelsRequest.Cargo;
using PortKisel.Repositories.Contracts.Interface;

namespace PortKisel.Api.Validators.Cargo
{
    public class EditCargoRequestValidator : AbstractValidator<EditCargoRequest>
    {
        public EditCargoRequestValidator(ICompanyZakazchikReadRepository companyZakazchikReadRepository)
        {
            RuleFor(cargo => cargo.Id)
                .NotNull()
                .NotEmpty()
                .WithMessage("Id не должно быть пустым или null");
            RuleFor(cargo => cargo.Name)
                .NotNull()
                .NotEmpty()
                .WithMessage("Название груза не должно быть пустым");
            RuleFor(cargo => cargo.Description)
                .NotNull()
                .NotEmpty()
                .WithMessage("Описание груза не должно быть пустым");
            RuleFor(cargo => cargo.Weight)
                .NotNull()
                .NotEmpty()
                .WithMessage("Масса груза не должна быть пустой");
            RuleFor(cargo => cargo.CompanyZakazchikId)
                .NotNull()
                .NotEmpty()
                .WithMessage("Компания заказчик не должна быть пустым")
                .MustAsync(async (id, cancellationToken) =>
                {
                    var companyZakazchik = await companyZakazchikReadRepository.GetByIdAsync(id, cancellationToken);
                    return companyZakazchik != null;
                })
                .WithMessage("Такого заказчика не существует");
        }
    }
}
