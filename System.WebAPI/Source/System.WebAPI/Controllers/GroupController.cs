namespace System.WebAPI.Controllers;

using System.EntityFramework.Commands.Groups;
using System.Models.ViewModel.User;
using System.WebAPI.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

[Route("[controller]")]
[ApiController]
[Authorize]
public class GroupController : ControllerBase
{
    /// <summary>
    /// Get Group List
    /// </summary>
    /// <param name="command"></param>
    /// <param name="status"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("list", Name = GroupControllerRoute.GetGroup)]
    [SwaggerResponse(StatusCodes.Status200OK, "Success")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "The input is invalid.", typeof(ProblemDetails))]
    [SwaggerResponse(StatusCodes.Status406NotAcceptable,
            "The MIME type in the Accept HTTP header is not acceptable.", typeof(ProblemDetails))]
    [SwaggerResponse(StatusCodes.Status415UnsupportedMediaType,
            "The MIME type in the Content-Type HTTP header is unsupported.", typeof(ProblemDetails))]
    public Task<IActionResult> GetUserListAsync(
        [FromServices] GroupGetListCommand command,
        CancellationToken cancellationToken, int status = -1) => command.ExecuteAsync(status, cancellationToken);

    /// <summary>
    /// Create New Group
    /// </summary>
    /// <param name="command"></param>
    /// <param name="group"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("create", Name = GroupControllerRoute.AddGroup)]
    [SwaggerResponse(StatusCodes.Status200OK, "Success")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "The input is invalid.", typeof(ProblemDetails))]
    [SwaggerResponse(StatusCodes.Status406NotAcceptable,
            "The MIME type in the Accept HTTP header is not acceptable.", typeof(ProblemDetails))]
    [SwaggerResponse(StatusCodes.Status415UnsupportedMediaType,
            "The MIME type in the Content-Type HTTP header is unsupported.", typeof(ProblemDetails))]
    public Task<IActionResult> AddGroupAsync(
        [FromServices] GroupAddCommand command,
        SaveGroup group,
        CancellationToken cancellationToken) => command.ExecuteAsync(group, cancellationToken);

    /// <summary>
    /// Update Group
    /// </summary>
    /// <param name="command"></param>
    /// <param name="groupId"></param>
    /// <param name="group"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPut("update/{groupId}", Name = GroupControllerRoute.UpdateGroup)]
    [SwaggerResponse(StatusCodes.Status200OK, "Success")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "The input is invalid.", typeof(ProblemDetails))]
    [SwaggerResponse(StatusCodes.Status406NotAcceptable,
            "The MIME type in the Accept HTTP header is not acceptable.", typeof(ProblemDetails))]
    [SwaggerResponse(StatusCodes.Status415UnsupportedMediaType,
            "The MIME type in the Content-Type HTTP header is unsupported.", typeof(ProblemDetails))]
    public Task<IActionResult> UpdateGroupAsync(
        [FromServices] GroupUpdateCommand command,
        int groupId,
        SaveGroup group,
        CancellationToken cancellationToken) => command.ExecuteAsync(groupId, group, cancellationToken);

    /// <summary>
    /// Config permission for group
    /// </summary>
    /// <param name="command"></param>
    /// <param name="data"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("config-permission", Name = GroupControllerRoute.ConfigPermission)]
    [SwaggerResponse(StatusCodes.Status200OK, "Success")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "The input is invalid.", typeof(ProblemDetails))]
    [SwaggerResponse(StatusCodes.Status406NotAcceptable,
            "The MIME type in the Accept HTTP header is not acceptable.", typeof(ProblemDetails))]
    [SwaggerResponse(StatusCodes.Status415UnsupportedMediaType,
            "The MIME type in the Content-Type HTTP header is unsupported.", typeof(ProblemDetails))]
    public Task<IActionResult> ConfigPermissionAsync(
        [FromServices] GroupConfigPermissionCommand command,
        SaveGroupPermission data,
        CancellationToken cancellationToken) => command.ExecuteAsync(data, cancellationToken);
}
