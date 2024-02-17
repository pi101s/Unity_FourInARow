public readonly struct BoardCoordinate
{
    public readonly byte row;
    public readonly byte column;

    public BoardCoordinate(in byte row, in byte column)
    {
        this.row = row;
        this.column = column;
    }

    public override string ToString()
    {
        return $"({row}, {column})";
    }
}