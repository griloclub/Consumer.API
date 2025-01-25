using Consumer.Application.UseCases.User.Interface;
using Consumer.Communication.Request;
using Consumer.Communication.Response;
using Consumer.Domain.Repositories.User;
using Consumer.Domain.Security;
using Consumer.Exception;
using Consumer.Exception.ExceptionBase;

namespace Consumer.Application.UseCases.User;
public class LoginUserUseCase : ILoginUserUseCase
{
    private readonly IUserReadOnlyRepository _userReadOnlyRepository;
    private readonly IPasswordEncripter _passwordEncripter;
    private readonly IAccessTokenGenerator _accessTokenGenerator;

    public LoginUserUseCase(IUserReadOnlyRepository userReadOnlyRepository, IPasswordEncripter passwordEncripter, IAccessTokenGenerator accessTokenGenerator)
    {
        _userReadOnlyRepository = userReadOnlyRepository;
        _passwordEncripter = passwordEncripter;
        _accessTokenGenerator = accessTokenGenerator;
    }

    public async Task<ResponseLoginUserJson> Login(RequestLoginUserJson request)
    {
        var user = await _userReadOnlyRepository.GetByUsernameAsync(request.Username);
        if (user is null)
            throw new InvalidLoginException(ResourceErrorMessages.USERNAME_OR_PASSWORD_INVALID);

        var passwordMatch = _passwordEncripter.VerificationPassword(request.Password, user.Password);
        if (!passwordMatch)
            throw new InvalidLoginException(ResourceErrorMessages.USERNAME_OR_PASSWORD_INVALID);

        return new ResponseLoginUserJson
        {
            Name = request.Username,
            Token = _accessTokenGenerator.GenerateAccessToken(user)
        };
    }
}