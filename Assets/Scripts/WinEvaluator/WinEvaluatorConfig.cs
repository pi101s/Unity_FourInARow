namespace FIAR
{
    public readonly struct WinEvaluatorConfig
    {
        public readonly string name;
        public readonly string[] patterns;

        public WinEvaluatorConfig(in string name, in string[] patterns)
        {
            this.name = name;
            this.patterns = patterns;
        }
    }
}