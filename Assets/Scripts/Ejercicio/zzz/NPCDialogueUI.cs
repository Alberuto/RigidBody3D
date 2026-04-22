using TMPro;
using UnityEngine;

public class NPCDialogueUI : MonoBehaviour {

    public NPCQuestManager npcQuest;
    public TMP_Text titleText;
    public TMP_Text descriptionText;

    public void RefreshQuest() {

        if (npcQuest == null || npcQuest.IsQuestCompleted) {

            titleText.text = "¡No hay más misiones!";
            descriptionText.text = "Gracias por ayudar.";
            return;
        }
        QuestItemData current = npcQuest.CurrentItem;
        titleText.text = "Busca: " + current.itemName;
        descriptionText.text = current.description;
    }
}