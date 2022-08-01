using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
[ExecuteAlways]
[RequireComponent(typeof(TMP_Text))]
public class CoordinateLabeler : MonoBehaviour
{
    [SerializeField] Color defaultColor = Color.black;
    [SerializeField] Color blockedColor = Color.gray;
    [SerializeField] Color exploredColor = Color.red;
    [SerializeField] Color pathColor = Color.blue;
    TMP_Text label;
    Vector2Int currentPosition = new Vector2Int();
    GridManager gridManager;
    void Awake()
    {
        label = GetComponent<TMP_Text>();
        gridManager = FindObjectOfType<GridManager>();
        UpdateCoordinates();
    }
    void Update()
    {
        if (!Application.isPlaying)
        {
            UpdateCoordinates();
        }
        ToggleLabeler();
        SetTileColor();
        
    }
    void ToggleLabeler()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            label.enabled = !label.enabled;
        }
    }
    void UpdateCoordinates()
    {
        if (gridManager == null) return;
        currentPosition = new Vector2Int((int)transform.parent.position.x, (int)transform.parent.position.z)/gridManager.UnityGridSize;
        label.text = "(" + currentPosition.x+ "," + currentPosition.y + ")";
        UpdateParentName();
    }
    void UpdateParentName()
    {
        transform.parent.name = label.text;
    }

    void SetTileColor()
    {
        if (gridManager == null)
        {
            Debug.Log("Grid is Null");
            return;
        }
        //Debug.Log("Coordinates: ("+currentPosition.x+","+currentPosition.y+")");
        Node node = gridManager.GetNode(currentPosition);
        if (node == null) 
        {
            //Debug.Log("Node is Null");
            return; 
        }
        if (!node.isWalkable)
        {
            label.color = blockedColor;
        }
        else if (node.isPath)
        {
            label.color = pathColor;
        }
        else if (node.isExplored)
        {
            label.color = exploredColor;
        }
        else 
        {
            label.color = defaultColor; 
        }
    }
}
