using UnityEngine;
using UnityEngine.Assertions;

public class TileBoard : Board
{
    [SerializeField]
    private float _speed = 25f;

    public override void Reset()
    {
        for (int row = 0; row < height; row++)
            for (int column = 0; column < width; column++)
                ResetCell(row, column);
    }

    protected virtual void ResetCell(in int row, in int column)
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

    protected void UpdateState(in int playerId, in BoardCoordinate playedCoordinate) {
        _lastPlayedCoordinate = playedCoordinate;
        _grid[playedCoordinate.row, playedCoordinate.column] = playerId;
    }


    public override void PlayToken(in int playerId, in int column)
    {
        Assert.IsTrue(IsValidPlayerId(playerId), "Invalid player id playing a token");

        int nextAvailableRow = CalculateNextAvailableRow(column);
        if (nextAvailableRow == height)
            return;

        BoardCoordinate playedCoordinate = new(nextAvailableRow, column);

        PlaceToken(playerId, playedCoordinate);
        UpdateState(playerId, playedCoordinate);
    }

    protected int CalculateNextAvailableRow(in int column)
    {
        int row;
        for (row = 0; row < height && !IsEmptySpace(new BoardCoordinate(row, column)); ++row) ;
        return row;
    }

    protected Token PlaceToken(in int playerId, in BoardCoordinate coordinate) {
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