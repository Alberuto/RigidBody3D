using UnityEngine;

public class QuestTarget : MonoBehaviour {

    public QuestItemData itemData;

    private void Awake() {

        // asegurar que el prefab tiene un tag correcto
        if (!string.IsNullOrEmpty(itemData?.itemTag)) {

            gameObject.tag = itemData.itemTag;
        }
    }
}