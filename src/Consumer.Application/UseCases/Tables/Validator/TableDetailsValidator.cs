using Consumer.Communication.Response;
using Consumer.Exception;
using FluentValidation;

namespace Consumer.Application.UseCases.Tables.Validator;
public class TableDetailsValidator : AbstractValidator<ResponseTableDetailsJson>
{
    public TableDetailsValidator()
    {
        RuleFor(x => x.Items)
            .Must(items => items != null && items.All(i => i.Price > 0 && i.Quantity > 0))
            .WithMessage(ResourceErrorMessages.PRICE_AND_QUANTITY_POSITIVE);
    }
}