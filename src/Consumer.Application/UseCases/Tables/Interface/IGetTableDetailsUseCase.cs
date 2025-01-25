using Consumer.Communication.Response;

namespace Consumer.Application.UseCases.Tables.Interface;
public interface IGetTableDetailsUseCase
{
    Task<ResponseTableDetailsJson> GetTableDetailsAsync(long id);
}