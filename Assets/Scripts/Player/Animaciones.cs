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
    private void FixedUpdate() {

        Vector3 vWorld = rb.linearVelocity;
        Vector3 vLocal = transform.InverseTransformDirection(vWorld);
        animator.SetFloat("x", vLocal.x);
        animator.SetFloat("y", vLocal.z);
    }
    void Update() {


    }
}