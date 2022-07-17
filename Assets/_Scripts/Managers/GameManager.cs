using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Core;
using Player;
using WeaponNamespace;
using Scene;
using Networking.LookLocker;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int _score = 0;
    private bool _isRolling = false;
    private float _internalTimer = 0f;
    private BuffManager _buffManager;
    [SerializeField] private int currentRoll = -1;

    [Header("Player")]
    [SerializeField] private GameObject player;
    [SerializeField] private WeaponCatalogueSO weaponCatalogue;
    private WeaponHandler weaponHandler;
    public PlayerController _playerController;
    public PlayerMovement _playerMovement;

    [Header("Randomizer")]
    [SerializeField] private float maxRandInterval;

    [SerializeField] private float timer;
    private float _randInterval;

    public float RandomizeInterval => maxRandInterval;
    public float CurrentRandInterval => _randInterval;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI ScoreUI;
    [SerializeField] private TextMeshProUGUI HealthUI;
    [SerializeField] private TextMeshProUGUI intervalUI;
    [SerializeField] private TextMeshProUGUI timerUI;
    [SerializeField] private List<Sprite> dieSprites;
    [SerializeField] private Image RedScreen;
    [SerializeField] private Image diceImage;

    [Header("Components")]
    [SerializeField] private SceneController sceneController;
    [SerializeField] private LeaderboardManager leaderboardManager;

    public GameObject Player => player;

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
        
        _playerController = player.GetComponent<PlayerController>();
        _playerMovement = player.GetComponent<PlayerMovement>();
        
        _randInterval = maxRandInterval;
        _buffManager = GetComponent<BuffManager>();
        
        StartCoroutine(RollDice());
    }

    private void Update()
    {
        RandomizerLogic();
        UpdateTimer();
    }

    IEnumerator RollDice()
    {
        _isRolling = true;
        var timesChanged = 20;
        timerUI.SetText($"Next Roll:\nRolling...");
        var index = -1;
        while (timesChanged > 0)
        {
            diceImage.sprite = MathHelper.RandomFromList(dieSprites, out index);
            yield return new WaitForSeconds(0.1f);
            timesChanged -= 1;
        }

        _internalTimer = timer;
        currentRoll = index;
        _isRolling = false;
        _buffManager.ChangeBuff(currentRoll);
    }
    
    

    private void UpdateTimer()
    {
        if (_isRolling) return;
        if (_internalTimer < 0)
        {
            StartCoroutine(RollDice());
            return;
        }
        _internalTimer -= Time.deltaTime;
        timerUI.SetText($"Next Roll:\n{_internalTimer:00}");
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

    public void AddScore(int score)
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
        leaderboardManager.SubmitScore(_score);
        sceneController.ChangeScene(sceneController.MainSceneID);
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

    public void AddHealth(float health)
    {
        _playerController.AddHealth(health);
    }
    
}