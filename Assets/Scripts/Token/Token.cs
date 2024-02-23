using UnityEngine;

namespace FIAR
{
    public abstract class Token : MonoBehaviour
    {
        public delegate void OnFinishedMovingHandler();

        protected OnFinishedMovingHandler _onFinishedMovingHandler = null;

        public void OnFinishedMoving(in OnFinishedMovingHandler handler)
        {
            _onFinishedMovingHandler = handler;
        }

        public abstract void MoveTo(in Vector3 position, in float speed);
    }
}
