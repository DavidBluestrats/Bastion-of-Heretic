using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Node
{
    public Vector2Int coordinate;
    public bool isWalkable;
    public bool isExplored;
    public bool isPath;
    public Node connectedTo;
    public Node(Vector2Int coordinate, bool isWalkable)
    {
        this.coordinate = coordinate;
        this.isWalkable = isWalkable;
    }
}
