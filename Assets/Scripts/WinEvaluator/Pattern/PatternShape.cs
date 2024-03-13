using UnityEngine;

namespace FIAR
{
    public abstract class PatternShape : MonoBehaviour
    {
        public abstract int width { get; }
        public abstract int height { get; }
        public abstract BoardCoordinate[] coodinates { get; }
    }
}
