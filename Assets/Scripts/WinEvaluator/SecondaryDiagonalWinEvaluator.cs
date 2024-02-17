using System.Collections.Generic;

public class SecondaryDiagonalWinEvaluator : WinEvaluator
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
        for (byte column = (byte)(evaluationData.grid.width - 1); column < 255; --column)
        {
            evaluationData.row = 0;
            evaluationData.column = column;
            EvaluateDiagonal(evaluationData);
        }

        for (byte row = 0; row < evaluationData.grid.height; ++row)
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
            coordinates[i] = new BoardCoordinate((byte)(row - tokensCount + i), (byte)(column - tokensCount + i));
        evaluationData.winCombinations.Add(new WinCombination(player, coordinates));
    }
}