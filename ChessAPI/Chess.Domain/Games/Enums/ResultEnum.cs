namespace Chess.Domain.Games.Enums;

public enum ResultEnum
{
    WhiteWonCheckmate = 1,
    BlackWonCheckmate = 2,
    WhiteWonResignation = 3,
    BlackWonResignation = 4,
    DrawAgreement = 5,
    DrawStalemate = 6,
    DrawInsufficientMaterial = 7,
    DrawRepetition = 8,
    Draw50MoveRule = 9,
}