using UnityEngine;

namespace FIAR
{
    public class Game : MonoBehaviour
    {
        [SerializeField]
        private BoardFactory _boardFactory;

        [SerializeField]
        private WinEvaluatorFactory _winEvaluatorFactory;

        private Board _board;
        private WinEvaluator _winEvaluator;
        private int _x = 0;
        private int _lastPlayer = 0;
        private bool _canPlay = true;

        void Start()
        {
            _winEvaluator = _winEvaluatorFactory.CreateWinEvaluator(GameConfig.winEvaluatorConfig);
            _board = _boardFactory.CreateBoard(GameConfig.boardConfig);
            _board.OnTurnFinished(CheckWin);
        }

        void Update()
        {
            if (Input.anyKeyDown)
                PlayToken();
        }

        private void PlayToken()
        {
            if (!_canPlay)
                return;

            if (_x % 500 == 0)
                _lastPlayer = 0;
            else
                _lastPlayer = 1;

            _board.PlayToken(_lastPlayer, _x);
            if (_board.CouldPlayToken())
                _canPlay = false;
            _x = (_x + 1) % _board.grid.width;
        }

        private void CheckWin()
        {
            _canPlay = true;
            WinEvaluationResult winResult = _winEvaluator.Evaluate(_board.grid, _lastPlayer);
            if (winResult.winCombinations.Length > 0)
                Debug.Log(winResult);
        }
    }
}
