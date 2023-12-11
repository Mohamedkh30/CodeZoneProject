using CodeZoneProject.Application.Items.Queries;
using CodeZoneProject.Application.StoreItems.Commands;
using CodeZoneProject.Application.StoreItems.Queries;
using CodeZoneProject.Application.Stores.Queries;
using CodeZoneProject.Domain.Entities;
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

        #region APIs
        public async Task<IActionResult> GetStoreItemQuantity(Guid storeId, Guid itemId)
        {
            return await Execute(new GetStoreItemQuantityQuery() { ItemId = itemId , StoreId = storeId }, "GetStoreItemQuantityQuery");
        }
        [HttpPost]
        public async Task<IActionResult> UpdateStoreItemQuantity([FromBody]UpdateStoreItemQuantityCommand command)
        {
            return await Execute(command, "GetStoreItemQuantityQuery");
        }
        #endregion
    }
}
