using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLook : MonoBehaviour {

    [Header("Sensitivity")]    
        public float mouseSensitivity = 0.15f;

    [Header("Pitch clamp")]   
        public float minPitch = -20f;
        public float maxPitch =  40f;
    
    private Vector2 lookInput; //valores del raton eje cartesiano
    private float pitch;

    private Transform cameraTransform;
    private Camera currentCamera;
    private bool canMove = true;

    void Start() {

        UpdateActiveCamera();
        Cursor.lockState = CursorLockMode.Locked; //puntero raton locked
        Cursor.visible = false;                   // and hidden
    }
    void Update() {

        if (!canMove)
            return;
        if (cameraTransform==null)
            return;
        //rotacion horizontal
        float yaw = lookInput.x * mouseSensitivity;
        transform.Rotate(0,yaw,0,Space.Self);
        //rotacion camara
        pitch-=lookInput.y * mouseSensitivity;
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);
        cameraTransform.localRotation = Quaternion.Euler(pitch,0,0);
        UpdateActiveCamera();
    }
    private void UpdateActiveCamera() {

        if (Camera.main != currentCamera) { 

            currentCamera = Camera.main;

            if (currentCamera != null) {
                cameraTransform = currentCamera.transform;
                pitch = cameraTransform.localEulerAngles.x;
                if (pitch > 180f) 
                    pitch -= 360f;
            }
        }
    }
    public void SetCanMove(bool value) { 
        canMove = value;
    }
    public void OnLook(InputValue value) { //input system tienen que ser publicos
        lookInput = value.Get<Vector2>();
    }
    private void OnEnable() { //reiniciar el puntero
        lookInput=Vector2.zero;
    }
}