public readonly struct BoardGrid {
    public readonly byte width;
    public readonly byte height;
    private readonly byte[,] _grid;

    public BoardGrid(byte[,] grid, byte width, byte height) {
        _grid = grid;
        this.width = width;
        this.height = height;
    }

    public byte GetValueOfCoordiante(BoardCoordinate coordinate) {
        return _grid[coordinate.row, coordinate.column];
    }
}
