using CodeZoneProject.Application.Interfaces;
using CodeZoneProject.Application.Items.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CodeZoneProject.Application.Items.Queries
{
    public class GetItemByIdQuery : IRequest<ItemDto>
    {
        public Guid Id { get; set; }
        public class Handler : IRequestHandler<GetItemByIdQuery, ItemDto>
        {
            private readonly IContext _context;
            private readonly ILogger _logger;
            public Handler(IContext context)
            {
                _context = context;
            }
            public async Task<ItemDto> Handle(GetItemByIdQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    return await _context.Items.Where(i => i.Id== request.Id && !i.Deleted)
                    .Select(i => new ItemDto
                    {
                        Id = i.Id,
                        Name = i.Name,
                        Category = i.Category,
                        CategoryName = i.Category.ToString()
                    })
                    .FirstOrDefaultAsync(cancellationToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An unexpected error occurred.");
                    throw;
                }
            }
        }
    }
}
