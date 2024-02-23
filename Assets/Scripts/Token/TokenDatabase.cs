using UnityEngine;

namespace FIAR
{
    public class TokenDatabase
    {
        private const string TOKENS_FOLDER = "Tokens";
        private static Object[] _tokens = null;

        public static Token GetToken(in string name)
        {
            LoadTokens();
            return FindTokenByName(name);
        }

        private static Token FindTokenByName(in string name)
        {
            foreach (Object token in _tokens)
                if (token.name == name)
                    return (Token)token;
            return null;
        }

        private static void LoadTokens()
        {
            _tokens ??= Resources.LoadAll(TOKENS_FOLDER, typeof(Token));
        }
    }
}

