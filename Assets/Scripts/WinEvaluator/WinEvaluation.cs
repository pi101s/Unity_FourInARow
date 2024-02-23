using System.Collections.Generic;

namespace FIAR
{
    public class WinEvaluation
    {
        public static WinEvaluationResult CombineResults(params WinEvaluationResult[] evaluationResults)
        {
            if (evaluationResults == null || evaluationResults.Length <= 0)
                throw new System.Exception("It is not possible to combine win evaluation results from null or from an empty list");

            List<WinCombination> winCombinationsList = new();
            foreach (WinEvaluationResult evaluationResult in evaluationResults)
                if (evaluationResult.winCombinations.Length > 0)
                    winCombinationsList.AddRange(evaluationResult.winCombinations);

            WinCombination[] winCombinations = winCombinationsList.ToArray();
            int winnerId = CalculateWinner(winCombinations);
            EMatchResult matchResult = CalculateMatchResult(winCombinations);
            return new WinEvaluationResult(winCombinations, winnerId, matchResult);
        }

        public static WinEvaluationResult CreateWinEvaluationResult(in WinCombination[] winCombinations)
        {
            int winnerId = CalculateWinner(winCombinations);
            EMatchResult matchResult = CalculateMatchResult(winCombinations);
            return new WinEvaluationResult(winCombinations, winnerId, matchResult);
        }

        private static int CalculateWinner(in WinCombination[] winCombinations)
        {
            int numberOfWinners = CalculateNumberOfWinners(winCombinations);
            return numberOfWinners == 1 ? winCombinations[0].winnerId : WinEvaluationResult.NO_WINNER;
        }

        private static int CalculateNumberOfWinners(in WinCombination[] winCombinations)
        {
            HashSet<int> winners = new();
            foreach (WinCombination winCombination in winCombinations)
                winners.Add(winCombination.winnerId);
            return winners.Count;
        }

        private static EMatchResult CalculateMatchResult(in WinCombination[] winCombinations)
        {
            EMatchResult result = EMatchResult.NONE;
            if (CalculateWinner(winCombinations) != WinEvaluationResult.NO_WINNER)
                result = EMatchResult.WIN;
            else if (CalculateNumberOfWinners(winCombinations) > 1)
                result = EMatchResult.DRAW;

            return result;
        }
    }
}
