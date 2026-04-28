using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour {
    public static GameController Instance;

    [Header("Game State")]
    public int maxLives = 5;
    public int currentLives = 5;
    public int totalItems = 20;
    public int foundItems = 0;

    [Header("Timer")]
    public float roundTime = 60f;
    public float currentTime;
    public bool timerRunning;

    [Header("UI")]
    public TMP_Text timeText;
    public TMP_Text livesText;
    public TMP_Text progressText;
    public TMP_Text objetiveText;

    public GameObject winPanel;
    public GameObject losePanel;

    [Header("Quest")]
    public NPCQuestManager questManager;   // referencia al NPCQuestManager del NPC

    public event Action OnTimerEnded;          // cuando el tiempo se acaba
    public event Action<int, int> OnLivesChanged;
    public event Action<int, int> OnProgressChanged;

    private void Awake() {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    private void Start() {
        // suscribir el evento ON TIMER ENDED
        OnTimerEnded += OnTimerEndedHandler;
        ResetGameState();
        UpdateUI();
        // si quieres, puedes empezar la primera ronda aquí
    }
    private void Update() {
        if (!timerRunning) 
            return;
        currentTime -= Time.deltaTime;
        if (currentTime <= 0f) {
            currentTime = 0f;
            timerRunning = false;
            OnTimerEnded?.Invoke();   // no usar el mismo nombre de método
            UpdateUI();
            return;
        }
        UpdateUI();
    }
    public void ResetGameState() {
        currentLives = maxLives;
        foundItems = 0;
        currentTime = roundTime;
        timerRunning = false;
        UpdateUI();
    }
    public void StartTimer() {
        currentTime = roundTime;
        timerRunning = true;
        UpdateUI();
    }
    public void StopTimer() {
        timerRunning = false;
        UpdateUI();
    }
    public void AddFoundItem() {
        foundItems = Mathf.Clamp(foundItems + 1, 0, totalItems);
        OnProgressChanged?.Invoke(foundItems, totalItems);
        UpdateUI();

        if (foundItems >= totalItems) {
            timerRunning = false;
            Debug.Log("todo completo");
            winPanel?.SetActive(true);
        }

    }
    public void LoseLife(int amount = 1) {
        currentLives = Mathf.Max(0, currentLives - amount);
        if (OnLivesChanged != null)
            OnLivesChanged.Invoke(currentLives, maxLives);
        UpdateUI();
        if (currentLives <= 0) {
            timerRunning = false;
            losePanel?.SetActive(true);
            Debug.Log("sin vidas");
            Time.timeScale = 0f;
        }
    }
    public void SetTotalItems(int value) {
        totalItems = Mathf.Max(1, value);
        UpdateUI();
    }
    private void UpdateUI() {
        if (timeText != null)
            timeText.text = Mathf.CeilToInt(currentTime).ToString("00") + "s";

        if (livesText != null)
            livesText.text = currentLives + "/" + maxLives;

        if (progressText != null)
            progressText.text = foundItems + "/" + totalItems;

        if (objetiveText != null && questManager != null) {
            if (questManager.IsQuestCompleted) {
                objetiveText.text = "No hay más objetivos.";
            }
            else {
                objetiveText.text = "Objetivo: " + questManager.CurrentItem.itemName;
            }
        }
    }
    private void OnTimerEndedHandler() {
        // cuando el tiempo se acaba, pierde una vida
        LoseLife();
       // questManager.NextItem();
    }
    public void Restart() {
        GameController.Instance.ResetGameState();

        // vuelve a la velocidad normal
        Time.timeScale = 1f;

        GameController.Instance.winPanel?.SetActive(false);
        GameController.Instance.losePanel?.SetActive(false);
    }
}