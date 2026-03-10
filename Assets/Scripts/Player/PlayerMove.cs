using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour {

    [Header("Movimiento")]
        public float speed = 5f;
        public float jumpForce = 6f;
        private Rigidbody rb;
        private Vector2 moveInput;
        private bool isGrounded;

    [SerializeField]
        private float runMultiplier=2f;
        private bool isRunning=false;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start() {
            rb= GetComponent<Rigidbody>();
        }
        void Update() {
            
        }
        private void FixedUpdate() {
            Vector3 direction = transform.TransformDirection(new Vector3(moveInput.x, 0, moveInput.y));

            isRunning = Keyboard.current != null && (Keyboard.current.leftCtrlKey.isPressed || Keyboard.current.rightCtrlKey.isPressed);

            float currentSpeed = isRunning ? speed * runMultiplier : speed;
            Vector3 velocity = direction * currentSpeed;
            Vector3 newVelocity = new Vector3(velocity.x, rb.linearVelocity.y, velocity.z);
            rb.linearVelocity = newVelocity;
        }
        public void OnMove(InputValue value) {
            moveInput = value.Get<Vector2>();
        }
        public void OnJump(InputValue value) {
            if (value.isPressed && isGrounded) {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
        }
}