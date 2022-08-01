using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurnOverTime : MonoBehaviour
{
    // Start is called before the first frame update
    float burnDamage = 15f;
    float timeBetweenBurnTicks = 2f;
    float timeForNextBurn = 0f;
    EnemyHealth health;
    // Update is called once per frame
    void Start()
    {
        health = GetComponent<EnemyHealth>();
    }
    void Update()
    {
        if (Time.time >= timeForNextBurn)
        {
            timeForNextBurn = Time.time + timeBetweenBurnTicks;
            health.TakeDamage(burnDamage);
        }
    }
}
