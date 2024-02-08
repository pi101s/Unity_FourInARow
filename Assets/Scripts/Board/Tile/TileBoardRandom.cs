using UnityEngine;
using UnityEngine.Assertions;

public class TileBoardRandom : TileBoard
{
    public override void PlayToken(byte playerId, byte column)
    {
        Assert.IsTrue(IsValidPlayerId(playerId), "Invalid player id playing a token");

        byte newCol = (byte)(Mathf.Abs(column + (Random.value*6 - 3)) % _width);

        byte nextAvailableRow = CalculateNextAvailableRow(newCol);
        if (nextAvailableRow == _height)
            return;

        BoardCoordinate playedCoordinate = new(nextAvailableRow, newCol);
        Token token = PlaceToken(playerId, playedCoordinate);
        token.OnPositionReached(TokenPositionReachedHandler);
        UpdateState(playerId, playedCoordinate);
    }

    private void TokenPositionReachedHandler()
    {
        Debug.Log("Random Board token placed");
    }
}
