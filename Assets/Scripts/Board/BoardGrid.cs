using System.Runtime.CompilerServices;

public readonly struct BoardGrid {
    public readonly byte width;
    public readonly byte height;
    private readonly byte[,] _grid;

    public BoardGrid(in byte[,] grid, in byte width, in byte height) {
        _grid = grid;
        this.width = width;
        this.height = height;
    }

    public byte this[byte row, byte column]
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get { return _grid[row, column]; }
    }
}
