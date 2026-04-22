using System.Collections;
using UnityEngine;
using TMPro;

public class NpcTalk2 : MonoBehaviour {

    [Header("UI")]
    public GameObject panel;
    public TMP_Text titleText;
    public TMP_Text descriptionText;

    [Header("Player")]
    public PlayerMove pm;
    public PlayerLook pl;

    [Header("Quest")]
    public NPCQuestManager questManager;

    private bool playerInside = false;

    private void Start() {
        if (panel != null) {
            panel.SetActive(false);
        }
    }
    public void cerrarPanel() {

        panel.SetActive(false);
        pl.SetCanMove(true);
        if (pm != null) {
            pm.SetCanMove(true);
        }
        // No bloqueamos el ratón aquí
    }
    public void activarRaton() {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    private void abrirPanel() {

        if (!playerInside || panel == null)
            return;

        // Actualiza el texto del panel
        if (questManager != null && !questManager.IsQuestCompleted) {

            QuestItemData current = questManager.CurrentItem;
            if (titleText != null) {
                titleText.text = "Busca: " + current.itemName;
            }
            if (descriptionText != null) {
                descriptionText.text = current.description;
            }
        }
        else {
            if (titleText != null) {
                titleText.text = "No tengo más que pedirte.";
            }
            if (descriptionText != null) {
                descriptionText.text = "Gracias por tu ayuda.";
            }
        }
        panel.SetActive(true);
        pm.SetCanMove(false);
        pl.SetCanMove(false);
        activarRaton();
        StartCoroutine(cerrarPanelConDelay());
    }
    // Usamos Collider en lugar de Collision para que funcione bien
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
    public IEnumerator cerrarPanelConDelay(float delay = 15f) {
        yield return new WaitForSeconds(delay);
        cerrarPanel();
    }
}