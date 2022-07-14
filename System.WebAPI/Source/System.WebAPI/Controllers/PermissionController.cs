namespace System.WebAPI.Controllers;

using System.EntityFramework.Commands.Permission;
using System.Models.ViewModel.User;
using System.WebAPI.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

[Route("[controller]")]
[ApiController]
[Authorize]
public class PermissionController : ControllerBase
{
    /// <summary>
    /// Get Permission List
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <param name="status">-1: All, 0: NoActive, 1: Active</param>
    /// <returns></returns>
    [HttpGet("list", Name = PermissionControllerRoute.GetListPermission)]
    [SwaggerResponse(StatusCodes.Status200OK, "Success")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "The input is invalid.", typeof(ProblemDetails))]
    [SwaggerResponse(StatusCodes.Status406NotAcceptable,
        "The MIME type in the Accept HTTP header is not acceptable.", typeof(ProblemDetails))]
    [SwaggerResponse(StatusCodes.Status415UnsupportedMediaType,
        "The MIME type in the Content-Type HTTP header is unsupported.", typeof(ProblemDetails))]
    public Task<IActionResult> GetPermissionListAsync(
    [FromServices] PermissionGetListCommand command,
    int? status,
    CancellationToken cancellationToken) => command.ExecuteAsync(status, cancellationToken);

    /// <summary>
    /// Create New Permission
    /// </summary>
    /// <param name="command"></param>
    /// <param name="permission"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("create", Name = PermissionControllerRoute.AddPermission)]
    [SwaggerResponse(StatusCodes.Status200OK, "Success")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "The input is invalid.", typeof(ProblemDetails))]
    [SwaggerResponse(StatusCodes.Status406NotAcceptable,
        "The MIME type in the Accept HTTP header is not acceptable.", typeof(ProblemDetails))]
    [SwaggerResponse(StatusCodes.Status415UnsupportedMediaType,
        "The MIME type in the Content-Type HTTP header is unsupported.", typeof(ProblemDetails))]
    public Task<IActionResult> AddPermissionAsync(
    [FromServices] PermissionAddCommand command,
    SavePermission permission,
    CancellationToken cancellationToken) => command.ExecuteAsync(permission, cancellationToken);

    /// <summary>
    /// Update Permission
    /// </summary>
    /// <param name="command"></param>
    /// <param name="permission"></param>
    /// <param name="permissionId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPut("update/{permissionId}", Name = PermissionControllerRoute.UpdatePermission)]
    [SwaggerResponse(StatusCodes.Status200OK, "Success")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "The input is invalid.", typeof(ProblemDetails))]
    [SwaggerResponse(StatusCodes.Status406NotAcceptable,
        "The MIME type in the Accept HTTP header is not acceptable.", typeof(ProblemDetails))]
    [SwaggerResponse(StatusCodes.Status415UnsupportedMediaType,
        "The MIME type in the Content-Type HTTP header is unsupported.", typeof(ProblemDetails))]
    public Task<IActionResult> UpdatePermissionAsync(
    [FromServices] PermissionUpdateCommand command,
    SavePermission permission,
    int permissionId,
    CancellationToken cancellationToken) => command.ExecuteAsync(permissionId, permission, cancellationToken);

    /// <summary>
    /// Delete Permission
    /// </summary>
    /// <param name="command"></param>
    /// <param name="permissionId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpDelete("delete/{permissionId}", Name = PermissionControllerRoute.DeletePermission)]
    [SwaggerResponse(StatusCodes.Status200OK, "Success")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "The input is invalid.", typeof(ProblemDetails))]
    [SwaggerResponse(StatusCodes.Status406NotAcceptable,
        "The MIME type in the Accept HTTP header is not acceptable.", typeof(ProblemDetails))]
    [SwaggerResponse(StatusCodes.Status415UnsupportedMediaType,
        "The MIME type in the Content-Type HTTP header is unsupported.", typeof(ProblemDetails))]
    public Task<IActionResult> DeletePermissionAsync(
    [FromServices] PermissionRemoveCommand command,
    int permissionId,
    CancellationToken cancellationToken) => command.ExecuteAsync(permissionId, cancellationToken);
}
