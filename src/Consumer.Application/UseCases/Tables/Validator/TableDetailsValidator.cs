using Consumer.Domain.Entities;
using Consumer.Exception;
using FluentValidation;

namespace Consumer.Application.UseCases.Tables.Validator;
public class TableDetailsValidator : AbstractValidator<Table>
{
    public TableDetailsValidator()
    {
        RuleFor(x => x.Items)
            .Must(items => items.All(i => i.Price > 0 && i.Quantity > 0))
            .WithMessage(ResourceErrorMessages.PRICE_AND_QUANTITY_POSITIVE);
    }
}