using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace FIAR
{
    public class TileBoardFactory: BoardFactory
    {
        public override Board CreateBoard(in BoardConfig config)
        {
            Board board = GetBoard(config.boardName);
            BoardShape shape = GetShape(config.shapeName);
            return InstantiateBoard(board, shape, config.playerTokenConfigDictionary);
        }

        private Board GetBoard(string name)
        {
            Board board = TileBoardDataBase.GetBoard(name);
            return board != null ? board : throw new FileNotFoundException("Could not find the board " + name);
        }

        private BoardShape GetShape(string name)
        {
            BoardShape shape = TileBoardDataBase.GetShape(name);
            return shape != null ? shape : throw new FileNotFoundException("Could not find the shape " + name);
        }

        private Board InstantiateBoard(in Board board, in BoardShape shape, Dictionary<int, TokenConfig> playersTokensConfig)
        {
            Board newBoard = Instantiate(board, Vector3.zero, Quaternion.identity);
            newBoard.shape = Instantiate(shape, Vector3.zero, Quaternion.identity);
            newBoard.playersTokensConfig = playersTokensConfig;
            newBoard.tokenFactory = _tokenFactory;
            return newBoard;
        }
    }
}
