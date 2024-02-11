using System.Collections;
using UnityEngine;

public class Token : MonoBehaviour
{
    public delegate void OnFinishedMovingHandler();

    static readonly float SECONDS_BETWEEN_STEPS = 0.01f;

    private Vector3 _initialPosition = Vector3.zero;
    private Vector3 _finalPosition = Vector3.zero;
    private float _motionTimeInSeconds = 0f;
    private float _startTimeInSeconds = 0f;
    private OnFinishedMovingHandler _onFinishedMovingHandler = null;

    public void OnFinishedMoving(OnFinishedMovingHandler handler)
    {
        _onFinishedMovingHandler = handler;
    }

    public void MoveTo(Vector3 position, float speed)
    {
        _initialPosition = transform.position;
        _finalPosition = position;
        _motionTimeInSeconds = CalculateMotionTime(speed);
        _startTimeInSeconds = Time.time;
        StartCoroutine(Move());
    }

    private float CalculateMotionTime(float speed)
    {
        float distance = (_finalPosition - _initialPosition).magnitude;
        return distance / speed;
    }

    private IEnumerator Move()
    {
        while (!FinalPositionHasBeenReached())
            yield return MoveOneStep();

        _onFinishedMovingHandler?.Invoke();
        yield return null;
    }

    private bool FinalPositionHasBeenReached()
    {
        return _finalPosition == transform.position;
    }

    private IEnumerator MoveOneStep()
    {
        float currentTimeInSeconds = Time.time;
        float elapsedTimeInSeconds = currentTimeInSeconds - _startTimeInSeconds;
        Vector3 interpolatedPosition = Vector3.Lerp(_initialPosition, _finalPosition, elapsedTimeInSeconds / _motionTimeInSeconds);
        transform.position = interpolatedPosition;
        yield return new WaitForSeconds(SECONDS_BETWEEN_STEPS);
    }
}
