using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EmployeeManagement.Controllers
{    
    public class ErrorController : Controller
    {
        private readonly ILogger logger;

        public ErrorController(ILogger<ErrorController> logger)
        {
            this.logger = logger;
        }

        [Route("Error/{statusCode}")]
        public IActionResult HttpStatusCodeHandler(int statusCode)
        {
            var statusCodeResult = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();

            switch (statusCode)
            {
                case 404:
                    ViewBag.ErrorMessage = "Sorry, the resource you requested could not be found";
                    //ViewBag.Path = statusCodeResult.OriginalPath;                    
                    //ViewBag.QueryString = statusCodeResult.OriginalQueryString;
                    logger.LogWarning($"404 Error Occurred. Path: {statusCodeResult.OriginalPath} and QueryString: {statusCodeResult.OriginalQueryString}");
                    break;
                default:
                    break;
            }

            return View("Error", statusCode);
        }


        [AllowAnonymous]
        [Route("Error")]
        public IActionResult ExceptionHandler()
        {
            var exceptionDetails = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            ViewBag.ErrorMessage = exceptionDetails.Error.Message;
            //ViewBag.Path = exceptionDetails.Path;
            //ViewBag.StackTrace = exceptionDetails.Error.StackTrace;
            logger.LogError($"The path {exceptionDetails.Path} threw an exception {exceptionDetails.Error}");
            return View("Error");
        }
    }
}