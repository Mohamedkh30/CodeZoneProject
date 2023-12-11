using CodeZoneProject.Application.Stores.Commands.Create;
using CodeZoneProject.Application.Stores.Commands.Delete;
using CodeZoneProject.Application.Stores.Commands.Update;
using CodeZoneProject.Application.Stores.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CodeZoneProject.Web.Controllers
{
    public class StoresController : BaseController
    {
        public async Task<ActionResult> Index()
        {
            ViewBag.Stores = await Mediator.Send(new GetAllStoresQuery());
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateStoreCommand command)
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
            var store = await Mediator.Send(new GetStoreByIdQuery() { Id = id });
            if (store != null)
                return View(new UpdateStoreCommand()
                {
                    Id = store.Id,
                    Name = store.Name,
                    Address = store.Address
                });

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<ActionResult> Edit(UpdateStoreCommand command)
        {
            try
            {
                await Execute(command, "Update Store Command");
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(command);
            }
        }

        [HttpGet]
        public async Task<ActionResult> Delete(Guid id)
        {
            await Mediator.Send(new DeleteStoreCommand() { Id = id });
            return RedirectToAction(nameof(Index));
        }
    }
}
