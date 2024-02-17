using System.Collections.Generic;

public class EvaluationData
{
    public List<WinCombination> winCombinations;
    public BoardGrid grid;
    public byte row;
    public byte column;
    public byte playerBeingEvaluated;
    public byte tokensCount;
    public byte maxPlayerId;
}