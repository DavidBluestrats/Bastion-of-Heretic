using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] bool isPlaceable;
    GridManager gridManager;
    PathFinder pathFinder;
    Vector2Int coordinates = new Vector2Int();
    public bool IsPlaceable { get { return isPlaceable; } }
    private void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        pathFinder = FindObjectOfType<PathFinder>();


    }
    private void Start()
    {
        if (gridManager != null)
        {
            coordinates = gridManager.GetCoordinatesFromPosition(transform.position);
            if (!isPlaceable)
            {
                gridManager.BlockNode(coordinates);
            }
        }
    }
    private void OnMouseDown()
    {
        if (gridManager.GetNode(coordinates).isWalkable && !pathFinder.WillBlockPath(coordinates))
        {
            if (FindObjectOfType<Tower>().PlaceTower(transform)) {
                //isPlaceable = !isPlaceable;
                gridManager.BlockNode(coordinates);
                pathFinder.NotifyReceivers();
            }
        }
    }    
}
