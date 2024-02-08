using UnityEngine;

public class TileBoardFactory: BoardFactory
{
    public override Board CreateBoard(string name)
    {
        Board board = TileBoardDataBase.GetBoard(name);
        if (board != null)
            return Instantiate(board, Vector3.zero, Quaternion.identity);

        return null;
    }
}
