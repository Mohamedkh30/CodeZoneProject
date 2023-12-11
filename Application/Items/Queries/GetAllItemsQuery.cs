using CodeZoneProject.Application.Interfaces;
using CodeZoneProject.Application.Items.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CodeZoneProject.Application.Items.Queries
{
    public class GetAllItemsQuery : IRequest<List<ItemDto>>
    {
        public class Handler : IRequestHandler<GetAllItemsQuery, List<ItemDto>>
        {
            private readonly IContext _context;
            public Handler(IContext context)
            {
                _context = context;
            }
            public async Task<List<ItemDto>> Handle(GetAllItemsQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var items = await _context.Items.Where(i => !i.Deleted)
                    .Select(i => new ItemDto
                    {
                        Id = i.Id,
                        Name = i.Name,
                        Category = i.Category,
                        CategoryName = i.Category.ToString()
                    })
                    .ToListAsync(cancellationToken);
                    return items;
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

