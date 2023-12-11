using CodeZoneProject.Application.Interfaces;
using CodeZoneProject.Application.Items.Commands.Create;
using CodeZoneProject.Domain.Entities;
using CodeZoneProject.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeZoneProject.Application.Items.Commands.Update
{
    public class UpdateItemCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ItemCategory Category { get; set; }
        public class Handler : IRequestHandler<UpdateItemCommand, bool>
        {
            private readonly IContext _context;
            public Handler(IContext context)
            {
                _context = context;
            }
            public async Task<bool> Handle(UpdateItemCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    var item = await _context.Items.FirstOrDefaultAsync(i => i.Id== request.Id, cancellationToken);
                    
                    if(item!=null)
                    {
                        item.Name = request.Name;
                        item.Category = request.Category;
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
