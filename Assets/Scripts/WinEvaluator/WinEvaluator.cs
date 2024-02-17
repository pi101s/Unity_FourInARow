using UnityEngine;

public abstract class WinEvaluator : MonoBehaviour
{
    private static readonly byte DEFAULT_TOKEN_COUNT_TO_WIN = 4;

    private byte _tokenCountToWin = 0;
    public byte tokenCountToWin
    {
        get
        {
            if (_tokenCountToWin == 0)
                return DEFAULT_TOKEN_COUNT_TO_WIN;
            else
                return _tokenCountToWin;
        }

        set
        {
            if (_tokenCountToWin != 0)
                throw new System.Exception("The token count to win of an evaluator cannot bet set more than once");
            if (value == 0)
                return;

            _tokenCountToWin = value;
            OnTokenCountToWinSet();
        }
    }

    public abstract WinEvaluationResult Evaluate(in BoardGrid grid, in byte lastTurnPlayer, in byte maxPlayerId);
    protected virtual void OnTokenCountToWinSet() { }
}
