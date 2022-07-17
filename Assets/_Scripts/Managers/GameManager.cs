using System.Collections;
using System.Collections.Generic;
using Core;
using Networking.LookLocker;
using Player;
using Scene;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using WeaponNamespace;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int _score;
    [SerializeField] private int currentRoll = -1;

    [Header("Player")] [SerializeField] private GameObject player;

    [SerializeField] private WeaponCatalogueSO weaponCatalogue;
    public PlayerController _playerController;
    public PlayerMovement _playerMovement;
    public AudioManager _audioManager;

    [Header("Randomizer")] [SerializeField]
    private float maxRandInterval;

    [SerializeField] private float maxTime;
    [SerializeField] private float minTime;

    [Header("UI")] [SerializeField] private TextMeshProUGUI ScoreUI;

    [SerializeField] private TextMeshProUGUI HealthUI;
    [SerializeField] private TextMeshProUGUI intervalUI;
    [SerializeField] private TextMeshProUGUI timerUI;
    [SerializeField] private List<Sprite> dieSprites;
    [SerializeField] private Image RedScreen;
    [SerializeField] private Image diceImage;

    [Header("Components")] [SerializeField]
    private SceneController sceneController;

    [SerializeField] private LeaderboardManager leaderboardManager;
    private BuffManager _buffManager;
    private float _internalTimer;
    private bool _isRolling;
    private WeaponHandler weaponHandler;

    public float RandomizeInterval => maxRandInterval;
    public float CurrentRandInterval { get; private set; }

    public GameObject Player => player;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        if (player == null)
            Debug.LogError("Player not assigned in the GameManager.", gameObject);

        weaponHandler = player.GetComponent<WeaponHandler>();

        _playerController = player.GetComponent<PlayerController>();
        _playerMovement = player.GetComponent<PlayerMovement>();
        _audioManager = GetComponent<AudioManager>();

        CurrentRandInterval = maxRandInterval;
        _buffManager = GetComponent<BuffManager>();

        StartCoroutine(RollDice());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (_score >= 100)
            {
                _audioManager.PlayPurchaseSound();
                AddScore(-100);
                _internalTimer -= 5;
            }
            else
            {
                _audioManager.PlayErrorSound();
            }
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            if (_score >= 200)
            {
                _audioManager.PlayPurchaseSound();
                AddScore(-200);
                _internalTimer += 5;
            }
            else
            {
                _audioManager.PlayErrorSound();
            }
        }

        if (Input.GetKeyDown(KeyCode.M)) AudioListener.volume = AudioListener.volume == 1 ? 0 : 1;
        UpdateTimer();
    }

    private IEnumerator RollDice()
    {
        _audioManager.PlayDiceAudio();
        _isRolling = true;
        var timesChanged = 10;
        timerUI.SetText("Next Roll: Rolling...");
        var index = -1;
        while (timesChanged > 0)
        {
            diceImage.sprite = MathHelper.RandomFromList(dieSprites, out index);
            yield return new WaitForSeconds(0.1f);
            timesChanged -= 1;
        }

        _internalTimer = Random.Range(minTime, maxTime);
        currentRoll = index;
        _isRolling = false;
        _buffManager.ChangeBuff(currentRoll);
    }

    public void GunShotSound()
    {
        _audioManager.PlayGunAudio();
    }

    public void AbilityChimeSound()
    {
        _audioManager.AbilityChimeAudio();
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
        timerUI.SetText($"Next Roll: {_internalTimer:00}");
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
        StartCoroutine(ReturnHome());
    }

    private IEnumerator ReturnHome()
    {
        yield return new WaitForSeconds(2f);
        sceneController.ChangeScene(sceneController.MainSceneID);
        Cursor.lockState = CursorLockMode.None;
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