using FluentValidation;

namespace Chess.Application.Games.Commands.CreateGame;

public sealed class CreateGameValidator : AbstractValidator<CreateGameCommand>
{
    public CreateGameValidator()
    {
        RuleFor(x => x.Result)
            .IsInEnum();
        
        When(x => x.WhitePlayerId is not null, () =>
        {
            RuleFor(x => x.WhiteRanking)
                .NotNull()
                .GreaterThanOrEqualTo(0);
            RuleFor(x => x.WhitePlayerId)
                .NotEqual(x => x.BlackPlayerId);
        });
        
        When(x => x.BlackPlayerId is not null, () =>
        {
            RuleFor(x => x.BlackRanking)
                .NotNull()
                .GreaterThanOrEqualTo(0);
            RuleFor(x => x.WhitePlayerId)
                .NotEqual(x => x.BlackPlayerId);
        });

        When(x => x.WhitePlayerId is null, () =>
        {
            RuleFor(x => x.WhiteRanking)
                .Null();
        });
        
        When(x => x.BlackPlayerId is null, () =>
        {
            RuleFor(x => x.BlackRanking)
                .Null();
        });
    }
}