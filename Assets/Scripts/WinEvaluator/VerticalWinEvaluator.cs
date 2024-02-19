using System.Collections.Generic;

public class VerticalWinEvaluator : WinEvaluator
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
        for (evaluationData.column = 0; evaluationData.column < evaluationData.grid.width; ++evaluationData.column)
            EvaluateColumn(evaluationData);
    }

    private void EvaluateColumn(in EvaluationData evaluationData)
    {
        BoardGrid grid = evaluationData.grid;
        evaluationData.tokensCount = 1;
        evaluationData.playerBeingEvaluated = grid[0, evaluationData.column];

        for (evaluationData.row = 1; evaluationData.row < grid.height; ++evaluationData.row)
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
        int row = evaluationData.row;
        int column = evaluationData.column;
        int player = evaluationData.playerBeingEvaluated;

        BoardCoordinate[] coordinates = new BoardCoordinate[tokensCount];
        for (int i = 0; i < tokensCount; i++)
            coordinates[i] = new BoardCoordinate(row - tokensCount + i, column);
        evaluationData.winCombinations.Add(new WinCombination(player, coordinates));
    }
}
