using CodeZoneProject.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CodeZoneProject.Application.Items.Commands.Delete
{
    public class DeleteItemCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public class Handler : IRequestHandler<DeleteItemCommand, bool>
        {
            private readonly IContext _context;
            public Handler(IContext context)
            {
                _context = context;
            }
            public async Task<bool> Handle(DeleteItemCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    
                    var item = await _context.Items.FirstOrDefaultAsync(i => i.Id== request.Id, cancellationToken);

                    if (item != null)
                    {
                        item.Deleted = true;
                        item.ModificationDate = DateTime.Now;

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
