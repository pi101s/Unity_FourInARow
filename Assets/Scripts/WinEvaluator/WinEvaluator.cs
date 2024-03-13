using UnityEngine;

namespace FIAR
{
    public abstract class WinEvaluator : MonoBehaviour
    {
        public abstract WinEvaluationResult Evaluate(in BoardGrid grid, in int lastTurnPlayer);
    }
}
