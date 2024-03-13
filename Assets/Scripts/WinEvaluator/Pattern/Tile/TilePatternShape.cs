using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace FIAR
{
    public class TilePatternShape : PatternShape
    {
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

        public override BoardCoordinate[] coodinates
        {
            get {
                List<BoardCoordinate> coordinates = new();
                for (int row = 0; row < height; row++)
                    for (int column = 0; column < width; column++)
                        if (IsInPattern(new BoardCoordinate(row, column)))
                            coordinates.Add(new BoardCoordinate(row, column));
                return coordinates.ToArray();
            }
        }

        public bool IsInPattern(in BoardCoordinate coordinate)
        {
            return IsTile(coordinate);
        }

        private bool IsTile(in BoardCoordinate coordinate)
        {
            LocalCoordinate localCoordinate = GetLocalCoordinate(coordinate);
            return _tileMap.HasTile(new Vector3Int(localCoordinate.column, localCoordinate.row, 0));
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
