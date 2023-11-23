using Chess.Domain.Games.Enums;
using Chess.Domain.Identity.Entities;

namespace Chess.Application.Games.DTO;

public sealed class GameDto
{
    public Guid Id { get; set; }
    public User? WhitePlayer { get; set; }
    public User? BlackPlayer { get; set; }
    public ResultEnum Result { get; set; }
    public int? WhiteRanking { get; set; }
    public int? BlackRanking { get; set; }
}