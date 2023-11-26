using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public static Manager main;
    public int currency;

    private void Awake()
    {
        main = this;
    }
    private void Start()
    {
        currency = 100;
    }
    public void IncreaseCurrency(int amount)
    {
        currency += amount;
    }
    public bool SpendCurrency(int amount)
    {
        if (amount <= currency)
        {
            currency -= amount;
            return true;
        }
        else
        {
            Debug.Log("You dont have enough money");
            return false;
        }
    }
}
