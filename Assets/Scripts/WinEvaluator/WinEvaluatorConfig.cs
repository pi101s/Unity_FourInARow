public readonly struct WinEvaluatorConfig
{
    public readonly string name;
    public readonly int tokenCountToWin;

    public WinEvaluatorConfig(in string name, in int tokenCountToWin)
    {
        this.name = name;
        this.tokenCountToWin = tokenCountToWin;
    }
}