using AutoMapper;
using CommonTests.AutoMapper;
using CommonTests.LoggedUser;
using Consumer.Application.UseCases.Tables;
using Consumer.Communication.Response;
using Consumer.Domain.Entities;
using ConsumerCommon.Entities;
using ConsumerCommon.Repositories;
using FluentAssertions;
using Moq;

namespace ConsumerApplication.Tests.Tables;
public class GetTablesUseCaseTest
{
    [Fact]
    //Testa retorno das mesas abertas do usuário e total de mesas, verificando quantidades
    public async Task Success_Return_TablesAndQuantities()
    {
        var user = UserBuilder.Build();
        var useCase = CreateUseCase(user);
        var result = await useCase.GetTablesAsync();

        result.MyOpenTables.Should().NotBeNull();
        result.MyOpenTables.Quantity.Should().Be(2);
        result.AllTables.Quantity.Should().Be(5);
    }

    [Fact]
    //Verifica se retorna lista vazia quando não há mesas abertas
    public async Task Tables_When_NoOpenTables_Return_Empty()
    {
        var user = UserBuilder.Build();
        var useCase = CreateUseCase(user, openTablesCount: 0);
        var result = await useCase.GetTablesAsync();

        result.MyOpenTables.Quantity.Should().Be(0);
        result.MyOpenTables.Tables.Should().BeEmpty();
    }

    [Fact]
    //Confirma se o mapeamento dos dados é chamado corretamente usando Mock do AutoMapper
    public async Task Map_Called_Correctly()
    {
        var user = UserBuilder.Build();
        var mapper = new Mock<IMapper>();
        var useCase = new GetTablesUseCase(
            new TablesReadOnlyRepositoryBuilder()
                .WithUser(user)
                .WithOpenTables(2)
                .WithAllTables(5)
                .Build(),
            mapper.Object,
            LoggedUserBuilder.Build(user)
        );

        await useCase.GetTablesAsync();
        mapper.Verify(x => x.Map<List<ResponseInformationTableJson>>(It.IsAny<List<Table>>()), Times.Exactly(2));
    }

    [Fact]
    //Testa o retorno quando não existem mesas no sistema / não encontradas
    public async Task GetTables_Should_Return_EmptyAllTables()
    {
        var user = UserBuilder.Build();
        var useCase = CreateUseCase(user, allTablesCount: 0);
        var result = await useCase.GetTablesAsync();
        result.AllTables.Quantity.Should().Be(0);
        result.AllTables.Tables.Should().BeEmpty();
    }

    private GetTablesUseCase CreateUseCase(Consumer.Domain.Entities.User user, int openTablesCount = 2, int allTablesCount = 5)
    {
        var repository = new TablesReadOnlyRepositoryBuilder()
            .WithUser(user)
            .WithOpenTables(openTablesCount)
            .WithAllTables(allTablesCount)
            .Build();

        return new GetTablesUseCase(repository, AutoMapperBuilder.Build(), LoggedUserBuilder.Build(user));
    }
}