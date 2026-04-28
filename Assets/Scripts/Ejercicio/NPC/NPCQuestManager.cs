using System.Collections.Generic;
using UnityEngine;

public class NPCQuestManager : MonoBehaviour {

    public List<QuestItemData> questItems;
    private int currentIndex = 0;
    public int objetosEncontrados = 0;
    public QuestItemData CurrentItem => questItems[currentIndex];

    public bool IsQuestCompleted => currentIndex >= questItems.Count;

    public void NextItem() {
        if (!IsQuestCompleted) {
            currentIndex++;
        }
        Debug.Log("Indice aumentado");
    }
    public bool IsTargetItem(GameObject target) {

        if (CurrentItem == null || target == null) 
            return false;
        // Opción 1: por tag
        if (!string.IsNullOrEmpty(CurrentItem.itemTag))  {

            return target.CompareTag(CurrentItem.itemTag);
        }
        // Opción 2: por referencia al prefab (si quieres más exacto)
        QuestTarget questTarget = target.GetComponent<QuestTarget>();
        if (questTarget != null) {

            return questTarget.itemData == CurrentItem;
        }
        return false;
    }
}