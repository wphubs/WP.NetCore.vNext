using MediatR;
using Microsoft.AspNetCore.Mvc;
using WP.Infrastructures.JwtBearer;
using WP.Shared.Application;
using WP.Shared.Application.Contracts;

namespace WP.Shared.WebApi.Controller
{
    [ApiController]
    public abstract class ApiController : ControllerBase
    {
        


        [ApiExplorerSettings(IgnoreApi = true)]
        protected TokenModelJwt GetToken()
        {
            return JWTEncryption.TokenInfo(User);
        }

        [NonAction]
        protected virtual ActionResult<TValue> Result<TValue>(ResponseResult<TValue> appSrvResult)
        {
            if (appSrvResult.IsSuccess)
                return appSrvResult.Content;
            return Problem(appSrvResult.ProblemDetails);
        }


        [NonAction]
        protected virtual ActionResult Result(ResponseResult appSrvResult)
        {
            if (appSrvResult.IsSuccess)
                return NoContent();
            return Problem(appSrvResult.ProblemDetails);
        }

        [NonAction]
        protected virtual ObjectResult Problem(Application.Contracts.ProblemDetails problemDetails)
        {
            problemDetails.Instance ??= this.Request.Path.ToString();
            return Problem(problemDetails.Detail
                , problemDetails.Instance
                , problemDetails.Status
                , problemDetails.Title
                , problemDetails.Type);
        }

        /// <summary>
        ///Refit.ProblemDetails => Problem
        /// </summary>
        /// <param name="problemDetails"></param>
        /// <returns></returns>
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
        protected virtual ActionResult<TValue> CreatedResult<TValue>(ResponseResult<TValue> appSrvResult)
        {
            if (appSrvResult.IsSuccess)
                return Created(this.Request.Path, appSrvResult.Content);
            return Problem(appSrvResult.ProblemDetails);
        }


    }
}
