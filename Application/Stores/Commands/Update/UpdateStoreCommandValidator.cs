using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeZoneProject.Application.Stores.Commands.Update
{
    public class UpdateStoreCommandValidator : AbstractValidator<UpdateStoreCommand>
    {
        public UpdateStoreCommandValidator() {
            RuleFor(a => a.Id).NotEmpty();

            RuleFor(a => a.Name).NotEmpty().WithMessage("Name is Required");

            RuleFor(a => a.Address).NotEmpty().WithMessage("Address is Required");

        }
    }
}
