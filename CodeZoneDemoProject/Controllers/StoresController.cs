using CodeZoneProject.Application.Items.Commands.Create;
using CodeZoneProject.Application.Items.Commands.Delete;
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
                return await ValidateAndProcessCommand<CreateStoreCommand, CreateStoreCommandValidator>(command);
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
                return await ValidateAndProcessCommand<UpdateStoreCommand, UpdateStoreCommandValidator>(command);
            }
            catch
            {
                return View(command);
            }
        }

        [HttpGet]
        public async Task<ActionResult> Delete(Guid id)
        {
            var result = await Mediator.Send(new DeleteStoreCommand() { Id = id });
            if (result)
                return Ok();
            else
                return BadRequest();
        }
    }
}
