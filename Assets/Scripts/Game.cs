using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField]
    private BoardFactory _boardFactory;

    [SerializeField]
    private WinEvaluatorFactory _winEvaluatorFactory;

    [SerializeField]
    private string _boardName;

    [SerializeField]
    private string _shapeName;

    [SerializeField]
    private string _winEvaluatorName;

    [SerializeField]
    private Token[] _tokens;


    private Board _board;
    private WinEvaluator _winEvaluator;
    private byte _x = 0;
    private byte _lastPlayer = 0;

    void Start()
    {
        _winEvaluator = _winEvaluatorFactory.CreateWinEvaluator(new WinEvaluatorConfig(_winEvaluatorName));
        _board = _boardFactory.CreateBoard(new BoardConfig(_boardName, _shapeName));
        _board.tokens = _tokens;
        _board.OnTurnFinished(CheckWin);
    }

    void Update()
    {
        if (Input.anyKeyDown)
        {
            if (_x % 2 == 0)
                _lastPlayer = 0;
            else
                _lastPlayer = 1;

            _board.PlayToken(_lastPlayer, _x);
            _x = (byte)((_x + 1) % _board.grid.width);
        }
    }

    private void CheckWin()
    {
        WinEvaluationResult winResult = _winEvaluator.Evaluate(_board.grid, _lastPlayer, 1);
        if (winResult.winCombinations.Length > 0)
        {
            Debug.Log(winResult);
        }
    }
}
