using System.Runtime.CompilerServices;
using UnityEngine;

public abstract class Board : MonoBehaviour
{
    public delegate void OnTurnFinishedHandler();

    protected const int BLOCKED_SPACE = int.MinValue;
    protected const int EMPTY_SPACE = int.MinValue + 1;

    protected int[,] _grid = null;
    protected BoardCoordinate _lastPlayedCoordinate = new(0, 0);
    protected BoardShape _shape = null;
    protected Token[] _tokens = null;
    protected OnTurnFinishedHandler _onTurnFinishedHandler = null;

    public BoardShape shape
    {
        set {
            if (_shape == null)
                Initialize(value);
            else
                throw new System.Exception("The shape of a board cannot bet set more than once.");
        }
    }

    public void Initialize(in BoardShape shape)
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

    public Token[] tokens
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        set { _tokens = value;}
    }

    public BoardCoordinate lastPlayedCoordinate
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get { return _lastPlayedCoordinate; }
    }

    public abstract void PlayToken(in int playerId, in int column);
    public abstract void Reset();

    protected virtual bool IsValidPlayerId(in int playerId)
    {
        return playerId >= 0
            && playerId != EMPTY_SPACE
            && playerId != BLOCKED_SPACE
            && playerId < _tokens.Length;
    }

    protected virtual bool IsEmptySpace(in BoardCoordinate coordinate)
    {
        return _grid[coordinate.row, coordinate.column] == EMPTY_SPACE;
    }
}
