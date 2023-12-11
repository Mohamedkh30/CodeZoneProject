using CodeZoneProject.Application.Interfaces;
using CodeZoneProject.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CodeZoneProject.Application.Stores.Commands.Create
{
    public class CreateStoreCommand : IRequest<Guid>
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public class Handler : IRequestHandler<CreateStoreCommand, Guid>
        {
            private readonly IContext _context;

            public Handler(IContext context)
            {
                _context = context;
            }
            public async Task<Guid> Handle(CreateStoreCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    var store = new Store
                    {
                        Name = request.Name,
                        Address = request.Address,
                        CreationDate = DateTime.Now,
                        Deleted = false
                    };
                    await _context.Stores.AddAsync(store, cancellationToken);
                    await _context.SaveChangesAsync(cancellationToken);
                    return store.Id;
                }
                catch (Exception ex)
                {
                    //error handling
                    throw;
                }
            }
        }
    }
}
