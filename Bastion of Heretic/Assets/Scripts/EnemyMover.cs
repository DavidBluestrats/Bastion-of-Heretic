using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMover : MonoBehaviour
{
    List<Node> pathToFollow = new List<Node>();
    PathFinder pathFinder;
    GridManager gridManager;
    [SerializeField] [Range(1f,5f)] float speed = 1f;
    Enemy enemyManager;
    void OnEnable()
    {
        ReturnToStart();
        RecalculatePath(true);
        //ReturnToStart();
    }
    void Awake()
    {
        enemyManager = GetComponent<Enemy>();
        pathFinder = FindObjectOfType<PathFinder>();
        gridManager = FindObjectOfType<GridManager>();
    }
    void ReturnToStart()
    {
        transform.position = gridManager.GetPositionFromCoordinates(pathFinder.StartCoordinates);
    }

    void RecalculatePath(bool resetPath)
    {
        Vector2Int coordinate = new Vector2Int();
        if (resetPath)
        {
            coordinate = pathFinder.StartCoordinates;
        }
        else
        {
            coordinate = gridManager.GetCoordinatesFromPosition(transform.position);
        }
        pathToFollow.Clear();
        pathToFollow = pathFinder.GetNewPath(coordinate);
        StopAllCoroutines();
        StartCoroutine(followPathWithDelay());
    }
    IEnumerator followPathWithDelay()
    {
        for(int i = 1; i<pathToFollow.Count;i++)
        {
            Vector3 startingPosition = transform.position;
            Vector3 endPosition = gridManager.GetPositionFromCoordinates(pathToFollow[i].coordinate);
            transform.LookAt(endPosition);
            float travelPercentage = 0f;
            while (travelPercentage < 1f)
            {
                travelPercentage += Time.deltaTime * speed;
                transform.position = Vector3.Lerp(startingPosition, endPosition, travelPercentage);
                yield return new WaitForEndOfFrame();
            }
        }
        FinishPath();
    }

    private void FinishPath()
    {
        enemyManager.TakeGold();
        gameObject.SetActive(false);
    }
}
