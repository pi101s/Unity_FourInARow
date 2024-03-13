using System.Text;

namespace FIAR
{
    public enum EMatchResult
    {
        WIN, DRAW, NONE
    }

    public readonly struct WinEvaluationResult
    {
        public const int NO_WINNER = -1;

        public readonly WinCombination[] winCombinations;
        public readonly int winnerId;
        public readonly EMatchResult matchResult;

        public WinEvaluationResult(in WinCombination[] winCombinations, in int winnerId, in EMatchResult matchResult)
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
        public readonly int winnerId;
        public readonly BoardCoordinate[] coordinates;

        public WinCombination(in int winnerId, in BoardCoordinate[] coordinates)
        {
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
}