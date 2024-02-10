using System.IO;
using UnityEngine;

public class TileBoardFactory: BoardFactory
{
    public override Board CreateBoard(BoardConfig config)
    {
        Board board = TileBoardDataBase.GetBoard(config.boardName);
        if (board == null)
            throw new FileNotFoundException("Could not find the board " + config.boardName);

        BoardShape shape = TileBoardDataBase.GetShape(config.shapeName);
        if (shape == null)
            throw new FileNotFoundException("Could not find the shape " + config.shapeName);

        return InstantiateBoard(board, shape);
    }

    private Board InstantiateBoard(Board board, BoardShape shape)
    {
        Board newBoard = Instantiate(board, Vector3.zero, Quaternion.identity);
        newBoard.shape = Instantiate(shape, Vector3.zero, Quaternion.identity);
        return newBoard;
    }
}
