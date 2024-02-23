using System.IO;
using UnityEngine;

namespace FIAR
{
    public class WinEvaluatorFactory : MonoBehaviour
    {
        public WinEvaluator CreateWinEvaluator(in WinEvaluatorConfig config)
        {
            WinEvaluator winEvaluator = WinEvaluatorDatabase.GetWinEvaluator(config.name);
            WinEvaluator winEvaluatorInstance;
            if (winEvaluator != null)
                winEvaluatorInstance = Instantiate(winEvaluator, Vector3.zero, Quaternion.identity);
            else
                throw new FileNotFoundException("Could not find the win evaluator " + config.name);

            winEvaluatorInstance.tokenCountToWin = config.tokenCountToWin;
            return winEvaluatorInstance;
        }
    }
}
