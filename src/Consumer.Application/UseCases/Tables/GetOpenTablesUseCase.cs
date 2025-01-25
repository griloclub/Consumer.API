using AutoMapper;
using Consumer.Application.UseCases.Tables.Interface;
using Consumer.Communication.Response;
using Consumer.Domain.Repositories.Tables;
using Consumer.Domain.Services;

namespace Consumer.Application.UseCases.Tables;
public class GetOpenTablesUseCase : IGetOpenTablesUseCase
{
    private readonly ITablesReadOnlyRepository _tablesReadOnlyRepository;
    private readonly IMapper _mapper;
    private readonly ILoggedUser _loggedUser;

    public GetOpenTablesUseCase(ITablesReadOnlyRepository tablesReadOnlyRepository, IMapper mapper, ILoggedUser loggedUser)
    {
        _tablesReadOnlyRepository = tablesReadOnlyRepository;
        _mapper = mapper;
        _loggedUser = loggedUser;
    }

    public async Task<ResponseTableJson> GetOpenTablesAsync()
    {
        var loggedUser = await _loggedUser.GetUser();
        var myOpenTables = await _tablesReadOnlyRepository.GetOpenTables(loggedUser);
        var allTables = await _tablesReadOnlyRepository.GetAllTables();

        return new ResponseTableJson
        {
            MyOpenTables = new ResponseShortTableJson
            {
                Quantity = myOpenTables.Count,
                Tables = _mapper.Map<List<ResponseInformationTableJson>>(myOpenTables)
            },
            AllTables = new ResponseShortTableJson
            {
                Quantity = allTables.Count,
                Tables = _mapper.Map<List<ResponseInformationTableJson>>(allTables)
            }
        };
    }
}