using System.Runtime.CompilerServices;
using UnityEngine;

public abstract class Board : MonoBehaviour
{
    protected byte _width = 0;
    protected byte _height = 0;
    protected byte[,] _grid;
    protected BoardCoordinate _lastPlayedCoordinate;
    protected Token[] _tokens;

    public BoardGrid grid
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get { return new BoardGrid(_grid, _width, _height); }
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
}
