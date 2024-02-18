using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] HealthBar healthBar;
    [SerializeField] int health;
    [SerializeField] int currencyWorth;
    [SerializeField] Animator animator;
    string ghost = "Ghost";


    [SerializeField] AnimationClip animationComponent;
    float animationLength;
    //EnemySpawner enemySpawner = enemyObject.GetComponent<>()
    //gameObject.GetComponent<Health>().TakeDamage(1, 1);
    //bulletScript = bulletObject.GetComponent<Bullet>();

    private void Awake()
    {
        animationLength = animationComponent.length;
        healthBar.SetMaxHealth(health);       
    }
    public void TakeDamage(int dmg)
    {
        health -= dmg;
        healthBar.SetHealth(health);
        
        if (health <= 0)
        {
            animator.SetTrigger("Death");
            gameObject.layer = LayerMask.NameToLayer(ghost);
            StartCoroutine(WaitForAnimation());
        }
    }
    private IEnumerator WaitForAnimation()
    {
        yield return new WaitForSeconds(animationLength);

        // After waiting for the animation to complete
        EnemySpawner.onEnemyDestroy.Invoke();
        Manager.main.IncreaseCurrency(currencyWorth);
        Destroy(gameObject);
    }

}
