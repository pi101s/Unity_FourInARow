using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Tilemaps;

public class BaseBoard : MonoBehaviour, Board
{
    const float TOKEN_SPEED = 20f;
    const byte BLOCKED_SPACE = 255;
    const byte EMPTY_SPACE = 254;

    [SerializeField]
    private Tilemap _tileMap;

    [SerializeField]
    private Token _token;

    private struct LocalCoordinate
    {
        public int row;
        public int column;

        public LocalCoordinate(int row, int column)
        {
            this.row = row;
            this.column = column;
        }
    }

    private byte _width = 0;
    private byte _height = 0;
    private byte[,] _grid;
    private BoardCoordinate _lastPlayedCoordinate;

    void Start()
    {
        _tileMap.CompressBounds();
        _width = (byte)_tileMap.size.x;
        _height = (byte)_tileMap.size.y;
        _grid = new byte[_height, _width];
        ResetGrid();
    }

    public byte[,] GetGrid()
    {
        return _grid;
    }

    public BoardCoordinate GetLastPlayedCoordinate()
    {
        return _lastPlayedCoordinate;
    }

    public void PlayToken(byte playerId, byte column)
    {
        Assert.IsTrue(IsValidPlayerId(playerId));

        byte nextAvailableRow = CalculateNextAvailableRow(column);

        if (nextAvailableRow == _height)
            return;

        BoardCoordinate coordinate = new(nextAvailableRow, column);
        Token newToken = Instantiate(_token, GetTokenInitialPosition(coordinate), Quaternion.identity);
        newToken.OnPositionReached(TokenPositionReachedHandler);
        newToken.MoveTo(GetTokenFinalPosition(coordinate), TOKEN_SPEED);
        _lastPlayedCoordinate = coordinate;
        _grid[coordinate.row, coordinate.column] = playerId;
    }

    private bool IsValidPlayerId(byte playerId)
    {
        return playerId != EMPTY_SPACE && playerId != BLOCKED_SPACE;
    }

    private byte CalculateNextAvailableRow(byte column)
    {
        byte row;
        for (row = 0; row < _height && !IsEmptySpace(new BoardCoordinate(row, column)); ++row) ;
        return row;
    }

    private bool IsEmptySpace(BoardCoordinate coordinate)
    {
        return _grid[coordinate.row, coordinate.column] == EMPTY_SPACE;
    }

    private LocalCoordinate CalculateLocalCoordinate(byte row, byte column)
    {
        return new LocalCoordinate(GetLocalRow(row), GetLocalColumn(column));
    }

    private int GetLocalColumn(int column)
    {
        return _tileMap.cellBounds.min.x + column;
    }

    private int GetLocalRow(int row)
    {
        return _tileMap.cellBounds.min.y + row;
    }

    private Vector3 GetTokenInitialPosition(BoardCoordinate coordinate)
    {
        int localColumn = GetLocalColumn(coordinate.column);
        Vector3Int initialCellPosition = new(localColumn, _tileMap.cellBounds.yMax, 0);
        Vector3 initialPosition = _tileMap.GetCellCenterLocal(initialCellPosition);
        return initialPosition;
    }

    private Vector3 GetTokenFinalPosition(BoardCoordinate coordinate)
    {
        LocalCoordinate localCoordinate = CalculateLocalCoordinate(coordinate.row, coordinate.column);
        Vector3Int finalCellPosition = new(localCoordinate.column, localCoordinate.row, 0);
        Vector3 finalPosition = _tileMap.GetCellCenterLocal(finalCellPosition);
        return finalPosition;
    }

    private void TokenPositionReachedHandler()
    {
        Debug.Log("Position reached");
    }

    private void ResetGrid()
    {
        for (byte row = 0; row < _height; row++)
        {
            for (byte column = 0; column < _width; column++)
            {
                LocalCoordinate localCoordinate = CalculateLocalCoordinate(row, column);
                if (_tileMap.HasTile(new Vector3Int(localCoordinate.column, localCoordinate.row, 0)))
                    _grid[row, column] = EMPTY_SPACE;
                else
                    _grid[row, column] = BLOCKED_SPACE;
            }
        }
    }
}
