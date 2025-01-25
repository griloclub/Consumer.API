using AutoMapper;
using Consumer.Application.UseCases.Tables.Interface;
using Consumer.Application.UseCases.Tables.Validator;
using Consumer.Communication.Response;
using Consumer.Domain.Repositories.Tables;
using Consumer.Exception;
using Consumer.Exception.ExceptionBase;

namespace Consumer.Application.UseCases.Tables;
public class GetTableDetailsUseCase : IGetTableDetailsUseCase
{
    private readonly ITablesReadOnlyRepository _tablesReadOnlyRepository;
    private readonly IMapper _mapper;

    public GetTableDetailsUseCase(ITablesReadOnlyRepository tablesReadOnlyRepository, IMapper mapper)
    {
        _tablesReadOnlyRepository = tablesReadOnlyRepository;
        _mapper = mapper;
    }

    public async Task<ResponseTableDetailsJson> GetTableDetailsAsync(long id)
    {
        var detail = await _tablesReadOnlyRepository.GetTableDetails(id);
        if (detail is null)
            throw new NotFoundException(ResourceErrorMessages.DETAILS_NOT_FOUND);

        Validate(detail);
        return _mapper.Map<ResponseTableDetailsJson>(detail);
    }

    private void Validate(Domain.Entities.Table detail)
    {
        var validator = new TableDetailsValidator();

        var result = validator.Validate(detail);
        if (!result.IsValid)
        {
            var objErros = new List<Object>();
            foreach (var item in result.Errors)
            {
                var objModel = new { item.PropertyName, Message = item.ErrorMessage };
                objErros.Add(objModel);
            }
            throw new ErrorOnValidationException(objErros);
        }
    }
}
