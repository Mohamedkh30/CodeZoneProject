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

        public async Task<ActionResult> ValidateAndProcessCommand<TCommand, TValidator>(TCommand command)
            where TValidator : AbstractValidator<TCommand>, new()
        {
            var validator = new TValidator();
            var validationResult = await validator.ValidateAsync(command);

            if (validationResult.IsValid)
            {
                await Mediator.Send(command);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                foreach (var error in validationResult.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                return View(command);
            }
        }
    }
}
