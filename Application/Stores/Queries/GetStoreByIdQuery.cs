using CodeZoneProject.Application.Interfaces;
using CodeZoneProject.Application.Stores.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CodeZoneProject.Application.Stores.Queries
{
    public class GetStoreByIdQuery : IRequest<StoreDto>
    {
        public Guid Id { get; set; }
        public class Handler : IRequestHandler<GetStoreByIdQuery, StoreDto>
        {
            private readonly IContext _context;
            public Handler(IContext context)
            {
                _context = context;
            }
            public async Task<StoreDto> Handle(GetStoreByIdQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    return await _context.Stores.Where(i => i.Id == request.Id && !i.Deleted)
                    .Select(s => new StoreDto
                    {
                        Id = s.Id,
                        Name = s.Name,
                        Address = s.Address,
                    })
                    .FirstOrDefaultAsync(cancellationToken);
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
