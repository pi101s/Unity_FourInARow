using System.Runtime.CompilerServices;

namespace FIAR
{
    public readonly struct BoardGrid
    {
        public readonly int width;
        public readonly int height;
        private readonly int[,] _grid;

        public BoardGrid(in int[,] grid, in int width, in int height)
        {
            _grid = grid;
            this.width = width;
            this.height = height;
        }

        public int this[int row, int column]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return _grid[row, column]; }
        }
    }
}
