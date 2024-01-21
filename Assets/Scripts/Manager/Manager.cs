using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    public static Manager main;
    public int currency;
    public int baseHealth = 10;

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
    public void TakeBaseHealth()
    {
        baseHealth--;
        if (baseHealth <= 0)
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
