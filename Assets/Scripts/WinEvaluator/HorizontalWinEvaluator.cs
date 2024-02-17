using System.Collections.Generic;

public class HorizontalWinEvaluator : WinEvaluator
{
    public override WinEvaluationResult Evaluate(in BoardGrid grid, in byte lastTurnPlayer, in byte maxPlayerId)
    {
        EvaluationData evaluationData = new()
        {
            winCombinations = new List<WinCombination>(),
            grid = grid,
            maxPlayerId = maxPlayerId
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
        byte row = evaluationData.row;
        byte column = evaluationData.column;
        byte maxPlayerId = evaluationData.maxPlayerId;
        byte playerBeingEvaluateId = evaluationData.playerBeingEvaluated;

        if (grid[row, column] <= maxPlayerId && grid[row, column] == playerBeingEvaluateId)
            ++evaluationData.tokensCount;
        else
            FinishEvaluatingPlayer(evaluationData);
    }

    private void FinishEvaluatingPlayer(in EvaluationData evaluationData)
    {
        BoardGrid grid = evaluationData.grid;
        byte row = evaluationData.row;
        byte column = evaluationData.column;

        if (evaluationData.tokensCount >= tokenCountToWin)
            CreateWinCombination(evaluationData);

        evaluationData.playerBeingEvaluated = grid[row, column];
        evaluationData.tokensCount = 1;
    }

    private void CreateWinCombination(in EvaluationData evaluationData)
    {
        byte tokensCount = evaluationData.tokensCount;
        byte column = evaluationData.column;
        byte row = evaluationData.row;
        byte player = evaluationData.playerBeingEvaluated;

        BoardCoordinate[] coordinates = new BoardCoordinate[tokensCount];
        for (byte i = 0; i < tokensCount; i++)
            coordinates[i] = new BoardCoordinate(row, (byte)(column - tokensCount + i));
        evaluationData.winCombinations.Add(new WinCombination(player, coordinates));
    }
}
