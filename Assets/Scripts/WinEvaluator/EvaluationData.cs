using System.Collections.Generic;

namespace FIAR
{
    public class EvaluationData
    {
        public List<WinCombination> winCombinations;
        public BoardGrid grid;
        public int row;
        public int column;
        public int playerBeingEvaluated;
        public int tokensCount;
    }
}