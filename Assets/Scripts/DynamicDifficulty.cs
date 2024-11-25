using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicDifficulty : MonoBehaviour
{
    private static List<int> scores = new List<int>(new int[MaxScores]);
    private const int MaxScores = 4;

    private int Difficulty;

    void Awake()
    {
        LoadRecentsScores();
    }

    public void AddScore(int score)
    {
        scores.Add(score);

        if (scores.Count > MaxScores)
        {
            scores.RemoveAt(0);
        }
        SaveRecentScores();
        LoadRecentsScores();
        UpdateDifficulty();
    }

    // Calculate average and update difficulty
    private void UpdateDifficulty()
    {
        if (scores.Count == 0) return;

        float average = 0;
        foreach (int score in scores)
        {
            //Debug.Log("score: " + score);
            average += score;
        }
        average /= scores.Count;
        //Debug.Log("avg: " + average);

        // Map average to a difficulty rating from 1 to 10
        Difficulty = Mathf.Clamp(Mathf.CeilToInt(average / 3f), 1, 10);
        Debug.Log("Difficulty " +  Difficulty);
        PlayerPrefs.SetInt("Difficulty", Difficulty);
    }

    public static void SaveRecentScores()
    {
        for (int i = 0; i < MaxScores; i++)
        {
            PlayerPrefs.SetInt("RecentScores" + i, scores[i]);
        }
        PlayerPrefs.Save();
    }

    public static void LoadRecentsScores()
    {
        for (int i = 0; i < MaxScores; i++)
        {
            scores[i] = PlayerPrefs.GetInt("RecentScores" + i, 0);
            //Debug.Log("Player Prefs: " + PlayerPrefs.GetInt("RecentScores" + i, 0));
        }
    }
}
