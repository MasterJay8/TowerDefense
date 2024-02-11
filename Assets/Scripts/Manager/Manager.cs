using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class Manager : MonoBehaviour
{
    public static Manager main;
    public int currency;
    public int baseHealth = 10;
    public int score;

    private void Awake()
    {
        main = this;
    }
    private void Start()
    {
        currency = 100;
        InvokeRepeating("IncreaseScoreOverTime", 1.0f, 1.0f);
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
            string filePath = Application.persistentDataPath + "/score.txt";

            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                writer.WriteLine(score.ToString());
            }
            SceneManager.LoadScene("MainMenu");
        }
    }
    void IncreaseScoreOverTime()
    {
        score += 1;
    }
}
