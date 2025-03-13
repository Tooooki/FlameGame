using Mono.Cecil;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoomManager : MonoBehaviour
{
    [SerializeField] GameObject roomPrefab;

    int roomWidth = 80;
    int roomHeight = 48;

    int gridSizeX = 40;
    int gridSizeY = 40;

    private List<GameObject> roomObjects = new List<GameObject>();

    private int[,] roomGrid;
    private int roomCount;

    private void Start()
    {
        roomGrid = new int[gridSizeX, gridSizeY];
    }

    private Vector2Int GetPositionFromGridIndex(Vector2Int gridIndex)
    {
        int gridX = gridIndex.x;
        int gridY = gridIndex.y;
        return new Vector2Int(roomWidth * (gridX - gridSizeX / 2), roomHeight * (gridY - gridSizeY / 2));
    }

    private void OnDrawGizmos()
    {
        Color gizmoColor = new Color(0, 1, 1, 0.05f);
        Gizmos.color = gizmoColor;

        for (int x = 0; x < gridSizeX; x++)
        {
            for(int y = 0; y < gridSizeY; y++)
            {
                Vector2Int position = GetPositionFromGridIndex(new Vector2Int(x, y));
            }
        }
    }
}
