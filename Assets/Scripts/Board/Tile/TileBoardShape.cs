using UnityEngine;
using UnityEngine.Tilemaps;

namespace FIAR
{
    public class TileBoardShape : BoardShape
    {
        private const string FRAME_PREFIX = "frame";

        protected readonly struct LocalCoordinate
        {
            public readonly int row;
            public readonly int column;

            public LocalCoordinate(in int row, in int column)
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

        public override int width => _tileMap.size.x;
        public override int height => _tileMap.size.y;

        public override Vector3 GetPosition(in BoardCoordinate coordinate)
        {
            LocalCoordinate localCoordinate = GetLocalCoordinate(coordinate);
            return _tileMap.GetCellCenterLocal(new Vector3Int(localCoordinate.column, localCoordinate.row, 0));
        }

        public override bool IsEmpty(in BoardCoordinate coordinate)
        {
            return IsTile(coordinate) && IsFrame(coordinate);
        }

        private bool IsTile(in BoardCoordinate coordinate)
        {
            LocalCoordinate localCoordinate = GetLocalCoordinate(coordinate);
            return _tileMap.HasTile(new Vector3Int(localCoordinate.column, localCoordinate.row, 0));
        }

        private bool IsFrame(in BoardCoordinate coordinate) {
            LocalCoordinate localCoordinate = GetLocalCoordinate(coordinate);
            TileBase tile = _tileMap.GetTile(new Vector3Int(localCoordinate.column, localCoordinate.row, 0));
            return tile.name.ToLower().StartsWith(FRAME_PREFIX);
        }

        protected LocalCoordinate GetLocalCoordinate(in BoardCoordinate coordinate)
        {
            return new LocalCoordinate(GetLocalRow(coordinate.row), GetLocalColumn(coordinate.column));
        }

        private int GetLocalRow(in int row)
        {
            return _tileMap.cellBounds.min.y + row;
        }

        private int GetLocalColumn(in int column)
        {
            return _tileMap.cellBounds.min.x + column;
        }
    }
}
