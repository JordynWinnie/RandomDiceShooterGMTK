using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI ScoreUI;
    [SerializeField] private TextMeshProUGUI HealthUI;
    public static GameManager instance;
    private float _score = 0;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddScore(float score)
    {
        _score += score;
        UpdateScoreUI();
    }

    public void SetHealthUI(float health, float maxHealth)
    {
        HealthUI.SetText($"Health: {health:00}/{maxHealth:00}");
    }

    public void GameOver()
    {
        print("Game Over!");
    }

    private void UpdateScoreUI()
    {
        ScoreUI.SetText($"Score: {_score:00000000}");
    }
}
