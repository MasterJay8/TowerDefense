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
    [SerializeField] AnimationClip attackLength;



    [SerializeField] float targetingRange;
    [SerializeField] float rotationSpeed;
    [SerializeField] float fireRate;

    Transform target;
    float timeUntilFire;

    void Update()
    {
        if (target == null || target.gameObject.layer == LayerMask.NameToLayer("Ghost"))
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
            timeUntilFire += Time.deltaTime;
            //0.2f = Shoot after 1 animtion tick
            //if (timeUntilFire >= attackLength.length)
            Debug.Log(1f / fireRate);
            if (timeUntilFire >= 1f / fireRate)
            {
                Shoot();
                timeUntilFire = 0f;
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
    public void FindTarget()
    {
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
