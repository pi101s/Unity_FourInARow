using UnityEngine;
using UnityEngine.Tilemaps;

public class TileBoardShape : BoardShape
{
    private const string FRAME_PREFIX = "frame";

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

    [SerializeField]
    private Tilemap _tileMap;

    void Awake()
    {
        _tileMap.CompressBounds();
    }

    public override byte width => (byte)_tileMap.size.x;
    public override byte height => (byte)_tileMap.size.y;

    public override Vector3 GetPosition(BoardCoordinate coordinate)
    {
        LocalCoordinate localCoordinate = GetLocalCoordinate(coordinate);
        return _tileMap.GetCellCenterLocal(new Vector3Int(localCoordinate.column, localCoordinate.row, 0));
    }

    public override bool IsEmpty(BoardCoordinate coordinate)
    {
        return IsTile(coordinate) && IsFrame(coordinate);
    }

    private bool IsTile(BoardCoordinate coordinate)
    {
        LocalCoordinate localCoordinate = GetLocalCoordinate(coordinate);
        return _tileMap.HasTile(new Vector3Int(localCoordinate.column, localCoordinate.row, 0));
    }

    private bool IsFrame(BoardCoordinate coordinate) {
        LocalCoordinate localCoordinate = GetLocalCoordinate(coordinate);
        TileBase tile = _tileMap.GetTile(new Vector3Int(localCoordinate.column, localCoordinate.row, 0));
        return tile.name.ToLower().StartsWith(FRAME_PREFIX);
    }

    private LocalCoordinate GetLocalCoordinate(BoardCoordinate coordinate)
    {
        return new LocalCoordinate(GetLocalRow(coordinate.row), GetLocalColumn(coordinate.column));
    }

    private int GetLocalRow(int row)
    {
        return _tileMap.cellBounds.min.y + row;
    }

    private int GetLocalColumn(int column)
    {
        return _tileMap.cellBounds.min.x + column;
    }
}
