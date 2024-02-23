namespace FIAR
{
    public static class GameConfig
    {
        private const string DEFAULT_BOARD = "TileBoard";
        private const string DEFAULT_SHAPE = "Shape";
        private const string DEFAULT_EVALUATOR = "OfficialWinEvaluator";
        private const int DEFAULT_TOKENS_TO_WIN = 4;

        public static BoardConfig boardConfig = new(DEFAULT_BOARD, DEFAULT_SHAPE, new());
        public static WinEvaluatorConfig winEvaluatorConfig = new(DEFAULT_EVALUATOR, DEFAULT_TOKENS_TO_WIN);
    }
}
