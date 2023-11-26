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
    public void IncreaseCurrency(int ammout)
    {
        currency += ammout;
    }
    public bool SpendCurrency(int ammout)
    {
        if (ammout <= currency)
        {
            currency -= ammout;
            return true;
        }
        else
        {
            Debug.Log("You dont have enough money");
            return false;
        }
    }
}
