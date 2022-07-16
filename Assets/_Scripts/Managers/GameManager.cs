using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Core;
using Player;
using WeaponNamespace;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private float _score = 0;

    [Header("Player")]
    [SerializeField] private GameObject player;
    [SerializeField] private WeaponCatalogueSO weaponCatalogue;
    private WeaponHandler weaponHandler;

    [Header("Randomizer")]
    [SerializeField] private float maxRandInterval;
    private float _randInterval;

    public float RandomizeInterval => maxRandInterval;
    public float CurrentRandInterval => _randInterval;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI ScoreUI;
    [SerializeField] private TextMeshProUGUI HealthUI;
    [SerializeField] private TextMeshProUGUI intervalUI;
    [SerializeField] private Image RedScreen;

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

    private void Start()
    {
        if (player == null)
            Debug.LogError($"Player not assigned in the GameManager.", gameObject);

        weaponHandler = player.GetComponent<WeaponHandler>();
        _randInterval = maxRandInterval;
    }

    private void Update()
    {
        RandomizerLogic();
    }

    private void RandomizerLogic()
    {
        if (_randInterval <= 0f)
        {
            weaponHandler.EquipWeapon(MathHelper.RandomFromIntZeroTo(weaponCatalogue.WeaponList.Length), MathHelper.RandomFromIntZeroTo(AssetManager.instance.BulletPrefabArray.Length));
            _randInterval = maxRandInterval;
        }
        else
        {
            _randInterval -= Time.deltaTime;
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

    public IEnumerator FlashScreenRed()
    {
        var color = RedScreen.color;

        color.a = 0.5f;

        RedScreen.color = color;
        while (color.a >= 0)
        {
            color.a -= 0.01f;
            RedScreen.color = color;
            yield return new WaitForSeconds(0.01f);
        }
    }

    private void UpdateScoreUI()
    {
        ScoreUI.SetText($"Score: {_score:00000000}");
    }
}