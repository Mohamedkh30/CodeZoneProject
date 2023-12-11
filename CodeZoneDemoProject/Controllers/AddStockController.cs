using CodeZoneProject.Application.Items.Queries;
using CodeZoneProject.Application.Stores.Queries;
using CodeZoneProject.Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CodeZoneProject.Web.Controllers
{
    public class AddStockController : BaseController
    {
        public async Task<IActionResult> Index()
        {
            var items = await Mediator.Send(new GetAllItemsQuery());
            var stores = await Mediator.Send(new GetAllStoresQuery());
            ViewBag.Items = new SelectList(items, "Id", "Name");
            ViewBag.Stores = new SelectList(stores, "Id", "Name");
            return View();
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
