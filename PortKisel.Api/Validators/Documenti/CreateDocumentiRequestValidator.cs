﻿using FluentValidation;
using PortKisel.Api.ModelsRequest.Documenti;
using PortKisel.Repositories.Contracts.Interface;

namespace PortKisel.Api.Validators.Documenti
{
    public class CreateDocumentiRequestValidator : AbstractValidator<CreateDocumentiRequest>
    {
        public CreateDocumentiRequestValidator(ICargoReadRepository cargoReadRepository,
            IVesselReadRepository vesselReadRepository,
            IStaffReadRepository staffReadRepository)
        {
            RuleFor(documenti => documenti.Number)
                .NotNull()
                .NotEmpty()
                .WithMessage("Номер документа не должен быть пустым");
            RuleFor(documenti => documenti.IssaedAt)
                .NotNull()
                .NotEmpty()
                .WithMessage("Время создания не должно быть пустым");
            RuleFor(documenti => documenti.CargoId)
                .NotNull()
                .NotEmpty()
                .WithMessage("Груз не должен быть пустым")
                .MustAsync(async (id, cancellationToken) =>
                {
                    var cargo = await cargoReadRepository.AnyByIdAsync(id, cancellationToken);
                    return cargo;
                })
                .WithMessage("Такого груза не существует");
            RuleFor(documenti => documenti.VesselId)
                .NotNull()
                .NotEmpty()
                .WithMessage("Судно не должно быть пустым")
                .MustAsync(async (id, cancellationToken) =>
                {
                    var vessel = await vesselReadRepository.AnyByIdAsync(id, cancellationToken);
                    return vessel;
                })
                .WithMessage("Такого судна не существует");
            RuleFor(documenti => documenti.StaffId)
                .NotNull()
                .NotEmpty()
                .WithMessage("Ответственный за груз не должен быть пустым")
                .MustAsync(async (id, cancellationToken) =>
                {
                    var staff = await staffReadRepository.AnyByIdAsync(id, cancellationToken);
                    return staff;
                })
                .WithMessage("Такого ответственного за груз не существует");
        }
    }
}
