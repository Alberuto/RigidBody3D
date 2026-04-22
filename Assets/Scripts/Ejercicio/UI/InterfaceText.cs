using UnityEngine;
using TMPro;

public class InterfaceText : MonoBehaviour {

    public TMP_Text interfaceText;  // Texto de interfaz
    public NPCQuestManager questManager;
    public NPCDialogueHandler dialogueHandler;

    private void Update() {
        if (questManager == null || dialogueHandler == null)
            return;

        if (questManager.IsQuestCompleted) {
            interfaceText.text = "Has completado todas las tareas.";
        }
        else if (questManager.objetosEncontrados > 0 && questManager.objetosEncontrados < questManager.questItems.Count) {
            interfaceText.text = "Objeto encontrado. Vuelve al NPC para que te pida el siguiente.";
        }
        else {
            interfaceText.text = "Busca el objeto: " + questManager.CurrentItem.itemName;
        }
    }
}