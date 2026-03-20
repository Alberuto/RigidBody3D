using UnityEngine;

public class Animaciones : MonoBehaviour {

    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody rb;

    void Start() {

        if(!animator)
            animator = GetComponent<Animator>();
        if(!rb)
            rb = GetComponent<Rigidbody>();
    }
    public void AnimacionSaltar1() {

        animator.SetTrigger("Saltar");    
    }
    public void AnimacionSaltar2() {

        animator.SetTrigger("Saltar2");
    }
    private void FixedUpdate() {

        Vector3 vWorld = rb.linearVelocity;
        Vector3 vLocal = transform.InverseTransformDirection(vWorld);
        animator.SetFloat("x", vLocal.x);
        animator.SetFloat("y", vLocal.z);
    }
    void Update() {

    }
}