using CodeZoneProject.Application.Items.Commands.Create;
using CodeZoneProject.Application.Items.Commands.Delete;
using CodeZoneProject.Application.Items.Commands.Update;
using CodeZoneProject.Application.Items.Queries;
using CodeZoneProject.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CodeZoneProject.Web.Controllers
{
    public class ItemsController : BaseController
    {
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
            if(item !=null)
                return View(new UpdateItemCommand()
                {
                    Id = item.Id,
                    Name = item.Name,
                    Category = item.Category
                });

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<ActionResult> Edit(UpdateItemCommand command)
        {
            try
            {
                await Execute(command, "Update Item Command");
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ViewBag.Categories = getCategorySelectList();
                return View(command);
            }
        }

        [HttpGet]
        public async Task<ActionResult> Delete(Guid id)
        {
            await Mediator.Send(new DeleteItemCommand() { Id = id });
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
