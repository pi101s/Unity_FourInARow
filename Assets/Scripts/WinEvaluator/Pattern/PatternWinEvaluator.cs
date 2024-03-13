using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace FIAR
{
    public class PatternWinEvaluator : WinEvaluator
    {
        private Pattern[] _patterns;

        public Pattern[] patterns {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set {
                if (value.Length == 0)
                    throw new System.Exception("Patterns list from a pattern win evaluator should contain at least one pattern");
                _patterns = value;
            }
        }

        public override WinEvaluationResult Evaluate(in BoardGrid grid, in int lastTurnPlayer)
        {
            PatternEvaluationData evaluationData = new()
            {
                winCombinations = new List<WinCombination>(),
                grid = grid
            };

            Evaluate(evaluationData);
            return WinEvaluation.CreateWinEvaluationResult(evaluationData.winCombinations.ToArray());
        }

        private void Evaluate(in PatternEvaluationData evaluationData)
        {
            foreach (var pattern in _patterns)
            {
                evaluationData.pattern = pattern;
                EvaluatePattern(evaluationData);
            }
        }

        private void EvaluatePattern(in PatternEvaluationData evaluationData)
        {
            BoardGrid grid = evaluationData.grid;
            Pattern pattern = evaluationData.pattern;

            for (evaluationData.initialRow = 0; evaluationData.initialRow <= grid.height - pattern.height; evaluationData.initialRow++)
                for (evaluationData.initialColumn = 0; evaluationData.initialColumn <= grid.width - pattern.width; evaluationData.initialColumn++)
                    EvaluatePatternIncell(evaluationData);
        }

        private void EvaluatePatternIncell(in PatternEvaluationData evaluationData)
        {
            evaluationData.playerBeingEvaluated = evaluationData.grid[evaluationData.initialRow, evaluationData.initialColumn];
            evaluationData.tokensCount = 0;

            foreach (var coordinate in evaluationData.pattern.coordinates)
            {
                evaluationData.row = evaluationData.initialRow + coordinate.row;
                evaluationData.column = evaluationData.initialColumn + coordinate.column;
                EvaluateCell(evaluationData);
            }
        }

        private void EvaluateCell(in PatternEvaluationData evaluationData)
        {
            BoardGrid grid = evaluationData.grid;
            int row = evaluationData.row;
            int column = evaluationData.column;
            int playerBeingEvaluateId = evaluationData.playerBeingEvaluated;

            if (grid[row, column] >= 0 && grid[row, column] == playerBeingEvaluateId)
                ++evaluationData.tokensCount;

            if (evaluationData.tokensCount >= evaluationData.pattern.coordinates.Length)
                CreateWinCombination(evaluationData);
        }

        private void CreateWinCombination(in PatternEvaluationData evaluationData)
        {
            int tokensCount = evaluationData.tokensCount;
            int initialRow = evaluationData.initialRow;
            int initialColumn = evaluationData.initialColumn;
            int player = evaluationData.playerBeingEvaluated;
            Pattern pattern = evaluationData.pattern;

            BoardCoordinate[] coordinates = new BoardCoordinate[tokensCount];
            for (int i = 0; i < tokensCount; i++)
                coordinates[i] = new BoardCoordinate(initialRow + pattern.coordinates[i].row, initialColumn + +pattern.coordinates[i].column);
            evaluationData.winCombinations.Add(new WinCombination(player, coordinates));
        }
    }
}
