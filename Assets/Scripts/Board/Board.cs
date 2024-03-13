using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace FIAR
{
    public abstract class Board : MonoBehaviour
    {
        public delegate void OnTurnFinishedHandler();

        protected const int BLOCKED_SPACE = int.MinValue;
        protected const int EMPTY_SPACE = int.MinValue + 1;
        protected TokenFactory _tokenFactory;

        protected int[,] _grid = null;
        protected BoardCoordinate _lastPlayedCoordinate = new(0, 0);
        protected BoardShape _shape = null;
        protected Dictionary<int, TokenConfig> _playersTokensConfig = null;
        protected OnTurnFinishedHandler _onTurnFinishedHandler = null;

        public BoardShape shape
        {
            set
            {
                if (_shape == null)
                    Initialize(value);
                else
                    throw new System.Exception("The shape of a board cannot bet set more than once.");
            }
        }

        public TokenFactory tokenFactory
        {
            set
            {
                if (_tokenFactory == null)
                    _tokenFactory = value;
                else
                    throw new System.Exception("The token factory of a board cannot bet set more than once.");
            }
        }

        private void Initialize(in BoardShape shape)
        {
            _shape = shape;
            _grid = new int[_shape.height, _shape.width];
            Reset();
        }

        public void OnTurnFinished(in OnTurnFinishedHandler handler)
        {
            _onTurnFinishedHandler = handler;
        }

        protected int width
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return _shape.width; }
        }
        protected int height
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return _shape.height; }
        }

        public BoardGrid grid
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new BoardGrid(_grid, width, height); }
        }

        public Dictionary<int, TokenConfig> playersTokensConfig
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set { _playersTokensConfig = value; }
        }

        public BoardCoordinate lastPlayedCoordinate
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return _lastPlayedCoordinate; }
        }

        protected virtual bool IsValidPlayerId(in int playerId)
        {
            return _playersTokensConfig.ContainsKey(playerId);
        }

        protected virtual bool IsEmptySpace(in BoardCoordinate coordinate)
        {
            return _grid[coordinate.row, coordinate.column] == EMPTY_SPACE;
        }

        public abstract void PlayToken(in int playerId, in int column);
        public abstract bool CouldPlayToken();
        public abstract void Reset();
    }
}
