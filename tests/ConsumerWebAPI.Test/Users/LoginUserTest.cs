using CommonTests.Requests;
using Consumer.Communication.Request;
using Consumer.Exception;
using FluentAssertions;
using System.Globalization;
using System.Net;
using System.Text.Json;
using WebApi.Test.InlineData;
using Xunit;

namespace WebApi.Test.Users;
public class LoginUserTest : ConsumerClassFixture
{
    private const string METHOD = "api/Login";
    private readonly string _username;
    private readonly string _password;
    public LoginUserTest(CustomWebApplicationFactory customWebApplicationFactory) : base(customWebApplicationFactory)
    {

        _username = customWebApplicationFactory.User.GetUsername();
        _password = customWebApplicationFactory.User.GetPassword();
    }

    [Fact]
    //Verifica se o login foi bem-sucedido com retorno HTTP 200
    //Nome do usuário correto
    //Token válido
    public async Task Success()
    {
        var request = new RequestLoginUserJson
        {
            Username = _username,
            Password = _password
        };

        var result = await DoPost(requestUri: METHOD, request: request);
        result.StatusCode.Should().Be(HttpStatusCode.OK);

        var body = await result.Content.ReadAsStreamAsync();
        var response = await JsonDocument.ParseAsync(body);

        response.RootElement.GetProperty("name").GetString().Should().Be(_username);
        response.RootElement.GetProperty("token").GetString().Should().NotBeNullOrEmpty();
    }

    [Theory]
    //Verifica falha com dados inválidos com o retorno HTTP 401
    //Mensagem de erro correta em diferentes culturas
    //Validação da quantidade e conteúdo dos erros
    [ClassData(typeof(CultureInlinaData))]
    public async Task Erro_Login_Invalid(string culture)
    {
        var request = RequestLoginUserJsonBuilder.Build();


        var result = await DoPost(requestUri: METHOD, request: request, culture: culture);
        result.StatusCode.Should().Be(HttpStatusCode.Unauthorized);

        var body = await result.Content.ReadAsStreamAsync();
        var response = await JsonDocument.ParseAsync(body);

        var resultErros = response.RootElement.GetProperty("erros").EnumerateArray();
        var expectedMessage = ResourceErrorMessages.ResourceManager.GetString("USERNAME_OR_PASSWORD_INVALID", new CultureInfo(culture));

        resultErros.Should().HaveCount(1).And.Contain(e => e.GetString()!.Equals(expectedMessage));
    }
}