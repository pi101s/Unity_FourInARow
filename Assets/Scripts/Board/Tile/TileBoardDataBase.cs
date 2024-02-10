using UnityEngine;

public class TileBoardDataBase
{
    private static readonly string FOLDER = "Board";
    private static Object[] _boards = null;

    public static Board GetBoard(string name)
    {
        LoadBoards();
        return FindBoardByName(name);
    }

    private static Board FindBoardByName(string name) {
        foreach (Object board in _boards)
            if (board.name == name)
                return (Board)board;
        return null;
    }

    private static void LoadBoards()
    {
        if (_boards == null)
            _boards = Resources.LoadAll(FOLDER, typeof(Board));
    }
}
