﻿using CodeZoneProject.Application.Items.Commands.Update;
using FluentValidation;

namespace CodeZoneProject.Application.Items.Commands.Create
{
    public class UpdateItemCommandValidator : AbstractValidator<UpdateItemCommand>
    {
        public UpdateItemCommandValidator()
        {
            RuleFor(a => a.Id).NotEmpty();

            RuleFor(a => a.Name).NotEmpty().WithMessage("Name is Required");

            RuleFor(a => a.Category).NotEmpty().WithMessage("Category is Required");
        }

    }
}
