using UnityEngine;

public class OfficialWinEvaluator : WinEvaluator
{
    [SerializeField]
    WinEvaluator _verticalEvaluator;

    [SerializeField]
    WinEvaluator _horizontalEvaluator;

    [SerializeField]
    WinEvaluator _mainDiagonalEvaluator;

    [SerializeField]
    WinEvaluator _secondaryDiagonalEvaluator;

    private void Awake()
    {
        _verticalEvaluator = Instantiate(_verticalEvaluator, transform);
        _horizontalEvaluator = Instantiate(_horizontalEvaluator, transform);
        _mainDiagonalEvaluator = Instantiate(_mainDiagonalEvaluator, transform);
        _secondaryDiagonalEvaluator = Instantiate(_secondaryDiagonalEvaluator, transform);
    }

    public override WinEvaluationResult Evaluate(in BoardGrid grid, in int lastTurnPlayer)
    {
        WinEvaluationResult verticalEvaluationResult = _verticalEvaluator.Evaluate(grid, lastTurnPlayer);
        WinEvaluationResult horizontalEvaluationResult = _horizontalEvaluator.Evaluate(grid, lastTurnPlayer);
        WinEvaluationResult mainDiagonalEvaluationResult = _mainDiagonalEvaluator.Evaluate(grid, lastTurnPlayer);
        WinEvaluationResult secondaryDiagonalEvaluationResult = _secondaryDiagonalEvaluator.Evaluate(grid, lastTurnPlayer);
        return WinEvaluation.CombineResults(verticalEvaluationResult, horizontalEvaluationResult, mainDiagonalEvaluationResult, secondaryDiagonalEvaluationResult);
    }

    protected override void OnTokenCountToWinSet()
    {
        _verticalEvaluator.tokenCountToWin = tokenCountToWin;
        _horizontalEvaluator.tokenCountToWin = tokenCountToWin;
        _mainDiagonalEvaluator.tokenCountToWin = tokenCountToWin;
        _secondaryDiagonalEvaluator.tokenCountToWin = tokenCountToWin;
    }
}
