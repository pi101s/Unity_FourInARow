using System.IO;
using UnityEngine;

public class WinEvaluatorFactory : MonoBehaviour
{
    public WinEvaluator CreateWinEvaluator(WinEvaluatorConfig config)
    {
        WinEvaluator winEvaluator = WinEvaluatorDatabase.GetWinEvaluator(config.name);
        if (winEvaluator != null)
            return Instantiate(winEvaluator, Vector3.zero, Quaternion.identity);

        throw new FileNotFoundException("Could not find the win evaluator " + config.name);
    }
}
