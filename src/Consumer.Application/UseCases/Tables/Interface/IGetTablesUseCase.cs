using Consumer.Communication.Response;

namespace Consumer.Application.UseCases.Tables.Interface;
public interface IGetTablesUseCase
{
    Task<ResponseTableJson> GetTablesAsync();
}