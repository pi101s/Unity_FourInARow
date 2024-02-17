public readonly struct BoardConfig
{
    public readonly string boardName;
    public readonly string shapeName;

    public BoardConfig(in string boardName, in string shapeName)
    {
        this.boardName = boardName;
        this.shapeName = shapeName;
    }
}