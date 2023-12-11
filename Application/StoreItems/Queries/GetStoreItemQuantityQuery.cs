using CodeZoneProject.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CodeZoneProject.Application.StoreItems.Queries
{
    public class GetStoreItemQuantityQuery : IRequest<decimal>
    {
        public Guid ItemId { get; set; }
        public Guid StoreId { get; set; }
        public class Handler : IRequestHandler<GetStoreItemQuantityQuery, decimal>
        {
            private readonly IContext _context;
            public Handler(IContext context)
            {
                _context = context;
            }
            public async Task<decimal> Handle(GetStoreItemQuantityQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    if (request.ItemId != Guid.Empty && request.StoreId != Guid.Empty)
                    {
                        var quantity =  await _context.StoreItems.Where(s => s.ItemId == request.ItemId && s.StoreId == request.StoreId)
                                                .Select(s => s.Quantity)
                                                .FirstOrDefaultAsync(cancellationToken);
                        return quantity;
                    }
                    return 0M;

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
