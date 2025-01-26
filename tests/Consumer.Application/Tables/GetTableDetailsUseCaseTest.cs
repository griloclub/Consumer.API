using AutoMapper;
using CommonTests.AutoMapper;
using Consumer.Application.UseCases.Tables;
using Consumer.Communication.Response;
using Consumer.Exception;
using Consumer.Exception.ExceptionBase;
using ConsumerCommon.Entities;
using ConsumerCommon.Repositories;
using FluentAssertions;
using Moq;
using System.Text.Json;

namespace ConsumerApplication.Tests.Tables;
public class GetTableDetailsUseCaseTest
{
    [Fact]
    //Verifica se retorna corretamente os detalhes da mesa (id, número e nome do cliente)
    public async Task Success_Return_TableDetails()
    {
        var table = TableDetailsBuilder.Build();
        var useCase = CreateUseCase(table);
        var result = await useCase.GetTableDetailsAsync(1);

        result.Should().NotBeNull();
        result.Id.Should().Be(table.Id);
        result.Number.Should().Be(table.Number);
        result.ClientName.Should().Be(table.ClientName);
    }

    [Fact]
    //Verifica se lança exceção quando a mesa não é encontrada
    public async Task Details_When_TableNotFound_Throw_Exception()
    {
        var useCase = CreateUseCase(null);
        var action = () => useCase.GetTableDetailsAsync(1);
        await action.Should().ThrowAsync<NotFoundException>()
            .WithMessage(ResourceErrorMessages.DETAILS_NOT_FOUND);
    }

    [Fact]
    //Verifica se os cálculos de valores (total, taxa de serviço e pagamento total) estão corretos
    public async Task Map_Calculate_Values_Correctly()
    {
        var table = TableDetailsBuilder.Build();
        var useCase = CreateUseCase(table);
        var result = await useCase.GetTableDetailsAsync(1);

        result.Values.Total.Should().Be(37.0M);
        result.Values.ServiceFee.Should().Be(3.70M);
        result.Values.TotalPayment.Should().Be(40.70M);
    }

    [Fact]
    //Testa se lança exceção de validação quando os dados do VALUES são inválidos (negativos)
    //Itens devem ter preço e quantidade positivos / (0) caso não tenha pedido / Mesa aberta
    public async Task Details_When_ValidationFails_Should_ThrowValidationException()
    {
        var mapper = new Mock<IMapper>();
        mapper.Setup(m => m.Map<ResponseTableDetailsJson>(It.IsAny<Consumer.Domain.Entities.Table>()))
            .Returns(new ResponseTableDetailsJson
            {
                Values = new ValuesJson { Total = 0, ServiceFee = 0, TotalPayment = 0 }
            });

        var repository = new TablesReadOnlyRepositoryBuilder()
            .WithTableDetails(TableDetailsBuilder.BuildInvalid())
            .Build();

        var useCase = new GetTableDetailsUseCase(repository, mapper.Object);
        var action = () => useCase.GetTableDetailsAsync(1);

        var exception = await action.Should().ThrowAsync<ErrorOnValidationException>();

        var result = exception.Which.GetErros().FirstOrDefault();
        var dict = JsonSerializer.Deserialize<Dictionary<string, string>>(JsonSerializer.Serialize(result));

        dict!["PropertyName"].Should().Be("Items");
        dict["Message"].Should().Be(ResourceErrorMessages.PRICE_AND_QUANTITY_POSITIVE);
    }

    private GetTableDetailsUseCase CreateUseCase(Consumer.Domain.Entities.Table? table)
    {
        var repository = new TablesReadOnlyRepositoryBuilder()
            .WithTableDetails(table).Build();
        return new GetTableDetailsUseCase(repository, AutoMapperBuilder.Build());
    }
}