using UnityEngine;

public class Animaciones : MonoBehaviour {

    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody rb;

    public bool puedeGolpear = false;
    void Start() {

        if(!animator)
            animator = GetComponent<Animator>();
        if(!rb)
            rb = GetComponent<Rigidbody>();
        //puedeGolpear = false;
        Debug.Log("start animaciones");
    }
    public void puedoGolpear() {
        Debug.Log("puedo golpear");
        puedeGolpear = true;
    }
    public void noPuedoGolpear() {
        Debug.Log("No puedo golpear");
        puedeGolpear = false;
    }
    public bool golpeoPosible() {
        return puedeGolpear;
    }
    public void EnSuelo(bool value) {
        animator.SetBool("EnSuelo",value);
    }
    public void AnimacionSaltar1() {
        animator.SetTrigger("Saltar");    
    }
    public void AnimacionSaltar2() {
        animator.SetTrigger("Saltar2");
    }
    public void AnimacionDisparar() {
        animator.SetTrigger("Disparo");
    }
    public void AnimacionGolpear() {
        animator.SetTrigger("Golpear");
    }
    private void FixedUpdate() {
        Vector3 vWorld = rb.linearVelocity;
        Vector3 vLocal = transform.InverseTransformDirection(vWorld);
        animator.SetFloat("x", vLocal.x);
        animator.SetFloat("y", vLocal.z);
        animator.SetFloat("VelocidadVertical", vLocal.y);
    }
    void Update() {

    }
}