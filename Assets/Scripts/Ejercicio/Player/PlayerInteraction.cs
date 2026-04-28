using UnityEngine;

public class PlayerInteraction : MonoBehaviour {

    public NPCQuestManager npcQuest;
    public AudioSource audioSource;
    public AudioClip successClip;
    public FlashEffect flashEffect;

    private void OnTriggerEnter(Collider other) {
        // Detectar cuando el jugador se acerca al objeto
        if (npcQuest == null || npcQuest.IsQuestCompleted) 
            return;

        if (npcQuest.IsTargetItem(other.gameObject)) {

            // en UI o via evento, muestras: "¡Encontraste <nombre>!"
            Debug.Log("Objeto encontrado: " + npcQuest.CurrentItem.itemName);

            if (audioSource != null && successClip != null)
                audioSource.PlayOneShot(successClip);

            if (flashEffect != null)
                flashEffect.PlayFlash();

            // Manda al GameController
            GameController.Instance.AddFoundItem();
            GameController.Instance.StopTimer();
            npcQuest.NextItem();

        }
    }
}