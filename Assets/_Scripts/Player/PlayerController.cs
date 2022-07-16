using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IDamageable
{
    [SerializeField] private float maxHealth;
    private float currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
        GameManager.instance.SetHealthUI(currentHealth, maxHealth);
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        GameManager.instance.SetHealthUI(currentHealth, maxHealth);
        StartCoroutine(GameManager.instance.FlashScreenRed());
        if (currentHealth <= 0)
        {
            GameManager.instance.GameOver();
        }
    }
}
