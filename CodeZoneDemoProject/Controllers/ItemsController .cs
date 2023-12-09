using CodeZoneProject.Application.Items.Commands.Create;
using CodeZoneProject.Application.Items.Commands.Delete;
using CodeZoneProject.Application.Items.Commands.Update;
using CodeZoneProject.Application.Items.Queries;
using CodeZoneProject.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace CodeZoneProject.Web.Controllers
{
    public class ItemsController : Controller
    {
        private IMediator _mediator;
        protected IMediator Mediator => _mediator ?? (_mediator = HttpContext.RequestServices.GetService<IMediator>());

        public async Task<ActionResult> Index()
        {
            ViewBag.Items = await Mediator.Send(new GetAllItemsQuery());
            return View();
        }

        public ActionResult Create()
        {
            ViewBag.Categories = getCategorySelectList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateItemCommand command)
        {
            try
            {
                await Mediator.Send(command);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public async Task<ActionResult> Edit(Guid id)
        {
            ViewBag.Categories = getCategorySelectList();
            var item = await Mediator.Send(new GetItemByIdQuery() { Id = id });
            return View(new UpdateItemCommand() 
            { 
                Id= item.Id,
                Name = item.Name,
                Category = item.Category
            });
        }

        [HttpPost]
        public async Task<ActionResult> Edit(UpdateItemCommand command)
        {
            try
            {
                await Mediator.Send(command);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public async Task<ActionResult> Delete(Guid id)
        {
            await Mediator.Send(new DeleteItemCommand() { Id = id});
            return RedirectToAction(nameof(Index));
        }

        #region Helper Methods
        private static SelectList getCategorySelectList()
        {
            var catergories = Enum.GetValues(typeof(ItemCategory)).Cast<ItemCategory>().Select(v => new SelectListItem
            {
                Text = v.ToString(),
                Value = ((int)v).ToString()
            }).ToList();

            return new SelectList(catergories, "Value", "Text");
        }
        #endregion
    }
}
