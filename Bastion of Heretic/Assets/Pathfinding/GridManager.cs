using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] Vector2Int gridSize;
    [Tooltip("World grid size should match Unity Editor snap settings.")]
    [SerializeField] int unityGridSize = 10;
    public int UnityGridSize{ get { return unityGridSize; } }
    Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();
    public Dictionary<Vector2Int, Node> Grid { get { return grid; } }
    private void Awake()
    {
        CreateGrid();
    }
    public Node GetNode(Vector2Int coordinates)
    {
        if (grid.ContainsKey(coordinates))
        {
            return grid[coordinates];
        }
        return null;
    }
    void CreateGrid()
    {
        for(int x = 0; x < gridSize.x; x++)
        {
            for(int y = 0; y < gridSize.y; y++)
            {
                Vector2Int coordinates = new Vector2Int(x, y);
                grid.Add(coordinates, new Node(coordinates,true));
            }
        }
    }
    public void BlockNode(Vector2Int nodeToBlock)
    {
        if (grid.ContainsKey(nodeToBlock))
        {
            grid[nodeToBlock].isWalkable = false;
        }
    }
    public void ResetNodes()
    {
        foreach(KeyValuePair<Vector2Int, Node> node in grid)
        {
            node.Value.connectedTo = null;
            node.Value.isExplored = false;
            node.Value.isPath = false;
        }
    }

    public Vector2Int GetCoordinatesFromPosition(Vector3 position)
    {
        Vector2Int coordinates = new Vector2Int();
        coordinates.x = Mathf.RoundToInt(position.x) / unityGridSize;
        coordinates.y = Mathf.RoundToInt(position.z) / unityGridSize;
        return coordinates;
    }

    public Vector3 GetPositionFromCoordinates(Vector2Int position)
    {
        Vector3 coordinates = new Vector3();
        coordinates.x = position.x * unityGridSize;
        coordinates.z = position.y * unityGridSize;
        return coordinates;
    }
}
