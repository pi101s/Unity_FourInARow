using UnityEngine;

public abstract class BoardShape : MonoBehaviour
{
    public abstract byte width {get;}
    public abstract byte height {get;}

    public abstract bool IsEmpty(BoardCoordinate coordinate);
    public abstract Vector3 GetPosition(BoardCoordinate coordinate);
}
