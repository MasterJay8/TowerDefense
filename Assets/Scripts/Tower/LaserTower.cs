using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class LaserTower : MonoBehaviour
{
    //[SerializeField] Transform turretRotationPoint;
    [SerializeField] LayerMask enemyMask;
    //[SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform firingPoint;
    //[SerializeField] Animator animator;


    [SerializeField] int damageOverTime;
    [SerializeField] float targetingRange;
    //[SerializeField] float rotationSpeed;
    //[SerializeField] float fireRate;

    [SerializeField] LineRenderer lineRenderer;
    //[SerializeField] ParticleSystem impactEffect;
    //[SerializeField] Light impactLight;

    Transform target;
    float timeUntilFire;

    void Update()
    {
        //lineRenderer.SetPosition(0, new Vector3(0,0,0));
        //lineRenderer.SetPosition(1, new Vector3(2, 2, 0));
        if (target == null || target.gameObject.layer == LayerMask.NameToLayer("Ghost"))
        {
            lineRenderer.enabled = false;
            FindTarget();
            return;
        }
        //RotateTowardTarget();

        //Check enemy distantion
        if (Vector2.Distance(target.position, transform.position) >= targetingRange) target = null;
        else
        {
            timeUntilFire += Time.deltaTime;
            //0.2f = Shoot after 1 animtion tick
            if (timeUntilFire >= 0.2f)
            {
                Laser();
                timeUntilFire = 0f;
            }
        }
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
    void Laser()
    {
        //target.TakeDamage(damageOverTime * Time.deltaTime);
        //target.gameObject.GetComponent<Health>().TakeDamage(Mathf.RoundToInt(damageOverTime * Time.deltaTime), "Electric");
        target.gameObject.GetComponent<Health>().TakeDamage(damageOverTime);

        if (!lineRenderer.enabled)
        {
            lineRenderer.enabled = true;
            //impactEffect.Play();
            //impactLight.enabled = true;
        }

        lineRenderer.SetPosition(0, firingPoint.position);
        lineRenderer.SetPosition(1, target.position);
        //Debug.Log(firingPoint.position.x + " " + firingPoint.position.y);
        //Debug.Log(target.position.x + " " + target.position.y);

        //Vector3 dir = firingPoint.position - target.position;

        //impactEffect.transform.position = target.position + dir.normalized;

        //impactEffect.transform.rotation = Quaternion.LookRotation(dir);
    }
}
