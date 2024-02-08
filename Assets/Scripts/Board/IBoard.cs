public interface IBoard
{
    void PlayToken(byte playerId, byte column);
    void SetTokens(Token[] token);
    BoardCoordinate GetLastPlayedCoordinate();
    BoardGrid GetGrid();
}