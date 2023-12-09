using CodeZoneProject.Application.Items.Queries;
using CodeZoneProject.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;

namespace CodeZoneProject.Web.Controllers
{
    public class StoresController : Controller
    {
        private IMediator _mediator;
        protected IMediator Mediator => _mediator ?? (_mediator = HttpContext.RequestServices.GetService<IMediator>());

        // GET: StoresController
        public async Task<ActionResult> Index()
        {
            await Mediator.Send(new GetAllItemsQuery());
            return View();
        }

        // GET: StoresController/Create
        public ActionResult Create()
        {
            var catergories = Enum.GetValues(typeof(ItemCategory)).Cast<ItemCategory>().Select(v => new SelectListItem
            {
                Text = v.ToString(),
                Value = ((int)v).ToString()
            }).ToList();
            ViewBag.Categories = new SelectList(catergories, "Value", "Text");
            return View();
        }

        // POST: StoresController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: StoresController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: StoresController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: StoresController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: StoresController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
