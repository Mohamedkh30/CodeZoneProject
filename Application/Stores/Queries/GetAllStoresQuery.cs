using CodeZoneProject.Application.Interfaces;
using CodeZoneProject.Application.Stores.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CodeZoneProject.Application.Stores.Queries
{
    public class GetAllStoresQuery : IRequest<List<StoreDto>>
    {
        public class Handler : IRequestHandler<GetAllStoresQuery, List<StoreDto>>
        {
            private readonly IContext _context;
            public Handler(IContext context)
            {
                _context = context;
            }
            public async Task<List<StoreDto>> Handle(GetAllStoresQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var stores = await _context.Stores.Where(i => !i.Deleted)
                    .Select(s => new StoreDto
                    {
                        Id = s.Id,
                        Name = s.Name,
                        Address = s.Address,
                    })
                    .ToListAsync(cancellationToken);
                    return stores;
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
