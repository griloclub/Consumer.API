using Consumer.Application.UseCases.Tables.Interface;
using Consumer.Communication.Response;
using Consumer.Communication.Response.Erros;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Consumer.API.Controllers;

[Route("api/tables")]
[ApiController]
[Authorize]
public class TablesController : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(ResponseTableJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetOpenTables([FromServices] IGetOpenTablesUseCase useCase)
    {
        var response = await useCase.GetOpenTablesAsync();
        return Ok(response);
    }
}