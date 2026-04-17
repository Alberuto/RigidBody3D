using UnityEngine;

public class PlayerInteraction : MonoBehaviour {

    public NPCQuestManager npcQuest;

    private void OnTriggerEnter(Collider other) {
        // Detectar cuando el jugador se acerca al objeto
        if (npcQuest == null || npcQuest.IsQuestCompleted) return;

        if (npcQuest.IsTargetItem(other.gameObject)) {

            // en UI o via evento, muestras: "¡Encontraste <nombre>!"
            Debug.Log("Objeto encontrado: " + npcQuest.CurrentItem.itemName);

            // marcar como encontrado y pasar al siguiente
            npcQuest.NextItem();
        }
    }
}