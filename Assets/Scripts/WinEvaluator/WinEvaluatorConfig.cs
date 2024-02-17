public readonly struct WinEvaluatorConfig
{
    public readonly string name;
    public readonly byte tokenCountToWin;

    public WinEvaluatorConfig(string name, byte tokenCountToWin)
    {
        this.name = name;
        this.tokenCountToWin = tokenCountToWin;
    }
}