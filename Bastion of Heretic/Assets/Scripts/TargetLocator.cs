using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLocator : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Transform weapon;
    [SerializeField] float rangeRadius = 15f;
    Transform target = null;
    public GameObject arrowPrefab;
    float secondsBetweenShots = 2f;
    float nextShotTime = 0f;
    // Update is called once per frame
    private void Start()
    {
        StartCoroutine(StructureController());
    }
    void Update()
    {
        if (target != null)
        {
            AimWeapon();
            ShootWeapon();
        }
    }

    IEnumerator StructureController()
    {
        while (true)
        {
            target = FindClosestTarget();
            yield return new WaitForSeconds(0.55f);
        }
     
    }
    void AimWeapon()
    {
       weapon.LookAt(target.position);
    }

    Transform FindClosestTarget()
    {
        Collider [] enemies = Physics.OverlapSphere(transform.position,rangeRadius,1<<6);
        Transform closestTarget = null;
        float currentShortestDistance = Mathf.Infinity;
        foreach (Collider enemy in enemies)
        {
            float distanceThisIteration = Vector3.Distance(transform.position, enemy.gameObject.transform.position);
            if (distanceThisIteration < currentShortestDistance && enemy.gameObject.activeInHierarchy)
            {
                closestTarget = enemy.gameObject.transform;
                currentShortestDistance = distanceThisIteration;
            }
        }
       return closestTarget;
    }
    private void ShootWeapon()
    {
        if (Time.time > nextShotTime) {
            nextShotTime = Time.time + secondsBetweenShots;
            GameObject projectile = Instantiate(arrowPrefab);
            Physics.IgnoreLayerCollision(projectile.layer,gameObject.layer);
            projectile.transform.position = weapon.position;
            Vector3 rotation = projectile.transform.rotation.eulerAngles;
            projectile.transform.rotation = Quaternion.Euler(rotation.x, weapon.eulerAngles.y, rotation.z);
            projectile.GetComponent<Rigidbody>().AddForce(projectile.transform.forward * 100f, ForceMode.Impulse);
            StartCoroutine(DestroyProjectile(projectile));
        }
    }

    IEnumerator DestroyProjectile(GameObject projectile)
    {
        yield return new WaitForSeconds(1f);
        Destroy(projectile);
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision between " + transform.name + " and " + collision.gameObject.name);
    }
}
