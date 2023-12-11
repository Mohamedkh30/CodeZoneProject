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
                var result = await ValidateAndProcessCommand<CreateItemCommand, CreateItemCommandValidator>(command);
                if (result is ViewResult)
                    ViewBag.Categories = getCategorySelectList();
                return result;
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
                var result = await ValidateAndProcessCommand<UpdateItemCommand, UpdateItemCommandValidator>(command);
                if (result is ViewResult)
                    ViewBag.Categories = getCategorySelectList();
                return result;
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
            var result = await Mediator.Send(new DeleteItemCommand() { Id = id });
            if(result)
                return Ok();
            else
                return BadRequest();
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
