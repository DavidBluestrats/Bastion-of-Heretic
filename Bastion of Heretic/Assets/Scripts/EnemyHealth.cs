using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float hitPoints;
    [SerializeField] int difficultyRamp = 5;
    public float currentHitPoints;
    Enemy enemyManager;
    void OnEnable()
    {
        currentHitPoints = hitPoints;
    }
    void Start()
    {
        enemyManager = GetComponent<Enemy>();
    }
    void Update()
    {
        CheckIfDeath();
    }

    void CheckIfDeath()
    {
        if (currentHitPoints <= Mathf.Epsilon)
        {
            enemyManager.RewardGold();
            hitPoints += difficultyRamp;
            if (GetComponent<BurnOverTime>())
            {
                Destroy(GetComponent<BurnOverTime>());
            }
            gameObject.SetActive(false);
        }
    }
    public void TakeDamage(float damageToTake)
    {
        currentHitPoints -= damageToTake;
    }
}
