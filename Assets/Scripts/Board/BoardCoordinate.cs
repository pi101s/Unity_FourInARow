public readonly struct BoardCoordinate
{
    public readonly byte row;
    public readonly byte column;

    public BoardCoordinate(byte row, byte column)
    {
        this.row = row;
        this.column = column;
    }
}