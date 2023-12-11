using CodeZoneProject.Application.Interfaces;
using CodeZoneProject.Application.Items.Commands.Delete;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeZoneProject.Application.Stores.Commands.Delete
{
    public class DeleteStoreCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public class Handler : IRequestHandler<DeleteStoreCommand, bool>
        {
            private readonly IContext _context;
            public Handler(IContext context)
            {
                _context = context;
            }
            public async Task<bool> Handle(DeleteStoreCommand request, CancellationToken cancellationToken)
            {
                try
                {

                    var store = await _context.Stores.FirstOrDefaultAsync(i => i.Name == "test", cancellationToken);

                    if (store != null)
                    {
                        store.Deleted = true;
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
