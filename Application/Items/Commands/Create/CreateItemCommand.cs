using CodeZoneProject.Application.Interfaces;
using CodeZoneProject.Domain.Entities;
using CodeZoneProject.Domain.Enums;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CodeZoneProject.Application.Items.Commands.Create
{
    public class CreateItemCommand : IRequest<Guid>
    {
        public string Name { get; set; }
        public ItemCategory Category { get; set; }
        public class Handler : IRequestHandler<CreateItemCommand, Guid>
        {
            private readonly IContext _context;
            public Handler(IContext context)
            {
                _context = context;
            }
            public async Task<Guid> Handle(CreateItemCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    var item = new Item
                    {
                        Name = request.Name,
                        Category = request.Category,
                        CreationDate = DateTime.Now,
                        Deleted = false
                    };
                    await _context.Items.AddAsync(item, cancellationToken);
                    await _context.SaveChangesAsync(cancellationToken);
                    return item.Id;
                }
                catch (Exception ex)
                {
                    //error handling
                    throw;
                }
            }
        }

        public class CreateItemCommandValidator : AbstractValidator<CreateItemCommand>
        {
            public CreateItemCommandValidator()
            {

                RuleFor(a => a.Name).NotEmpty().WithMessage("Name is Required");

                RuleFor(a => a.Category).NotEmpty().WithMessage("Category is Required");
            }
        }
    }
}
