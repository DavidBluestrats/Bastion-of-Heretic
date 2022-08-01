using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    CoinsBag coinManager;
    GameObject currentTower;
    [SerializeField] List<GameObject> towers = new List<GameObject>();
    int cost = 15;
    public GameObject currentSelectedPlaceable()
    {
        return currentTower;
    }
    // Start is called before the first frame update
    void Start()
    {
        coinManager = FindObjectOfType<CoinsBag>();
        currentTower = towers[0];
    }

    public bool PlaceTower(Transform parentTile)
    {
        if (coinManager.CurrentBalance>= cost)
        {
            GameObject currentBuiltTower = Instantiate(currentTower, parentTile.position, Quaternion.identity);
            currentBuiltTower.transform.parent = parentTile;
            StartCoroutine(BuildTowerDelay(currentBuiltTower));
            coinManager.updateBalance(-cost);
            return true;
        }
        else
        {
            return false;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentTower = towers[0];
            cost = 15;
            Debug.Log("Equiped Ballista.");
        }else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentTower = towers[1];
            cost = 40;
            Debug.Log("Equiped FlameThrower.");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            currentTower = towers[2];
            cost = 60;
            Debug.Log("Equiped Lightning Bolt.");
        }
    }
    IEnumerator BuildTowerDelay(GameObject towerPrefab)
    {
        foreach(Transform structure in towerPrefab.transform)
        {
            structure.gameObject.SetActive(false);
        }
        foreach (Transform structure in towerPrefab.transform)
        {
            structure.gameObject.SetActive(true);
            yield return new WaitForSeconds(1.5f);
        }
    }
    
}
