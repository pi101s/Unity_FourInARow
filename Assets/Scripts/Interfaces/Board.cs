public interface Board
{
    void PlayToken(byte playerId, byte column);
    BoardCoordinate GetLastPlayedCoordinate();
    byte[,] GetGrid();
}