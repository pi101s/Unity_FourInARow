using UnityEngine;

namespace FIAR
{
    public abstract class BoardShape : MonoBehaviour
    {
        public abstract int width {get;}
        public abstract int height {get;}

        public abstract bool IsEmpty(in BoardCoordinate coordinate);
        public abstract Vector3 GetPosition(in BoardCoordinate coordinate);
    }
}
