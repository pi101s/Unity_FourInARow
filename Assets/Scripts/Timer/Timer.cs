using UnityEngine;

public class Timer
{
    private static float _startTime = 0f;

    public static void Start()
    {
        _startTime = Time.realtimeSinceStartup;
    }

    public static float ElapsedSeconds
    {
        get { return Time.realtimeSinceStartup - _startTime; }
    }

    public static float ElapsedMilliseconds
    {
        get { return ElapsedSeconds * 1000; }
    }

    public static float ElapsedMicroseconds
    {
        get { return ElapsedSeconds * 1000000; }
    } 
}
