public readonly struct BoardCoordinate
{
    public readonly int row;
    public readonly int column;

    public BoardCoordinate(in int row, in int column)
    {
        this.row = row;
        this.column = column;
    }

    public override string ToString()
    {
        return $"({row}, {column})";
    }
}