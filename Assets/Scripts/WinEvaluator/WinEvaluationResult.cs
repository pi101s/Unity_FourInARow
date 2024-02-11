using System.Diagnostics;
using System.Text;

public enum EGameResult
{
    WIN, DRAW, NONE
}

public readonly struct WinEvaluationResult
{
    public readonly WinCombination[] winCombinations;
    public readonly byte playerId;

    public WinEvaluationResult(WinCombination[] winCombinations, byte playerId)
    {
        this.winCombinations = winCombinations;
        this.playerId = playerId;
    }

    public override string ToString()
    {
        StringBuilder sb = new($"Winner: {playerId}");
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
    public readonly byte playerId;
    public readonly BoardCoordinate[] coordinates;

    public WinCombination(byte playerId, BoardCoordinate[] coordinates) {
        this.playerId = playerId;
        this.coordinates = coordinates;
    }

    public override string ToString()
    {
        StringBuilder sb = new($"{playerId} - ");
        foreach (BoardCoordinate coordinate in coordinates)
            sb.Append($"({coordinate.row}, {coordinate.column}) ");
        sb.Remove(sb.Length - 1, 1);

        return sb.ToString();
    }
}