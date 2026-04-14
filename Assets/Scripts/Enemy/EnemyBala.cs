using UnityEngine;
using UnityEngine.UIElements;

public class EnemyBala : MonoBehaviour {

    Animator animator;
    bool muerto = false;
    public AudioSource audio;
    public Animaciones animaciones;
    [SerializeField] private GameObject efectoSangre;
    [SerializeField] private Transform puntoSangrado;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        
        animator = GetComponent<Animator>();
    }
    private void OnCollisionEnter(Collision collision) {

        if (muerto) 
            return;

        if (collision.gameObject.tag == "bala")  {
            Destroy(collision.gameObject);
            SangrarCollision(collision);
            muerto = true;
            animator.SetTrigger("Muerto");
            audio.Play();
        }
    }
    private void OnTriggerEnter(Collider other) {

        if (muerto)
            return;

        if (other.gameObject.tag == "mano" && animaciones.golpeoPosible()) {
            SangrarTrigger(other);
            muerto = true;
            animator.SetTrigger("Muerto");
            audio.Play();
        }
    }
    private void SangrarCollision(Collision other) {

        Vector3 posicion = other.contacts[0].point;
        Sangrado(posicion);
    }
    private void SangrarTrigger(Collider other) { 
        
        Vector3 posicion = other.transform.position;
        Sangrado(posicion);
    }
    private void Sangrado(Vector3 posicion) {
        GameObject sangre = Instantiate(efectoSangre, posicion, Quaternion.identity);
        sangre.transform.SetParent(transform.GetChild(1));
        Debug.Log("sangra");
        Destroy (sangre, 6f);
    }
    // Update is called once per frame
    void Update() {
        
    }
}