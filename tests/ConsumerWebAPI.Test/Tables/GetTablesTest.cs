using FluentAssertions;
using System.Net;
using System.Text.Json;
using WebApi.Test;
using Xunit;

namespace ConsumerWebAPI.Test.Tables;
public class GetTablesTest : ConsumerClassFixture
{
    private const string METHOD = "api/tables";
    private readonly CustomWebApplicationFactory _factory;
    private readonly string _token;
    public GetTablesTest(CustomWebApplicationFactory customWebApplicationFactory) : base(customWebApplicationFactory)
    {
        _factory = customWebApplicationFactory;
        _token = customWebApplicationFactory.User.GetToken();
    }

    [Fact]
    //Verifica o retorno HTTP 200 e estrutura JSON
    //Quantidade e lista de mesas abertas do usuário
    //Quantidade e lista total de mesas
    //Confirmação das contagens esperadas(1 mesa aberta e 2 mesas no total)
    public async Task Success_Return_TablesAndQuantities()
    {
        var result = await DoGet(requestUri: METHOD, token: _token);
        result.StatusCode.Should().Be(HttpStatusCode.OK);

        var response = JsonDocument.Parse(await result.Content.ReadAsStreamAsync());
        var root = response.RootElement;

        root.GetProperty("myOpenTables").GetProperty("quantity").GetInt32().Should().Be(1);
        root.GetProperty("myOpenTables").GetProperty("tables").GetArrayLength().Should().Be(1);

        root.GetProperty("allTables").GetProperty("quantity").GetInt32().Should().Be(2);
        root.GetProperty("allTables").GetProperty("tables").GetArrayLength().Should().Be(2);
    }

    [Fact]
    //Confirma que com banco vazio retorna quantidades zeradas para todas as mesas
    public async Task Empty_Tables_ReturnZero()
    {
        var emptyFactory = _factory.WithEmptyTables();

        var result = await DoGet(METHOD, _token);
        var root = JsonDocument.Parse(await result.Content.ReadAsStreamAsync()).RootElement;

        root.GetProperty("myOpenTables").GetProperty("quantity").GetInt32().Should().Be(0);
        root.GetProperty("myOpenTables").GetProperty("tables").GetArrayLength().Should().Be(0);
        root.GetProperty("allTables").GetProperty("quantity").GetInt32().Should().Be(0);
        root.GetProperty("allTables").GetProperty("tables").GetArrayLength().Should().Be(0);
    }

    [Fact]
    //Valida se as propriedades (id, número, status) das mesas estão corretas e no formato esperado
    public async Task ValidateTableProperties()
    {
        var result = await DoGet(METHOD, _token);
        result.StatusCode.Should().Be(HttpStatusCode.OK);

        var root = JsonDocument.Parse(await result.Content.ReadAsStreamAsync()).RootElement;
        foreach (var table in root.GetProperty("myOpenTables").GetProperty("tables").EnumerateArray())
        {
            table.GetProperty("id").GetInt32().Should().BeGreaterThan(0);
            table.GetProperty("number").GetInt32().Should().BeGreaterThan(0);
            table.GetProperty("status").GetInt32().Should().BeGreaterThanOrEqualTo(0);
        }

        foreach (var table in root.GetProperty("allTables").GetProperty("tables").EnumerateArray())
        {
            table.GetProperty("id").GetInt32().Should().BeGreaterThan(0);
            table.GetProperty("number").GetInt32().Should().BeGreaterThan(0);
            table.GetProperty("status").GetInt32().Should().BeGreaterThanOrEqualTo(0);
        }
    }

    [Fact]
    //Confirma que token inválido retorna erro 401 (Unauthorized)
    public async Task UnauthorizedAccess_WithoutToken()
    {
        var result = await DoGet(requestUri: METHOD, "123456");
        result.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }
}