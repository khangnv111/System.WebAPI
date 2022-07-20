namespace System.WebAPI.Controllers;

using System.EntityFramework.Commands.Giftcode;
using System.WebAPI.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

[Route("[controller]")]
[ApiController]
[Authorize]
public class GiftCodeController : ControllerBase
{
    [HttpGet("list", Name = GiftCodeControllerRoute.GiftCodeList)]
    [SwaggerResponse(StatusCodes.Status200OK, "Success")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "The input is invalid.", typeof(ProblemDetails))]
    [SwaggerResponse(StatusCodes.Status406NotAcceptable,
            "The MIME type in the Accept HTTP header is not acceptable.", typeof(ProblemDetails))]
    [SwaggerResponse(StatusCodes.Status415UnsupportedMediaType,
            "The MIME type in the Content-Type HTTP header is unsupported.", typeof(ProblemDetails))]
    public Task<IActionResult> GetGiftCodeListAsync(
        string giftCodeName,
        string fromDate,
        string toDate,
        [FromServices] GiftCodeListCommand command,
        CancellationToken cancellationToken) => command.ExecuteAsync(giftCodeName, fromDate, toDate, cancellationToken);
}
