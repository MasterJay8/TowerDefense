using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Tower : MonoBehaviour
{
    [SerializeField] Transform turretRotationPoint;
    [SerializeField] LayerMask enemyMask;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform firingPoint;
    [SerializeField] Animator animator;



    [SerializeField] float targetingRange;
    [SerializeField] float rotationSpeed;
    [SerializeField] float fireRate;

    Transform target;
    float timeUnitlFire;

    void Update()
    {
        if (target == null)
        {
            animator.SetBool("Shooting", false);
            FindTarget();
            return;
        }
        RotateTowardTarget();

        //Check enemy distantion
        if (Vector2.Distance(target.position, transform.position) >= targetingRange) target = null;
        else
        {
            timeUnitlFire += Time.deltaTime;
            //0.2f = Shoot after 1 animtion tick
            if (timeUnitlFire >= 1f / fireRate + 0.2f) 
            {
                Shoot();
                timeUnitlFire = 0f;
            }
        }
    }
    void Shoot()
    {
        GameObject bulletObject = Instantiate(bulletPrefab, firingPoint.position, Quaternion.identity);
        Bullet bulletScript = bulletObject.GetComponent<Bullet>();
        bulletScript.SetTarget(target);

        animator.SetBool("Shooting", true);
    }
    void FindTarget()
    {
        /*RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetingRange, Vector2.up, 0f, enemyMask);
        if (hits.Length > 0)
        {
            target = hits[0].transform;
        }*/

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, targetingRange, enemyMask);

        if (colliders.Length > 0)
        {
            float furthestDistance = float.MinValue;
            Transform furthestEnemy = null;

            foreach (Collider2D collider in colliders)
            {
                float distanceTraveled = collider.GetComponent<Enemy>().GetDistanceTraveled();

                if (distanceTraveled > furthestDistance)
                {
                    furthestDistance = distanceTraveled;
                    furthestEnemy = collider.transform;
                }
            }
            target = furthestEnemy;
        }
    }
    void RotateTowardTarget()
    {
        float angle = Mathf.Atan2(target.position.y - transform.position.y, target.position.x - transform.position.x) * Mathf.Rad2Deg - 90f;

        Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        //turretRotationPoint.rotation = targetRotation;
        turretRotationPoint.rotation = Quaternion.RotateTowards(turretRotationPoint.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
    private void OnDrawGizmosSelected()
    {
        //Handles.color = Color.cyan;
        //Handles.DrawWireDisc(transform.position, transform.forward, targetingRange);
    }
}
