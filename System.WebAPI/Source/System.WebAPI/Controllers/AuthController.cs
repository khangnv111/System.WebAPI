namespace System.WebAPI.Controllers;

using System.EntityFramework.Commands.Auth;
using System.Models.Model;
using System.WebAPI.Constants;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

[Route("[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    /// <summary>
    /// Login cms
    /// </summary>
    /// <param name="command"></param>
    /// <param name="auth"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("cms/login", Name = AuthControllerRoute.CmsLogin)]
    [SwaggerResponse(StatusCodes.Status200OK, "Success")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "The input is invalid.", typeof(ProblemDetails))]
    [SwaggerResponse(StatusCodes.Status406NotAcceptable,
            "The MIME type in the Accept HTTP header is not acceptable.", typeof(ProblemDetails))]
    [SwaggerResponse(StatusCodes.Status415UnsupportedMediaType,
            "The MIME type in the Content-Type HTTP header is unsupported.", typeof(ProblemDetails))]
    public Task<IActionResult> CmsLoginAsync(
        [FromServices] CmsLoginCommand command,
        AuthLogin auth,
        CancellationToken cancellationToken) => command.ExecuteAsync(auth, cancellationToken);
}
