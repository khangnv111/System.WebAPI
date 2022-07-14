namespace System.WebAPI.Controllers;

using System.EntityFramework.Commands.Users;
using System.Models.ViewModel.User;
using System.WebAPI.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

[Route("[controller]")]
[ApiController]
[Authorize]
public class UserController : ControllerBase
{
    /// <summary>
    /// Get User List
    /// </summary>
    /// <param name="command"></param>
    /// <param name="userName"></param>
    /// <param name="status"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("list", Name = UsersControllerRoute.GetListUser)]
    [SwaggerResponse(StatusCodes.Status200OK, "Success")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "The input is invalid.", typeof(ProblemDetails))]
    [SwaggerResponse(StatusCodes.Status406NotAcceptable,
            "The MIME type in the Accept HTTP header is not acceptable.", typeof(ProblemDetails))]
    [SwaggerResponse(StatusCodes.Status415UnsupportedMediaType,
            "The MIME type in the Content-Type HTTP header is unsupported.", typeof(ProblemDetails))]
    public Task<IActionResult> GetUserListAsync(
        [FromServices] UserGetListCommand command,
        string userName,
        CancellationToken cancellationToken,
        int status = -1) => command.ExecuteAsync(userName, status, cancellationToken);

    /// <summary>
    /// Create New User
    /// </summary>
    /// <param name="command"></param>
    /// <param name="user"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("create", Name = UsersControllerRoute.AddUser)]
    [SwaggerResponse(StatusCodes.Status200OK, "Success")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "The input is invalid.", typeof(ProblemDetails))]
    [SwaggerResponse(StatusCodes.Status406NotAcceptable,
            "The MIME type in the Accept HTTP header is not acceptable.", typeof(ProblemDetails))]
    [SwaggerResponse(StatusCodes.Status415UnsupportedMediaType,
            "The MIME type in the Content-Type HTTP header is unsupported.", typeof(ProblemDetails))]
    public Task<IActionResult> AddUserAsync(
        [FromServices] UserAddCommand command,
        SaveUser user,
        CancellationToken cancellationToken) => command.ExecuteAsync(user, cancellationToken);

    /// <summary>
    /// Get User Info
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("info", Name = UsersControllerRoute.GetUser)]
    [SwaggerResponse(StatusCodes.Status200OK, "Success")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "The input is invalid.", typeof(ProblemDetails))]
    [SwaggerResponse(StatusCodes.Status406NotAcceptable,
            "The MIME type in the Accept HTTP header is not acceptable.", typeof(ProblemDetails))]
    [SwaggerResponse(StatusCodes.Status415UnsupportedMediaType,
            "The MIME type in the Content-Type HTTP header is unsupported.", typeof(ProblemDetails))]
    public Task<IActionResult> GetUserInfoAsync(
        [FromServices] UserGetInfoCommand command,
        CancellationToken cancellationToken) => command.ExecuteAsync(cancellationToken);
}
