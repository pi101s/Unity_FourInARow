using System.Collections.Generic;

namespace FIAR
{
    public readonly struct BoardConfig
    {
        public readonly string boardName;
        public readonly string shapeName;
        public readonly Dictionary<int, TokenConfig> playerTokenConfigDictionary;

        public BoardConfig(in string boardName, in string shapeName, Dictionary<int, TokenConfig> playerTokenConfigDictionary)
        {
            this.boardName = boardName;
            this.shapeName = shapeName;
            this.playerTokenConfigDictionary = playerTokenConfigDictionary;
        }
    }
}