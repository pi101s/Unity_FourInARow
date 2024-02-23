using System.IO;
using UnityEngine;

namespace FIAR
{
    public class TokenFactory : MonoBehaviour
    {
        public Token CreateToken(in TokenConfig config)
        {
            Token token = TokenDatabase.GetToken(config.name);
            Token tokenInstance;
            if (token != null)
                tokenInstance = Instantiate(token, Vector3.zero, Quaternion.identity);
            else
                throw new FileNotFoundException("Could not find the token " + config.name);

            return tokenInstance;
        }
    }
}
