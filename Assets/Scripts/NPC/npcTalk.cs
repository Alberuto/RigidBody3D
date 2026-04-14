using System.Collections;
using UnityEngine;

public class npcTalk : MonoBehaviour {
    
    [Header("UI")] 
        public GameObject panel;

    [Header("Player")] 
        public PlayerMove pm;
        public PlayerLook pl;

    private bool playerInside = false;
    private bool noActivar = false;

    private void Start() {

        if (panel != null)
            cerrarPanel();
    }
    public void cerrarPanel() {

        panel.SetActive(false);
        pl.SetCanMove(true);
        desactivarRaton();
        if (pm != null)
            pm.SetCanMove(true);
        StartCoroutine(NoActivarPanel());
    }
    public IEnumerator NoActivarPanel() {

        noActivar = true;
        playerInside = false;
        yield return new WaitForSeconds(5f);
        noActivar = false;
    }
    public void desactivarRaton() {

        Debug.Log("raton desactivado");
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    public void activarRaton() {

        Debug.Log("raton activado");
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    private void abrirPanel() {

        if (!playerInside)
            return;
        panel.SetActive(true);
        if (pm != null)
            pm.SetCanMove(false);
        if(pl!=null)
            pl.SetCanMove(false);
        activarRaton();
    }
    private void OnCollisionExit(Collision collision) {

        if (collision.gameObject.CompareTag("Player"))
            playerInside = false;
    }
    private void OnCollisionEnter(Collision collision) {
        
        if (noActivar)
            return;
        
        if (!collision.gameObject.CompareTag("Player"))
            return;
        
        playerInside = true;
        abrirPanel();
    }
}