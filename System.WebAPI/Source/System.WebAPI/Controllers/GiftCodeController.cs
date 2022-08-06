namespace System.WebAPI.Controllers;

using System.EntityFramework.Commands.Giftcode;
using System.Models.ViewModel.GiftCode;
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
    /// <summary>
    /// List GiftCode Package
    /// </summary>
    /// <param name="giftCodeName"></param>
    /// <param name="fromDate">yyyy-MM-dd</param>
    /// <param name="toDate">yyyy-MM-dd</param>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [AllowAnonymous]
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

    /// <summary>
    /// List GiftCode in Package
    /// </summary>
    /// <param name="giftCodeId"></param>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpGet("data/list", Name = GiftCodeControllerRoute.GiftCodeDataList)]
    [SwaggerResponse(StatusCodes.Status200OK, "Success")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "The input is invalid.", typeof(ProblemDetails))]
    [SwaggerResponse(StatusCodes.Status406NotAcceptable,
            "The MIME type in the Accept HTTP header is not acceptable.", typeof(ProblemDetails))]
    [SwaggerResponse(StatusCodes.Status415UnsupportedMediaType,
            "The MIME type in the Content-Type HTTP header is unsupported.", typeof(ProblemDetails))]
    public Task<IActionResult> GetGiftCodeDataListAsync(
        long giftCodeId,
        [FromServices] GiftCodeDataListCommand command,
        CancellationToken cancellationToken) => command.ExecuteAsync(giftCodeId, cancellationToken);

    [AllowAnonymous]
    [HttpPost("create", Name = GiftCodeControllerRoute.GiftCodeAdd)]
    [SwaggerResponse(StatusCodes.Status200OK, "Success")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "The input is invalid.", typeof(ProblemDetails))]
    [SwaggerResponse(StatusCodes.Status406NotAcceptable,
            "The MIME type in the Accept HTTP header is not acceptable.", typeof(ProblemDetails))]
    [SwaggerResponse(StatusCodes.Status415UnsupportedMediaType,
            "The MIME type in the Content-Type HTTP header is unsupported.", typeof(ProblemDetails))]
    public Task<IActionResult> CreateGiftCodeAsync(
        [FromBody] SaveGiftCode giftcode,
        [FromServices] GiftCodeAddCommand command,
        CancellationToken cancellationToken) => command.ExecuteAsync(giftcode, cancellationToken);
}
