using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour {

    [Header("Movimiento")]
    public float speed = 5f;
    public float jumpForce = 6f;
    private Rigidbody rb;
    private Vector2 moveInput;
    public bool isGrounded;

    [SerializeField]
    private float runMultiplier = 2f;
    private bool isRunning = false;

    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundRadius = 0.5f;

    [SerializeField] private Animaciones animator;


    void Start() {
        animator = GetComponent<Animaciones>();
        rb = GetComponent<Rigidbody>();
    }
    void Update() {
        isRunning = Keyboard.current != null &&
        (Keyboard.current.leftCtrlKey.isPressed ||
        Keyboard.current.rightCtrlKey.isPressed);
    }
    private void FixedUpdate() {

        Vector3 direction = transform.TransformDirection(new Vector3(moveInput.x, 0, moveInput.y));
        float currentSpeed = isRunning ? speed * runMultiplier : speed;
        Vector3 velocity = direction * currentSpeed;
        Vector3 newVelocity = new Vector3(velocity.x, rb.linearVelocity.y, velocity.z);
        rb.linearVelocity = newVelocity;
        CheckGround();

    }
    public void OnMove(InputValue value) {
        moveInput = value.Get<Vector2>();
    }
    void CheckGround() {

        Collider[] colliders = Physics.OverlapSphere(groundCheck.position, groundRadius);

        bool groundedNow = false;

        foreach (Collider collider in colliders) {
            
            if (collider.gameObject != gameObject) {
                groundedNow = true;
                //Debug.Log("Grounded tocando: " + collider.gameObject.name); debug del collider para verificar el toque de suelo
                break;
            }
        }
        if (groundedNow != isGrounded) { 

            isGrounded = groundedNow;
            animator.EnSuelo(isGrounded);
            //animations......
        }
    }
    /*
    void OnCollisionEnter(Collision collision) {
        if (collision.collider.CompareTag("suelo")) {
            isGrounded = true;
        }
    }
    void OnCollisionStay(Collision collision) {
        if (collision.collider.CompareTag("suelo")) {
            isGrounded = true;
        }
    }
    void OnCollisionExit(Collision collision) {
        if (collision.collider.CompareTag("suelo")) {
            isGrounded = false;
        }
    }*/
    public void OnJump(InputValue value) {
        if (value.isPressed && isGrounded) {
            animator.AnimacionSaltar1();
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;  //Evitar salto múltiple y problemas de timing
        }
    }
    private void OnDrawGizmosSelected() { //ver el icono de toque en suelo
        if (groundCheck == null) 
            return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundRadius);
    }
}