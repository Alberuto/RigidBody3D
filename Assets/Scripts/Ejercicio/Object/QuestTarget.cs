using UnityEngine;

public class QuestTarget : MonoBehaviour  {
    
    public QuestItemData itemData;

    private void Awake() {
        // asegurar que el prefab tiene un tag correcto
        if (!string.IsNullOrEmpty(itemData?.itemTag)) {

            gameObject.tag = itemData.itemTag;
        }
    }
    // Detecta cuando el jugador entra en el collider
    private void OnTriggerEnter(Collider other) {
        // Solo si el NPC lo está buscando
        NPCQuestManager questManager = FindObjectOfType<NPCQuestManager>();
        PlayerInteraction playerInteraction = other.GetComponent<PlayerInteraction>();

        if (questManager != null && playerInteraction != null) {
            if (questManager.IsTargetItem(gameObject)) {
                Debug.Log("Objeto encontrado: " + questManager.CurrentItem.itemName);
                // Avanza el objetivo
                questManager.NextItem();
                // Cierra el panel si está abierto
                NpcTalk2 npcTalk = FindObjectOfType<NpcTalk2>();
                if (npcTalk != null) {
                    npcTalk.cerrarPanel();
                }
            }
        }
    }
    // Detecta cuando el jugador sale del collider
    private void OnTriggerExit(Collider other) {
        // No hace nada aquí, pero se puede usar para otras cosas
    }
}