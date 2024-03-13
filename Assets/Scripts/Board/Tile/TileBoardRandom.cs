using UnityEngine;
using UnityEngine.Assertions;

namespace FIAR
{
    public class TileBoardRandom : TileBoard
    {
        public override void PlayToken(in int playerId, in int column)
        {
            Assert.IsTrue(IsValidPlayerId(playerId), "Invalid player id playing a token");

            int newCol = (int)(Mathf.Abs(column + (Random.value * 6 - 3)) % width);

            int nextAvailableRow = CalculateNextAvailableRow(newCol);
            if (nextAvailableRow == height)
                return;

            BoardCoordinate playedCoordinate = new(nextAvailableRow, newCol);
            PlaceToken(playerId, playedCoordinate);
            UpdateState(playerId, playedCoordinate);
        }
    }
}
