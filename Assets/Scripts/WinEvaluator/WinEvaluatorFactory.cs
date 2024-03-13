using System.IO;
using UnityEngine;

namespace FIAR
{
    public class WinEvaluatorFactory : MonoBehaviour
    {
        public virtual WinEvaluator CreateWinEvaluator(in WinEvaluatorConfig config)
        {
            WinEvaluator winEvaluator = WinEvaluatorDatabase.GetWinEvaluator(config.name);
            WinEvaluator winEvaluatorInstance;
            if (winEvaluator != null)
                winEvaluatorInstance = Instantiate(winEvaluator, Vector3.zero, Quaternion.identity);
            else
                throw new FileNotFoundException("Could not find the win evaluator " + config.name);

            if (winEvaluatorInstance is PatternWinEvaluator)
            {
                PatternShape[] shapes = new TilePatternShape[config.patterns.Length];
                for (int i = 0; i < config.patterns.Length; i++)
                    shapes[i] = Instantiate(TilePatternDatabase.GetShape(config.patterns[i]));
                (winEvaluatorInstance as PatternWinEvaluator).patterns = PatternUtilities.ConvertShapesIntoPatterns(shapes);
                foreach (var shape in shapes)
                    Destroy(shape.gameObject);
            }
            return winEvaluatorInstance;
        }
    }
}
