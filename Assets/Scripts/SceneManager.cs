using System.Collections.Generic;
using UnityEngine;

namespace FIAR
{
    public class SceneManager : MonoBehaviour
    {
        public void SwapScene(string scene)
        {
            Dictionary<int, TokenConfig> map = new()
            {
                { 0, new TokenConfig("TokenGreen") },
                { 1, new TokenConfig("TokenRed") }
            };

            GameConfig.boardConfig = new("TileBoard", "Shape", map);
            GameConfig.winEvaluatorConfig = new("PatternWinEvaluator", new string[] { "Horizontal", "Vertical" });
            UnityEngine.SceneManagement.SceneManager.LoadScene(scene);
        }
    }
}
