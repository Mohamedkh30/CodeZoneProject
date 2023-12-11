using CodeZoneProject.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CodeZoneProject.Application.Stores.Commands.Update
{
    public class UpdateStoreCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public class Handler : IRequestHandler<UpdateStoreCommand, bool>
        {
            private readonly IContext _context;
            public Handler(IContext context)
            {
                _context = context;
            }
            public async Task<bool> Handle(UpdateStoreCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    var store = await _context.Stores.FirstOrDefaultAsync(s => s.Id == request.Id);

                    if (store != null)
                    {
                        store.Name = request.Name;
                        store.Address = request.Address;
                        store.ModificationDate = DateTime.Now;

                        await _context.SaveChangesAsync(cancellationToken);
                        return true;
                    }
                    return false;
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
