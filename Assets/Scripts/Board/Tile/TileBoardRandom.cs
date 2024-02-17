using UnityEngine;
using UnityEngine.Assertions;

public class TileBoardRandom : TileBoard
{
    public override void PlayToken(in byte playerId, in byte column)
    {
        Assert.IsTrue(IsValidPlayerId(playerId), "Invalid player id playing a token");

        byte newCol = (byte)(Mathf.Abs(column + (Random.value*6 - 3)) % width);

        byte nextAvailableRow = CalculateNextAvailableRow(newCol);
        if (nextAvailableRow == height)
            return;

        BoardCoordinate playedCoordinate = new(nextAvailableRow, newCol);
        PlaceToken(playerId, playedCoordinate);
        UpdateState(playerId, playedCoordinate);
    }
}
