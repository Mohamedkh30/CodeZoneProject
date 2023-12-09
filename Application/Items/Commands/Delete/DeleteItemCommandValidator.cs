using FluentValidation;

namespace CodeZoneProject.Application.Items.Commands.Delete
{
    public class DeleteItemCommandValidator:AbstractValidator<DeleteItemCommand>
    {
        public DeleteItemCommandValidator()
        {
            RuleFor(a => a.Id).NotEmpty();
        }
    }
}
