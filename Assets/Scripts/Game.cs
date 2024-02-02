using UnityEngine;
using UnityEngine.Tilemaps;

public class Game : MonoBehaviour
{
    [SerializeField]
    private Tilemap m_TileMap;

    [SerializeField]
    private Token m_Prefab;

    [SerializeField]
    private BaseBoard _board;


    private int m_X = 0;
    private int m_Y = 0;

    void Start()
    {
        m_TileMap.CompressBounds();
        m_X = m_TileMap.cellBounds.xMin;
        m_Y = m_TileMap.cellBounds.yMin;
    }

    void Update()
    {
        if (Input.anyKeyDown)
        {
            Vector3Int CellPosition = new(m_X, m_Y, 0);

            if (m_TileMap.HasTile(CellPosition))
            {
                int TOKEN_SPAWN_COUNT = 1;
                for (int i = 0; i < TOKEN_SPAWN_COUNT; ++i)
                {
                    byte x = (byte)(m_X - m_TileMap.cellBounds.xMin);
                    _board.PlayToken(0, x);
                }
            }

            ++m_X;
            if (m_X >= m_TileMap.cellBounds.xMax)
            {
                m_X = m_TileMap.cellBounds.xMin;
                ++m_Y;
            }
        }
    }
}
