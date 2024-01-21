using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;

    [SerializeField] float bulletSpeed;
    [SerializeField] int bulletDamage;
    [SerializeField] string bulletType;

    Transform target;

    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    private void FixedUpdate()
    {
        //if (!target) return;
        if (!target) Destroy(gameObject);

        Vector2 direction = (target.position - transform.position).normalized;
        rb.velocity = direction * bulletSpeed;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.gameObject.GetComponent<Health>().TakeDamage(bulletDamage, bulletType);
        Destroy(gameObject);
    }
}
