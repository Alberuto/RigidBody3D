using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Disparar : MonoBehaviour {

    [Header("Punta de disparo")] 
        public Transform puntoDisparo;

    [Header("Prefab Bala")]
        public GameObject prefabBala;

    public Animaciones animator;
    public float retraso = 0.5f;

    private void Disparado() {

        if (prefabBala == null || puntoDisparo == null) 
            return;

        StartCoroutine(DisparoDelay());
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void OnDisparar(InputValue value) {

        animator.AnimacionDisparar();

        if(value.isPressed)
            Disparado();
        
    }
    public IEnumerator DisparoDelay () { 
        
        yield return new WaitForSeconds(retraso);
        
        GameObject bala = Instantiate(prefabBala, puntoDisparo.position, puntoDisparo.rotation);
    }
}