namespace FIAR
{
    public readonly struct TokenConfig
    {
        public readonly string name;

        public TokenConfig(in string name)
        {
            this.name = name;
        }
    }
}