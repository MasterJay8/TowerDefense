using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;
using UnityEngine.iOS;


public class ScoreboardManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreUI;

    string scoreboardText;

    void Start()
    {
        string filePath = Application.persistentDataPath + "/score.txt";
        Debug.Log(filePath);

        if (File.Exists(filePath))
        {
            
            string[] lines = File.ReadAllLines(filePath);
            int[] scores = new int[lines.Length];

            for (int i = 0; i < lines.Length; i++)
            {
                if (int.TryParse(lines[i], out scores[i]))
                {

                }
            }

            Array.Sort(scores);
            Array.Reverse(scores);

            string scoreboardString = "Scoreboard:\n";
            for (int i = 0; i < Mathf.Min(10, scores.Length); i++)
            {
                scoreboardString += (i + 1) + ". " + scores[i] + "\n";
            }

            scoreboardText = scoreboardString;
        }
        else
        {
            scoreboardText = "No scores yet.";
        }
    }
    private void OnGUI()
    {
        scoreUI.text = scoreboardText;
    }
}