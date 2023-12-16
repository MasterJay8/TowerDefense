using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int hitPoints;
    [SerializeField] int currencyWorth;
    [SerializeField] Animator animator;

    [SerializeField] AnimationClip animationComponent;
    float animationLenght;
    float deathStart;

    private void Awake()
    {
        animationLenght = animationComponent.length;
    }
    public void TakeDamage(int dmg)
    {
        hitPoints -= dmg;
        if (hitPoints <= 0)
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
