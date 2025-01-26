using FluentAssertions;
using System.Net;
using System.Text.Json;
using WebApi.Test;
using Xunit;

namespace ConsumerWebAPI.Test.Tables;
public class GetTableDetailsTest : ConsumerClassFixture
{
    private const string METHOD = "api/tables";
    private readonly string _token;
    private readonly long _tableDetailsId;
    private readonly long _tableId;

    public GetTableDetailsTest(CustomWebApplicationFactory factory) : base(factory)
    {
        _token = factory.User.GetToken();
        _tableDetailsId = factory.TableDetails.GetId();
        _tableId = factory.Table.GetId();
    }

    [Fact]
    //Verifica se retornou Ok e valida estrutura do JSON
    //Dados básicos da mesa
    //Itens do pedido e seus detalhes
    //Valores totais calculados
    public async Task Success_ReturnTableDetails()
    {
        var result = await DoGet($"{METHOD}/{_tableDetailsId}", _token);
        result.StatusCode.Should().Be(HttpStatusCode.OK);

        var root = JsonDocument.Parse(await result.Content.ReadAsStreamAsync()).RootElement;

        // Validar estrutura básica
        ValidateBasicStructure(root);
        ValidateItemsStructure(root.GetProperty("items"));
        ValidateValuesStructure(root.GetProperty("values"));
    }

    private void ValidateBasicStructure(JsonElement root)
    {
        root.GetProperty("id").GetInt64().Should().BeGreaterThan(0);
        root.GetProperty("number").GetInt32().Should().BeGreaterThan(0);
        root.GetProperty("clientName").GetString().Should().NotBeNullOrEmpty();
        root.GetProperty("quantity").GetInt32().Should().BeGreaterThanOrEqualTo(0);
        root.GetProperty("status").GetInt32().Should().BeGreaterThanOrEqualTo(0);
    }

    private void ValidateItemsStructure(JsonElement items)
    {
        var itemsList = items.EnumerateArray().ToList();
        itemsList.Should().NotBeEmpty();

        foreach (var item in itemsList)
        {
            ValidateItem(item);
        }
    }

    private void ValidateItem(JsonElement item)
    {
        item.GetProperty("id").GetInt32().Should().BeGreaterThan(0);
        item.GetProperty("name").GetString().Should().NotBeNullOrEmpty();
        item.GetProperty("price").GetDecimal().Should().BeGreaterThan(0);
        item.GetProperty("quantity").GetInt32().Should().BeGreaterThan(0);

        var additions = item.GetProperty("additions").EnumerateArray();
        foreach (var addition in additions)
        {
            addition.GetProperty("name").GetString().Should().NotBeNullOrEmpty();
            addition.GetProperty("price").GetDecimal().Should().BeGreaterThanOrEqualTo(0);
        }

        var options = item.GetProperty("options").EnumerateArray();
        foreach (var option in options)
        {
            option.GetProperty("name").GetString().Should().NotBeNullOrEmpty();
        }
    }

    private void ValidateValuesStructure(JsonElement values)
    {
        values.GetProperty("total").GetDecimal().Should().BeGreaterThanOrEqualTo(0);
        values.GetProperty("serviceFee").GetDecimal().Should().BeGreaterThanOrEqualTo(0);
        values.GetProperty("totalPayment").GetDecimal().Should().BeGreaterThanOrEqualTo(0);
    }

    [Fact]
    //Verifica se o retorno da mesa está sem itens, confirmando valores zerados
    private async Task Success_ReturnTable_ButNot_Items()
    {
        var result = await DoGet($"{METHOD}/{_tableId}", _token);
        result.StatusCode.Should().Be(HttpStatusCode.OK);

        var response = JsonDocument.Parse(await result.Content.ReadAsStreamAsync());
        var root = response.RootElement;

        root.GetProperty("id").GetInt64().Should().Be(_tableId);
        root.GetProperty("number").GetInt32().Should().BeGreaterThan(0);
        root.GetProperty("clientName").GetString().Should().NotBeNullOrEmpty();

        var values = root.GetProperty("values");
        values.GetProperty("total").GetDecimal().Should().Be(0);
        values.GetProperty("serviceFee").GetDecimal().Should().Be(0);
        values.GetProperty("totalPayment").GetDecimal().Should().Be(0);
    }

    [Fact]
    //Verifica se a mesa foi encontrada / existe para retornar o erro 404
    public async Task NotFound_WhenTableDoesNotExist()
    {
        var result = await DoGet($"{METHOD}/999", _token);
        result.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}