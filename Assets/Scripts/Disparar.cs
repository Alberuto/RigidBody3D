using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Disparar : MonoBehaviour {

    [Header("Punta de disparo")] 
        public Transform puntoDisparo;

    [Header("Prefab Bala")]
        public GameObject prefabBala;

    public Animaciones animator;
    public float retraso = 0.25f;
    public AudioSource audioSource;
    public GameObject arma;
    public GameObject mano;
    public GameObject espalda;
    private void Disparado() {

        if (prefabBala == null || puntoDisparo == null) 
            return;

        audioSource.Play();
        StartCoroutine(DisparoDelay());
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void OnDisparar(InputValue value) {

        animator.AnimacionDisparar();

        if(value.isPressed)
            Disparado();
    }
    public IEnumerator DisparoDelay () {
        conmutarPistola();
        yield return new WaitForSeconds(retraso);        
        GameObject bala = Instantiate(prefabBala, puntoDisparo.position, puntoDisparo.rotation);
        soltarPistola();
    }
    public void conmutarPistola() {
        arma.transform.SetParent(mano.transform);
        arma.transform.localPosition = new Vector3(0.004f, 0.144f,-0.044f);
        arma.transform.localRotation = Quaternion.Euler(96.845f, 124.873f, -77.505f);
    }
    public void soltarPistola() {
        arma.transform.SetParent (espalda.transform);
        arma.transform.localPosition = new Vector3(-0.02531169f, 0.2417175f, -0.2214009f);
        arma.transform.localRotation = Quaternion.Euler(32.773f, 262.642f, -25.383f);
    }
}