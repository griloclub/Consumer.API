using Consumer.Communication.Request;
using Consumer.Communication.Response;

namespace Consumer.Application.UseCases.User.Interface;
public interface ILoginUserUseCase
{
    Task<ResponseLoginUserJson> Login(RequestLoginUserJson request);
}