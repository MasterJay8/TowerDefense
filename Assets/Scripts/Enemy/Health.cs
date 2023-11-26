using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int hitPoints;
    [SerializeField] int currencyWorth;


    public void TakeDamage(int dmg)
    {
        hitPoints -= dmg;
        if (hitPoints <= 0)
        {
            EnemySpawner.onEnemyDestroy.Invoke();
            Manager.main.IncreaseCurrency(currencyWorth);
            Destroy(gameObject);
        }
    }

}
