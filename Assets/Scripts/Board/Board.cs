using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public abstract class Board : MonoBehaviour
{
    protected byte[,] _grid;
    protected BoardCoordinate _lastPlayedCoordinate;
    protected BoardShape _shape = null;
    protected Token[] _tokens;

    public BoardShape shape
    {
        set {
            if (_shape == null)
                Initialize(value);
            else
                throw new NullReferenceException("The shape of a board cannot bet set more than once.");
        }
    }

    public void Initialize(BoardShape shape)
    {
        _shape = shape;
        _grid = new byte[_shape.height, _shape.width];
        Reset();
    }

    protected byte width
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get { return _shape.width; }
    }
    protected byte height
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

    public abstract void PlayToken(byte playerId, byte column);
    public abstract void Reset();
}
