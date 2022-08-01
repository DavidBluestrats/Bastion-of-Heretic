using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] [Range(0f, 50f)] int poolSize = 5;
    [SerializeField] [Range(0.5f, 5f)] float spawnSpeed = 1f;
    GameObject[] pool;

    void Awake()
    {
        PopulatePool();
    }
    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }
    void PopulatePool()
    {
        pool = new GameObject[poolSize];
        for (int i = 0; i<pool.Length-1;i++)
        {
            pool[i] = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            pool[i].transform.parent = transform;
            pool[i].SetActive(false);

        }
    }

    IEnumerator SpawnEnemies()
    {
        while (Application.isPlaying)
        {
            for(int i = 0; i < pool.Length - 1; i++)
            {
                if (!pool[i].activeInHierarchy)
                {
                    pool[i].SetActive(true);
                }
                yield return new WaitForSeconds(spawnSpeed);
            }
        }
    }
}
