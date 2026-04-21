using UnityEngine;
using TMPro;

public class NPCDialogueHandler : MonoBehaviour {

    public NPCQuestManager questManager;      // referencia al NPCQuestManager
    public GameObject dialoguePanel;          // el panel de diálogo
    public TMP_Text titleText;                // título del mensaje
    public TMP_Text descriptionText;          // descripción del objeto

    public void OpenDialogue() {

        if (questManager == null || questManager.IsQuestCompleted) {
            titleText.text = "No tengo más que pedirte.";
            descriptionText.text = "Gracias por tu ayuda.";
        }
        else {
            QuestItemData current = questManager.CurrentItem;
            titleText.text = "Busca: " + current.itemName;
            descriptionText.text = current.description;
        }
        dialoguePanel.SetActive(true);
    }
    public void CloseDialogue() {
        dialoguePanel.SetActive(false);
    }
}