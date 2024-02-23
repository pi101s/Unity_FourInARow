using System.Collections.Generic;

namespace FIAR
{
    public class HorizontalWinEvaluator : WinEvaluator
    {
        public override WinEvaluationResult Evaluate(in BoardGrid grid, in int lastTurnPlayer)
        {
            EvaluationData evaluationData = new()
            {
                winCombinations = new List<WinCombination>(),
                grid = grid
            };

            Evaluate(evaluationData);
            return WinEvaluation.CreateWinEvaluationResult(evaluationData.winCombinations.ToArray());
        }

        private void Evaluate(in EvaluationData evaluationData)
        {
            for (evaluationData.row = 0; evaluationData.row < evaluationData.grid.height; ++evaluationData.row)
                EvaluateRow(evaluationData);
        }

        private void EvaluateRow(in EvaluationData evaluationData)
        {
            BoardGrid grid = evaluationData.grid;
            evaluationData.tokensCount = 1;
            evaluationData.playerBeingEvaluated = grid[evaluationData.row, 0];

            for (evaluationData.column = 1; evaluationData.column < grid.width; ++evaluationData.column)
                EvaluateCell(evaluationData);

            if (evaluationData.tokensCount >= tokenCountToWin)
                CreateWinCombination(evaluationData);
        }

        private void EvaluateCell(in EvaluationData evaluationData)
        {
            BoardGrid grid = evaluationData.grid;
            int row = evaluationData.row;
            int column = evaluationData.column;
            int playerBeingEvaluateId = evaluationData.playerBeingEvaluated;

            if (grid[row, column] >= 0 && grid[row, column] == playerBeingEvaluateId)
                ++evaluationData.tokensCount;
            else
                FinishEvaluatingPlayer(evaluationData);
        }

        private void FinishEvaluatingPlayer(in EvaluationData evaluationData)
        {
            BoardGrid grid = evaluationData.grid;
            int row = evaluationData.row;
            int column = evaluationData.column;

            if (evaluationData.tokensCount >= tokenCountToWin)
                CreateWinCombination(evaluationData);

            evaluationData.playerBeingEvaluated = grid[row, column];
            evaluationData.tokensCount = 1;
        }

        private void CreateWinCombination(in EvaluationData evaluationData)
        {
            int tokensCount = evaluationData.tokensCount;
            int column = evaluationData.column;
            int row = evaluationData.row;
            int player = evaluationData.playerBeingEvaluated;

            BoardCoordinate[] coordinates = new BoardCoordinate[tokensCount];
            for (int i = 0; i < tokensCount; i++)
                coordinates[i] = new BoardCoordinate(row, (int)(column - tokensCount + i));
            evaluationData.winCombinations.Add(new WinCombination(player, coordinates));
        }
    }
}
