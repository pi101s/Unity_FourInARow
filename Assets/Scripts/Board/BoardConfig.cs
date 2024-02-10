public readonly struct BoardConfig
{
    public readonly string boardName;
    public readonly string shapeName;

    public BoardConfig(string boardName, string shapeName)
    {
        this.boardName = boardName;
        this.shapeName = shapeName;
    }
}