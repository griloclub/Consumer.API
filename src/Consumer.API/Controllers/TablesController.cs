using Consumer.Application.UseCases.Tables.Interface;
using Consumer.Communication.Response;
using Consumer.Communication.Response.Erros;
using Consumer.Exception;
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
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetTables([FromServices] IGetTablesUseCase useCase)
    {
        var response = await useCase.GetTablesAsync();
        return Ok(response);
    }

    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(typeof(ResponseTableDetailsJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetTableDetails([FromServices] IGetTableDetailsUseCase useCase, [FromRoute] long id)
    {
        var response = await useCase.GetTableDetailsAsync(id);

        if (response is not null)
            return Ok(response);

        return NotFound(ResourceErrorMessages.DETAILS_NOT_FOUND);
    }
}