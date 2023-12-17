using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] HealthBar healthBar;
    [SerializeField] int health;
    [SerializeField] int currencyWorth;
    [SerializeField] Animator animator;


    [SerializeField] AnimationClip animationComponent;
    float animationLenght;
    float deathStart;

    private void Awake()
    {
        animationLenght = animationComponent.length;
        healthBar.SetMaxHealth(health);
    }
    public void TakeDamage(int dmg)
    {
        health -= dmg;
        healthBar.SetHealth(health);
        if (health <= 0)
        {
            animator.SetTrigger("Death");
            //deathStart += Time.deltaTime;

            //if (deathStart >= 1f / animationLenght)
            //{
                EnemySpawner.onEnemyDestroy.Invoke();
                Manager.main.IncreaseCurrency(currencyWorth);
                Destroy(gameObject);
            //}


        }
    }

}
