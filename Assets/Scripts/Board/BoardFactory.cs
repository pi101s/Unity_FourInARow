using UnityEngine;

public abstract class BoardFactory: MonoBehaviour
{
    public abstract Board CreateBoard(in BoardConfig config);
}
