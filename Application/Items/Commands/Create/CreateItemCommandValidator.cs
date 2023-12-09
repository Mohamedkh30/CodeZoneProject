using FluentValidation;

namespace CodeZoneProject.Application.Items.Commands.Create
{
    public class CreateItemCommandValidator : AbstractValidator<CreateItemCommand>
    {
        public CreateItemCommandValidator()
        {

            RuleFor(a => a.Name).NotEmpty().WithMessage("Name is Required");

            RuleFor(a => a.Category).NotEmpty().WithMessage("Category is Required");
        }
    }
}
