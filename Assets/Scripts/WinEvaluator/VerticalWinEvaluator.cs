using System.Collections.Generic;

public class VerticalWinEvaluator : WinEvaluator
{
    private readonly List<WinCombination> _winCombinations = new();

    public override WinEvaluationResult Evaluate(BoardGrid grid, byte lastTurnPlayer, byte maxPlayerId)
    {
        _winCombinations.Clear();
        EvaluationData evaluationData = new()
        {
            grid = grid,
            maxPlayerId = maxPlayerId
        };

        Evaluate(evaluationData);

        byte numberOfWinners = GetNumberOfWinners();
        byte winner = numberOfWinners == 1 ? _winCombinations[0].playerId : NO_WINNER;
        return new WinEvaluationResult(_winCombinations.ToArray(), winner);
    }

    private void Evaluate(EvaluationData evaluationData)
    {
        for (byte column = 0; column < evaluationData.grid.width; ++column)
        {
            evaluationData.column = column;
            EvaluateColumn(evaluationData);
        }
    }

    private void EvaluateColumn(EvaluationData evaluationData)
    {
        BoardGrid grid = evaluationData.grid;
        evaluationData.tokensInRow = 1;
        evaluationData.playerBeingEvaluated = grid[0, evaluationData.column];

        for (byte row = 1; row < grid.height; ++row)
        {
            evaluationData.row = row;
            EvaluateCell(evaluationData);
        }

        evaluationData.row = grid.height;
        if (evaluationData.tokensInRow >= NUMBER_OF_TOKENS_TO_WIN)
            CreateWinCombination(evaluationData);
    }

    private void EvaluateCell(EvaluationData evaluationData)
    {
        BoardGrid grid = evaluationData.grid;
        byte row = evaluationData.row;
        byte column = evaluationData.column;
        byte maxPlayerId = evaluationData.maxPlayerId;
        byte playerBeingEvaluateId = evaluationData.playerBeingEvaluated;

        if (grid[row, column] <= maxPlayerId && grid[row, column] == playerBeingEvaluateId)
            ++evaluationData.tokensInRow;
        else
            FinishEvaluatingPlayer(evaluationData);
    }

    private void FinishEvaluatingPlayer(EvaluationData evaluationData)
    {
        BoardGrid grid = evaluationData.grid;
        byte row = evaluationData.row;
        byte column = evaluationData.column;

        if (evaluationData.tokensInRow >= NUMBER_OF_TOKENS_TO_WIN)
            CreateWinCombination(evaluationData);

        evaluationData.playerBeingEvaluated = grid[row, column];
        evaluationData.tokensInRow = 1;
    }

    private void CreateWinCombination(EvaluationData evaluationData)
    {
        byte tokensInRow = evaluationData.tokensInRow;
        byte row = evaluationData.row;
        byte column = evaluationData.column;
        byte player = evaluationData.playerBeingEvaluated;

        BoardCoordinate[] coordinates = new BoardCoordinate[tokensInRow];
        for (byte i = 0; i < tokensInRow; i++)
            coordinates[i] = new BoardCoordinate((byte)(row - i), column);
        _winCombinations.Add(new WinCombination(player, coordinates));
    }

    private byte GetNumberOfWinners()
    {
        HashSet<byte> winners = new();
        foreach (WinCombination winCombination in _winCombinations)
            winners.Add(winCombination.playerId);
        return (byte)winners.Count;
    }
}
