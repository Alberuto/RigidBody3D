using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class NpcTalk2 : MonoBehaviour
{
    [Header("Panel de diálogo con el NPC")]
    public GameObject dialoguePanel;        // panel que se abre al hablar con el NPC
    public TMP_Text titleText;              // "Busca: ..."
    public TMP_Text descriptionText;        // descripción del objeto
    public Button okButton;                 // botón para cerrar el panel

    [Header("Player")]
    public PlayerMove pm;
    public PlayerLook pl;

    [Header("Quest")]
    public NPCQuestManager questManager;     // referencia al NPCQuestManager del NPC

    private bool playerInside = false;

    private void Awake() {
        if (dialoguePanel != null) {
            dialoguePanel.SetActive(false);
        }
        if (okButton != null) {
            okButton.onClick.RemoveAllListeners();
            okButton.onClick.AddListener(CerrarPanel);
        }
    }
    public void CerrarPanel() {
        if (dialoguePanel != null) {
            dialoguePanel.SetActive(false);
        }
        pl.SetCanMove(true);
        if (pm != null) {
            pm.SetCanMove(true);
        }
    }
    public void activarRaton() {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    private void abrirPanel() {
        Debug.Log("abrirPanel ejecutado");

        if (!playerInside || dialoguePanel == null)
            return;

        StartCoroutine(CerrarPanelConDelay(5f));

        // Actualizamos el texto con el objetivo actual
        if (questManager != null) {
            QuestItemData current = questManager.CurrentItem;

            if (titleText != null)
                titleText.text = "Busca: " + current.itemName;

            if (descriptionText != null)
                descriptionText.text = current.description;
        }
        else {
            if (titleText != null)
                titleText.text = "No tengo más que pedirte.";
            if (descriptionText != null)
                descriptionText.text = "Gracias por tu ayuda.";
        }
        dialoguePanel.SetActive(true);
        pm.SetCanMove(false);
        pl.SetCanMove(false);
        activarRaton();
    }
    // Trigger cuando el player entra en el trigger del NPC
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            playerInside = true;
            abrirPanel();
        }
    }
    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("Player")) {
            playerInside = false;
        }
    }
    public IEnumerator CerrarPanelConDelay(float delay = 5f) {
        yield return new WaitForSeconds(delay);
        CerrarPanel();
        GameController.Instance.StartTimer();
    }
}