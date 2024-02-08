using UnityEditor;

public class TileBoardDataBase
{
    private static readonly string DATABASE_FOLDER = "Assets/Prefabs/Board";
    private static Board[] _boards = null;

    public static Board GetBoard(string name)
    {
        LoadBoards();
        return FindBoardByName(name);
    }

    private static Board FindBoardByName(string name) {
        foreach (Board board in _boards)
            if (board.name == name)
                return board;

        return null;
    }

    private static void LoadBoards()
    {
        LoadBoardsFromGuids(GetBoardGuids());
    }

    private static string[] GetBoardGuids()
    {
        return AssetDatabase.FindAssets("", new string[] { DATABASE_FOLDER });
    }

    private static void LoadBoardsFromGuids(string[] guids)
    {
        if (_boards == null)
        {
            _boards = new Board[guids.Length];
            for (int i = 0; i < _boards.Length; i++)
                _boards[i] = AssetDatabase.LoadAssetAtPath<Board>(AssetDatabase.GUIDToAssetPath(guids[i]));
        }
    }
}
