public readonly struct WinEvaluatorConfig
{
    public readonly string name;
    public readonly byte tokenCountToWin;

    public WinEvaluatorConfig(in string name, in byte tokenCountToWin)
    {
        this.name = name;
        this.tokenCountToWin = tokenCountToWin;
    }
}