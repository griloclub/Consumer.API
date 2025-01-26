using CommonTests.Cryptography;
using CommonTests.Repositories;
using CommonTests.Requests;
using CommonTests.Token;
using Consumer.Application.UseCases.User;
using Consumer.Exception;
using Consumer.Exception.ExceptionBase;
using ConsumerCommon.Entities;
using FluentAssertions;

namespace ConsumerApplication.Tests.User;
public class LoginUserUseCaseTest
{
    [Fact]
    //Testa se o login foi bem-sucedido, verificando retorno do token e nome (Username)
    public async Task Success_Return_TokenAndUsername()
    {
        var user = UserBuilder.Build();
        var request = RequestLoginUserJsonBuilder.Build();
        request.Username = user.Username;

        var useCase = CreateUseCase(user, request.Password);
        var response = await useCase.Login(request);

        response.Should().NotBeNull();
        response.Name.Should().Be(user.Username);
        response.Token.Should().NotBeNullOrWhiteSpace();
    }

    [Fact]
    //Verifica se lança exceção quando usuário não existe
    public async Task Login_When_UserNotFound_Throw_Exception()
    {
        var user = UserBuilder.Build();
        var request = RequestLoginUserJsonBuilder.Build();

        var useCase = CreateUseCase(user, request.Password);
        var action = () => useCase.Login(request);

        await action.Should().ThrowAsync<InvalidLoginException>()
            .WithMessage(ResourceErrorMessages.USERNAME_OR_PASSWORD_INVALID);
    }

    [Fact]
    //Testa se lança exceção quando senha está incorreta / username está incorreto
    public async Task Login_When_PasswordNotMatch_Throw_Exception()
    {
        var user = UserBuilder.Build();
        var request = RequestLoginUserJsonBuilder.Build();
        request.Username = user.Username;

        var useCase = CreateUseCase(user);
        var action = () => useCase.Login(request);

        await action.Should().ThrowAsync<InvalidLoginException>()
            .WithMessage(ResourceErrorMessages.USERNAME_OR_PASSWORD_INVALID);
    }

    private LoginUserUseCase CreateUseCase(Consumer.Domain.Entities.User user, string? password = null)
    {
        var repository = new UserReadOnlyRepositoryBuilder()
            .WithUser(user)
            .Build();

        var encrypter = new PasswordEncrypterBuilder()
            .WithPassword(password)
            .Build();

        var tokenGenerator = new JwtTokenGeneratorBuilder()
            .WithUser(user)
            .Build();

        return new LoginUserUseCase(repository, encrypter, tokenGenerator);
    }
}