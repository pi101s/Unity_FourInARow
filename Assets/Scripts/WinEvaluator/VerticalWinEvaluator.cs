using System.Collections.Generic;

public class VerticalWinEvaluator : WinEvaluator
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
        byte row = evaluationData.row;
        byte column = evaluationData.column;
        byte player = evaluationData.playerBeingEvaluated;

        BoardCoordinate[] coordinates = new BoardCoordinate[tokensCount];
        for (byte i = 0; i < tokensCount; i++)
            coordinates[i] = new BoardCoordinate((byte)(row - tokensCount + i), column);
        evaluationData.winCombinations.Add(new WinCombination(player, coordinates));
    }
}
