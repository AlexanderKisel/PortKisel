using FluentValidation;
using PortKisel.Api.ModelsRequest.Staff;

namespace PortKisel.Api.Validators.Staff
{
    public class EditStaffRequestValidator : AbstractValidator<EditStaffRequest>
    {
        public EditStaffRequestValidator()
        {
            RuleFor(staff => staff.Id)
                .NotEmpty()
                .NotNull()
                .WithMessage("Id не должно быть пустым или null");
            RuleFor(staff => staff.FIO)
                .NotNull()
                .NotEmpty()
                .WithMessage("ФИО работника не должно быть пустым");
            RuleFor(staff => staff.Post)
                .NotNull()
                .WithMessage("Тип работниуа не должен быть пустым");
        }
    }
}
