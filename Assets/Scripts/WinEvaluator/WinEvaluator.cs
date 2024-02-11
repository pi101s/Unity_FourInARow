using UnityEngine;

public abstract class WinEvaluator : MonoBehaviour
{
    public static readonly byte NO_WINNER = 255;
    protected static readonly byte NUMBER_OF_TOKENS_TO_WIN = 4;

    public abstract WinEvaluationResult Evaluate(BoardGrid grid, byte lastTurnPlayer, byte maxPlayerId);
}
