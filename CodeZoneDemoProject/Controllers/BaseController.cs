using CodeZoneProject.Application.Items.Commands.Create;
using CodeZoneProject.Infrastructure;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace CodeZoneProject.Web.Controllers
{
    public class BaseController : Controller 
    {
        private IMediator _mediator;
        protected IMediator Mediator => _mediator ?? (_mediator = HttpContext.RequestServices.GetService<IMediator>());
        public async Task<ActionResult> Execute<TRequest>(IRequest<TRequest> request, string Msg)
        {
            try
            {
                var response = await Mediator.Send(request);
                return Ok(new JsonResponse<TRequest> { Data = response, Message = Msg, StatusCode = (int)HttpStatusCode.OK, Success = true });
            }

            catch (Exception ex) 
            { return BadRequest(new JsonResponse<int> { Data = 0, Message = ex.Message, StatusCode = (int)HttpStatusCode.BadRequest }); }
        }

        //public void ValidateCommand<TRequest, TValidator>(TRequest request) where TValidator : AbstractValidator<TRequest>
        //{
        //    var validator = new TValidator();
        //    var res = validator.Validate(request);
        //    if (res.IsValid)
        //    {
        //        await Mediator.Send(command);
        //        return RedirectToAction(nameof(Index));
        //    }
        //    else
        //    {
        //        foreach (ValidationFailure failer in res.Errors)
        //        {
        //            ModelState.AddModelError(failer.PropertyName, failer.ErrorMessage);
        //        }
        //        return View(command);
        //    }
        //}
    }
}
