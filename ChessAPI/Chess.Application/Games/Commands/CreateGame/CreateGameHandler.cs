using Chess.Domain.Games.Entities;
using Chess.Domain.Games.Repositories;
using MediatR;

namespace Chess.Application.Games.Commands.CreateGame;

public sealed class CreateGameHandler : IRequestHandler<CreateGameCommand, Guid>
{
    private readonly IGameRepository _gameRepository;
    
    public CreateGameHandler(IGameRepository gameRepository)
    {
        _gameRepository = gameRepository;
    }
        
    public async Task<Guid> Handle(CreateGameCommand request, CancellationToken cancellationToken)
    {
        return await _gameRepository.CreateAsync(request.WhitePlayerId, request.BlackPlayerId, request.Result, request.WhiteRanking, request.BlackRanking, request.PGN, cancellationToken);
    }
}