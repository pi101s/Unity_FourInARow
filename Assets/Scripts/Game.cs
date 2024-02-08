using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField]
    private BoardFactory _boardFactory;

    [SerializeField]
    private string _boardName;

    [SerializeField]
    private Token[] _tokens;


    private Board _board;
    private byte m_X = 0;

    void Start()
    {
        _board = _boardFactory.CreateBoard(_boardName);
        _board.SetTokens(_tokens);
    }

    void Update()
    {
        if (Input.anyKeyDown)
        {
            if (m_X % 2 == 0)
                _board.PlayToken(0, m_X);
            else
                _board.PlayToken(1, m_X);

            m_X = (byte)((m_X + 1) % _board.GetGrid().width);
        }
    }
}
