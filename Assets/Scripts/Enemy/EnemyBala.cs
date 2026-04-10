using UnityEngine;

public class EnemyBala : MonoBehaviour {

    Animator animator;
    bool muerto = false;
    public AudioSource audio;
    public Animaciones animaciones;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        
        animator = GetComponent<Animator>();
    }
    private void OnCollisionEnter(Collision collision) {

        if (muerto) 
            return;

        if (collision.gameObject.tag == "bala")  {
            Destroy(collision.gameObject);
            muerto = true;
            animator.SetTrigger("Muerto");
            audio.Play();
        }
    }
    private void OnTriggerEnter(Collider other) {

        if (muerto)
            return;

        if (other.gameObject.tag == "mano" && animaciones.golpeoPosible()) {
            muerto = true;
            animator.SetTrigger("Muerto");
            audio.Play();
        }
    }
    // Update is called once per frame
    void Update() {
        
    }
}