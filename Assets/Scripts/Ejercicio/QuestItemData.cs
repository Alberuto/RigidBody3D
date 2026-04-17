using UnityEngine;

[CreateAssetMenu(menuName = "Game/Quest Item")]
public class QuestItemData : ScriptableObject {

    public string itemName;               // nombre del item
    public string description;           // "Taza azul con grieta", etc.
    public string itemTag;              // "TazaAzul", "LibroRojo", ...
    public GameObject itemPrefab;       // referencia al prefab que buscan
    public int id;                      // opcional, por si quieres usar int
}