using FluentValidation;
using PortKisel.Api.ModelsRequest.Staff;

namespace PortKisel.Api.Validators.Staff
{
    public class CreateStaffRequestValidator : AbstractValidator<CreateStaffRequest>
    {
        public CreateStaffRequestValidator()
        {
            RuleFor(staff => staff.FIO)
                .NotNull()
                .NotEmpty()
                .WithMessage("ФИО работника не должно быть пустым");
            RuleFor(staff => staff.Post)
                .NotNull()
                .WithMessage("Тип работника не должен быть пустым");
        }
    }
}
