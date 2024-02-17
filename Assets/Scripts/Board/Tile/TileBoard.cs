using UnityEngine;
using UnityEngine.Assertions;

public class TileBoard : Board
{
    [SerializeField]
    private float _speed = 25f;

    public override void Reset()
    {
        for (byte row = 0; row < height; row++)
            for (byte column = 0; column < width; column++)
                ResetCell(row, column);
    }

    protected virtual void ResetCell(in byte row, in byte column)
    {
        if (_shape.IsEmpty(new BoardCoordinate(row, column)))
            _grid[row, column] = EMPTY_SPACE;
        else
            _grid[row, column] = BLOCKED_SPACE;
    }

    protected Vector3 GetTokenInitialPosition(in BoardCoordinate coordinate)
    {
        return _shape.GetPosition(new BoardCoordinate(height, coordinate.column));
    }

    protected Vector3 GetTokenFinalPosition(in BoardCoordinate coordinate)
    {
        return _shape.GetPosition(coordinate);
    }

    protected void UpdateState(in byte playerId, in BoardCoordinate playedCoordinate) {
        _lastPlayedCoordinate = playedCoordinate;
        _grid[playedCoordinate.row, playedCoordinate.column] = playerId;
    }


    public override void PlayToken(in byte playerId, in byte column)
    {
        Assert.IsTrue(IsValidPlayerId(playerId), "Invalid player id playing a token");

        byte nextAvailableRow = CalculateNextAvailableRow(column);
        if (nextAvailableRow == height)
            return;

        BoardCoordinate playedCoordinate = new(nextAvailableRow, column);

        PlaceToken(playerId, playedCoordinate);
        UpdateState(playerId, playedCoordinate);
    }

    protected bool IsValidPlayerId(in byte playerId)
    {
        return playerId != EMPTY_SPACE && playerId != BLOCKED_SPACE && playerId < _tokens.Length;
    }

    protected byte CalculateNextAvailableRow(in byte column)
    {
        byte row;
        for (row = 0; row < height && !IsEmptySpace(new BoardCoordinate(row, column)); ++row) ;
        return row;
    }

    protected bool IsEmptySpace(in BoardCoordinate coordinate)
    {
        return _grid[coordinate.row, coordinate.column] == EMPTY_SPACE;
    }

    protected Token PlaceToken(in byte playerId, in BoardCoordinate coordinate) {
        Token prefab = _tokens[playerId];
        Vector3 initialPosition = GetTokenInitialPosition(coordinate);
        Token newToken = Instantiate(prefab, initialPosition, Quaternion.identity, transform);
        newToken.MoveTo(GetTokenFinalPosition(coordinate), _speed);
        newToken.OnFinishedMoving(OnTokenFinishedMoving);
        return newToken;
    }

    private void OnTokenFinishedMoving()
    {
        _onTurnFinishedHandler?.Invoke();
    }
}