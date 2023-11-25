using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Tower : MonoBehaviour
{
    [SerializeField] Transform turretRotationPoint;
    [SerializeField] LayerMask enemyMask;


    [SerializeField] float targetingRange;
    [SerializeField] float rotationSpeed;

    Transform target;

    void Update()
    {
        if (target == null)
        {
            FindTarget();
            return;
        }
        RotateTowardTarget();

        //Check enemy distantion
        if (Vector2.Distance(target.position, transform.position) >= targetingRange) target = null;
    }
    void FindTarget()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetingRange, Vector2.up, 0f, enemyMask);
        if (hits.Length > 0)
        {
            target = hits[0].transform;
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
        Handles.color = Color.cyan;
        Handles.DrawWireDisc(transform.position, transform.forward, targetingRange);
    }
}
