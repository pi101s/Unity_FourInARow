using System.Text;

public enum EMatchResult
{
    WIN, DRAW, NONE
}
public readonly struct WinEvaluationResult
{
    public static readonly byte NO_WINNER = 255;

    public readonly WinCombination[] winCombinations;
    public readonly byte winnerId;
    public readonly EMatchResult matchResult;

    public WinEvaluationResult(in WinCombination[] winCombinations, in byte winnerId, in EMatchResult matchResult)
    {
        this.winCombinations = winCombinations;
        this.winnerId = winnerId;
        this.matchResult = matchResult;
    }

    public override string ToString()
    {
        StringBuilder sb = new($"Winner: {winnerId}");
        sb.AppendLine();
        foreach (WinCombination winCombination in winCombinations)
        {
            sb.AppendLine(winCombination.ToString());
        }

        return sb.ToString();
    }
}

public readonly struct WinCombination
{
    public readonly byte winnerId;
    public readonly BoardCoordinate[] coordinates;

    public WinCombination(in byte winnerId, in BoardCoordinate[] coordinates) {
        this.winnerId = winnerId;
        this.coordinates = coordinates;
    }

    public override string ToString()
    {
        StringBuilder sb = new($"{winnerId} - ");
        foreach (BoardCoordinate coordinate in coordinates)
            sb.Append($"({coordinate.row}, {coordinate.column}) ");
        sb.Remove(sb.Length - 1, 1);

        return sb.ToString();
    }
}