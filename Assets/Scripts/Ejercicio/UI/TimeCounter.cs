using System.Collections;
using UnityEngine;
using TMPro;

public class TimeCounter : MonoBehaviour {

    public float timeLimit = 60f;  // Tiempo límite
    public TMP_Text timeText;     // Texto del tiempo
    public NPCQuestManager questManager;

    private float currentTime;
    private bool isRunning = false;

    private void Start() {
        currentTime = timeLimit;
        isRunning = true;
    }
    private void Update() {

        if (!isRunning)
            return;

        currentTime -= Time.deltaTime;

        if (currentTime <= 0f) {

            currentTime = 0f;
            isRunning = false;
            // Aquí puedes hacer algo cuando el tiempo se acaba
            // Por ejemplo, mostrar un mensaje de "Tiempo agotado"
        }
        if (timeText != null) {
            timeText.text = "Tiempo: " + Mathf.Ceil(currentTime).ToString();
        }
    }
    public void StartTimer() {
        currentTime = timeLimit;
        isRunning = true;
    }
    public void StopTimer() {
        isRunning = false;
    }
}