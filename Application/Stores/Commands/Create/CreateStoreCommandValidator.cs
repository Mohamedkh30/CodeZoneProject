using CodeZoneProject.Application.Stores.Commands.Update;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeZoneProject.Application.Stores.Commands.Create
{
    public class CreateStoreCommandValidator : AbstractValidator<CreateStoreCommand>
    {
        public CreateStoreCommandValidator()
        {

            RuleFor(a => a.Name).NotEmpty().WithMessage("Name is Required");

            RuleFor(a => a.Address).NotEmpty().WithMessage("Address is Required");

        }
    }
}
