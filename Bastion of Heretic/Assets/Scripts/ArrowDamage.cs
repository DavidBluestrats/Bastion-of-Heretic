using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowDamage : MonoBehaviour
{
    public float damage;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collision between " + transform.name + " and " + other.gameObject.name);
        other.gameObject.GetComponent<EnemyHealth>().TakeDamage(10f);
        if(transform.tag == "FireBolt")
        {
            if (!other.gameObject.GetComponent<BurnOverTime>())
            {
                other.gameObject.AddComponent<BurnOverTime>();
            }
        }
    }
}
