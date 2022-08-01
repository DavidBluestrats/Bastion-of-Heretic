using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    [SerializeField] Vector2Int startCoordinates;
    public Vector2Int StartCoordinates { get { return startCoordinates; } }
    [SerializeField] Vector2Int endCoordinates;
    public Vector2Int EndCoordinates { get { return EndCoordinates; } }
    private Node startPoint, destinationPoint, currentSearchNode;
    Dictionary<Vector2Int, Node> reached = new Dictionary<Vector2Int, Node>();
    Queue<Node> frontier = new Queue<Node>();
    Vector2Int[] directions = { Vector2Int.right, Vector2Int.left, Vector2Int.up, Vector2Int.down };
    GridManager gridManager;
    Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();
    void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();  

        if(gridManager!= null)
        {
            grid = gridManager.Grid;
            startPoint = gridManager.Grid[startCoordinates];
            destinationPoint = gridManager.Grid[endCoordinates];
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        GetNewPath();
    }

    // Update is called once per frame
    void ExploreNeighbors()
    {
        List<Node> neighbors = new List<Node>();
        foreach (Vector2Int  direction in directions)
        {
            Vector2Int neighborCoordinates = currentSearchNode.coordinate + direction;
            if (gridManager.Grid.ContainsKey(neighborCoordinates))
            {
                neighbors.Add(gridManager.GetNode(neighborCoordinates));
                
            }
        }
        foreach(Node neighbor in neighbors)
        {
            if (!reached.ContainsKey(neighbor.coordinate) && neighbor.isWalkable)
            {
                neighbor.connectedTo = currentSearchNode;
                reached.Add(neighbor.coordinate, neighbor);
                frontier.Enqueue(neighbor);
            }
        }
    }
    public List<Node> GetNewPath()
    {
        return GetNewPath(startCoordinates);
    }

    public List<Node> GetNewPath(Vector2Int coordinates)
    {
        gridManager.ResetNodes();
        BreadthFirstSearch(coordinates);
        return BuildPath();
    }
    void BreadthFirstSearch(Vector2Int coordinates)
    {
        startPoint.isWalkable = true;
        destinationPoint.isWalkable = true;
        frontier.Clear();
        reached.Clear();
        bool isRunning = true;
        frontier.Enqueue(grid[coordinates]);
        reached.Add(coordinates, grid[coordinates]);
        while (frontier.Count > 0 && isRunning)
        {
            currentSearchNode = frontier.Dequeue();
            currentSearchNode.isExplored = true;
            ExploreNeighbors();
            if(currentSearchNode.coordinate == endCoordinates)
            {
                isRunning = false;
            }
        }
    }
    List<Node> BuildPath()
    {
        List<Node> path = new List<Node>();
        Node currentNode = destinationPoint;
        path.Add(currentNode);
        currentNode.isPath = true;
        while (currentNode.connectedTo != null)
        {
            currentNode = currentNode.connectedTo;
            path.Add(currentNode);
            currentNode.isPath = true;
            
        }
        path.Reverse();
        return path;
    }
    public bool WillBlockPath(Vector2Int coordinate)
    {
        if (grid.ContainsKey(coordinate))
        {
            bool previousValue = grid[coordinate].isWalkable;
            grid[coordinate].isWalkable = false;
            List<Node> newPath = GetNewPath();
            grid[coordinate].isWalkable = previousValue;
            if(newPath.Count <= 1)
            {
                GetNewPath();
                return true;
            }
        }
        return false;
    }
    public void NotifyReceivers()
    {
        BroadcastMessage("RecalculatePath",false,SendMessageOptions.DontRequireReceiver);
    }
}
