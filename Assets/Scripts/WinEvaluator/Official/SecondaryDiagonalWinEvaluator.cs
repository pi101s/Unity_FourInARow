using System.Collections.Generic;

namespace FIAR
{
    public class SecondaryDiagonalWinEvaluator : WinEvaluator
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
            for (int column = evaluationData.grid.width - 1; column >= 0; --column)
            {
                evaluationData.row = 0;
                evaluationData.column = column;
                EvaluateDiagonal(evaluationData);
            }

            for (int row = 0; row < evaluationData.grid.height; ++row)
            {
                evaluationData.row = row;
                evaluationData.column = 0;
                EvaluateDiagonal(evaluationData);
            }
        }

        private void EvaluateDiagonal(in EvaluationData evaluationData)
        {
            BoardGrid grid = evaluationData.grid;
            evaluationData.tokensCount = 0;
            evaluationData.playerBeingEvaluated = grid[evaluationData.row, evaluationData.column];

            for (;
                evaluationData.row < grid.height && evaluationData.column < grid.width;
                ++evaluationData.row, ++evaluationData.column
             )
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
                coordinates[i] = new BoardCoordinate((int)(row - tokensCount + i), (int)(column - tokensCount + i));
            evaluationData.winCombinations.Add(new WinCombination(player, coordinates));
        }
    }
}