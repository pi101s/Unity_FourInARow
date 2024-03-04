using UnityEngine;

namespace FIAR
{
    public abstract class BoardFactory: MonoBehaviour
    {
        [SerializeField]
        protected TokenFactory _tokenFactory;

        public abstract Board CreateBoard(in BoardConfig config);
    }
}
