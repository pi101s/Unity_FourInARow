using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Tilemaps;

public class TileBoard : Board
{
    protected const byte BLOCKED_SPACE = 255;
    protected const byte EMPTY_SPACE = 254;
    private const float TOKEN_SPEED = 20f;
    private const string FRAME_PREFIX = "frame";

    [SerializeField]
    private Tilemap _tileMap;

    private readonly struct LocalCoordinate
    {
        public readonly int row;
        public readonly int column;

        public LocalCoordinate(int row, int column)
        {
            this.row = row;
            this.column = column;
        }
    }

    void Start()
    {
        _tileMap.CompressBounds();
        _width = (byte)_tileMap.size.x;
        _height = (byte)_tileMap.size.y;
        _grid = new byte[_height, _width];
        ResetGrid();
    }

    protected virtual void ResetGrid()
    {
        for (byte row = 0; row < _height; row++)
            for (byte column = 0; column < _width; column++)
                ResetGridCell(row, column);
    }

    protected virtual void ResetGridCell(byte row, byte column)
    {
        if (IsEmptyTile(row, column))
            _grid[row, column] = EMPTY_SPACE;
        else
            _grid[row, column] = BLOCKED_SPACE;
    }

    protected bool IsEmptyTile(byte row, byte column) {
        return IsTile(row, column) && IsFrame(row, column);
    }

    protected bool IsTile(byte row, byte column)
    {
        LocalCoordinate localCoordinate = GetLocalCoordinate(row, column);
        return _tileMap.HasTile(new Vector3Int(localCoordinate.column, localCoordinate.row, 0));
    }

    protected bool IsFrame(byte row, byte column) {
        LocalCoordinate localCoordinate = GetLocalCoordinate(row, column);
        TileBase tile = _tileMap.GetTile(new Vector3Int(localCoordinate.column, localCoordinate.row, 0));
        return tile.name.ToLower().StartsWith(FRAME_PREFIX);
    }

    private LocalCoordinate GetLocalCoordinate(byte row, byte column)
    {
        return new LocalCoordinate(GetLocalRow(row), GetLocalColumn(column));
    }

    private int GetLocalRow(int row)
    {
        return _tileMap.cellBounds.min.y + row;
    }

    private int GetLocalColumn(int column)
    {
        return _tileMap.cellBounds.min.x + column;
    }

    protected Vector3 GetTokenInitialPosition(BoardCoordinate coordinate)
    {
        int localColumn = GetLocalColumn(coordinate.column);
        Vector3Int initialCellCoordinate = new(localColumn, _tileMap.cellBounds.yMax, 0);
        Vector3 initialPosition = _tileMap.GetCellCenterLocal(initialCellCoordinate);
        return initialPosition;
    }

    protected Vector3 GetTokenFinalPosition(BoardCoordinate coordinate)
    {
        LocalCoordinate localCoordinate = GetLocalCoordinate(coordinate.row, coordinate.column);
        Vector3Int reversedLocalCoordinate = new(localCoordinate.column, localCoordinate.row, 0);
        Vector3 finalPosition = _tileMap.GetCellCenterLocal(reversedLocalCoordinate);
        return finalPosition;
    }

    protected void UpdateState(byte playerId, BoardCoordinate playedCoordinate) {
        _lastPlayedCoordinate = playedCoordinate;
        _grid[playedCoordinate.row, playedCoordinate.column] = playerId;
    }


    public override void PlayToken(byte playerId, byte column)
    {
        Assert.IsTrue(IsValidPlayerId(playerId), "Invalid player id playing a token");

        byte nextAvailableRow = CalculateNextAvailableRow(column);
        if (nextAvailableRow == _height)
            return;

        BoardCoordinate playedCoordinate = new(nextAvailableRow, column);

        PlaceToken(playerId, playedCoordinate);
        UpdateState(playerId, playedCoordinate);
    }

    protected bool IsValidPlayerId(byte playerId)
    {
        return playerId != EMPTY_SPACE && playerId != BLOCKED_SPACE && playerId < _tokens.Length;
    }

    protected byte CalculateNextAvailableRow(byte column)
    {
        byte row;
        for (row = 0; row < _height && !IsEmptySpace(new BoardCoordinate(row, column)); ++row) ;
        return row;
    }

    protected bool IsEmptySpace(BoardCoordinate coordinate)
    {
        return _grid[coordinate.row, coordinate.column] == EMPTY_SPACE;
    }

    protected Token PlaceToken(byte playerId, BoardCoordinate coordinate) {
        Token prefab = _tokens[playerId];
        Vector3 initialPosition = GetTokenInitialPosition(coordinate);
        Token newToken = Instantiate(prefab, initialPosition, Quaternion.identity, transform);
        newToken.MoveTo(GetTokenFinalPosition(coordinate), TOKEN_SPEED);
        return newToken;
    }
}