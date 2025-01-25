using Consumer.Application.UseCases.User.Interface;
using Consumer.Communication.Request;
using Consumer.Communication.Response;
using Consumer.Communication.Response.Erros;
using Microsoft.AspNetCore.Mvc;

namespace Consumer.API.Controllers;
[Route("api/login")]
[ApiController]
public class LoginController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseLoginUserJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login([FromServices] ILoginUserUseCase useCase, [FromBody] RequestLoginUserJson request)
    {
        var response = await useCase.Login(request);
        return Ok(response);
    }
}
