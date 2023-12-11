using CodeZoneProject.Application.Interfaces;
using CodeZoneProject.Application.StoreItems.Queries;
using CodeZoneProject.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeZoneProject.Application.StoreItems.Commands
{
    public class UpdateStoreItemQuantityCommand : IRequest<bool>
    {
        public Guid ItemId { get; set; }
        public Guid StoreId { get; set; }
        public decimal Quantity { get; set; }
        public class Handler : IRequestHandler<UpdateStoreItemQuantityCommand, bool>
        {
            private readonly IContext _context;
            public Handler(IContext context)
            {
                _context = context;
            }
            public async Task<bool> Handle(UpdateStoreItemQuantityCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    if (request.ItemId != Guid.Empty && request.StoreId != Guid.Empty)
                    {
                        var storeItems = await _context.StoreItems.FirstOrDefaultAsync(s => s.ItemId == request.ItemId && s.StoreId == request.StoreId, cancellationToken);
                        if (storeItems is null)
                        {
                            storeItems = new StoreItem
                            {
                                ItemId = request.ItemId,
                                StoreId = request.StoreId,
                                Quantity = request.Quantity < 0 ? 0 : request.Quantity,
                            };
                            await _context.StoreItems.AddAsync(storeItems, cancellationToken);
                        }
                        else
                        {
                            storeItems.Quantity = request.Quantity + storeItems.Quantity < 0 ? 0 : request.Quantity + storeItems.Quantity;
                        }
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
