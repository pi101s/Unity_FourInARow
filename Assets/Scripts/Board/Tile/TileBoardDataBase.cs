using UnityEngine;

namespace FIAR
{
    public class TileBoardDataBase
    {
        private const string BOARDS_FOLDER = "Boards";
        private const string SHAPES_FOLDER = "Shapes";
        private static Object[] _boards = null;
        private static Object[] _shapes = null;

        public static Board GetBoard(in string name)
        {
            LoadBoards();
            return FindBoardByName(name);
        }

        public static BoardShape GetShape(in string name)
        {
            LoadShapes();
            return FindShapeByName(name);
        }

        private static Board FindBoardByName(in string name) {
            foreach (Object board in _boards)
                if (board.name == name)
                    return (Board)board;
            return null;
        }

        private static BoardShape FindShapeByName(in string name) {
            foreach (Object shape in _shapes)
                if (shape.name == name)
                    return (BoardShape)shape;
            return null;
        }

        private static void LoadBoards()
        {
            _boards ??= Resources.LoadAll(BOARDS_FOLDER, typeof(Board));
        }

        private static void LoadShapes()
        {
            _shapes ??= Resources.LoadAll(SHAPES_FOLDER, typeof(BoardShape));
        }
    }
}
