using Consumer.Communication.Response;

namespace Consumer.Application.UseCases.Tables.Interface;
public interface IGetOpenTablesUseCase
{
    Task<ResponseTableJson> GetOpenTablesAsync();
}