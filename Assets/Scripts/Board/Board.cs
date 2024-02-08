using System.Runtime.CompilerServices;
using UnityEngine;

public abstract class Board : MonoBehaviour
{
    protected byte _width = 0;
    protected byte _height = 0;
    protected byte[,] _grid;
    protected BoardCoordinate _lastPlayedCoordinate;
    protected Token[] _tokens;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public BoardGrid GetGrid()
    {
        return new BoardGrid(_grid, _width, _height);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void SetTokens(Token[] tokens)
    {
        _tokens = tokens;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public BoardCoordinate GetLastPlayedCoordinate()
    {
        return _lastPlayedCoordinate;
    }

    public abstract void PlayToken(byte playerId, byte column);
}
