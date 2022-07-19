using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using WP.Infrastructures.Core;

namespace WP.Shared.WebApi.Controller
{
    [ApiController]
    public abstract class ApiController : ControllerBase
    {
        [NonAction]
        protected virtual ObjectResult Problem(dynamic exception)
        {
            var problemDetails = exception.Content;

            return Problem(problemDetails.Detail
                    , problemDetails.Instance
                    , problemDetails.Status
                    , problemDetails.Title
                    , problemDetails.Type);
        }


        [NonAction]
        protected virtual ActionResult Result(AppResult appSrvResult)
        {
            if (appSrvResult.IsSuccess)
                return Ok(appSrvResult.Content);
            return Problem(appSrvResult.ProblemDetails);
        }
    }
}
