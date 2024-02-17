using UnityEngine;

public class WinEvaluatorDatabase
{
    private static readonly string WIN_EVALUATORS_FOLDER = "WinEvaluators";
    private static Object[] _winEvaluators = null;

    public static WinEvaluator GetWinEvaluator(in string name)
    {
        LoadWinEvaluators();
        return FindWinEvaluatorByName(name);
    }

    private static WinEvaluator FindWinEvaluatorByName(in string name) {
        foreach (Object winEvaluator in _winEvaluators)
            if (winEvaluator.name == name)
                return (WinEvaluator)winEvaluator;
        return null;
    }

    private static void LoadWinEvaluators()
    {
        _winEvaluators ??= Resources.LoadAll(WIN_EVALUATORS_FOLDER, typeof(WinEvaluator));
    }
}
