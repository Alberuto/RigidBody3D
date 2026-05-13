using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CarController2 : MonoBehaviour {

    [Header("Wheel Colliders")]
    
    public WheelCollider frontLeftWheel;
    public WheelCollider frontRightWheel;
    public WheelCollider rearLeftWheel;
    public WheelCollider rearRightWheel;

    [Header("Wheel Meshes")]

    public Transform frontLeftMesh;
    public Transform frontRightMesh;
    public Transform rearLeftMesh;
    public Transform rearRightMesh;

    [Header("Car Settings")]
    
    public float motorForce = 1500f;
    public float breakForce = 1500f;
    public float maxSteerAngle = 30f;

    public Rigidbody rb;

    public float brakeVelocityMultiplier = 0.94f;
    public float minSpeedForExtraBrake = 0.25f;

    [Header("Freno al soltar acelaracion")]
    
    public float autoBrakeForce = 800f;
    public float autoBrakeVelocityMultiplier = 0.985f;
    public float deadZoneAcceleration = 0.05f;

    public GameObject imageStop;
    private PlayerInput playerInput; 
    private InputAction moveAction;  // X e Y
    private InputAction brakeAction; //asociado a la accion jump que a su vez se asocia al espacio generalmente
    private float accelerationInput; // cursor Y
    private float steeringInput; // cursor X

    private bool isBraking; //pulsado freno

    public bool isOn = true;

    private void Start() {
        
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions["Move"]; //wasd
        brakeAction = playerInput.actions["Jump"];//space

        if (imageStop != null) { 
            imageStop.SetActive(false);
        }

    }
    public void Update() {

        if (!isOn) { 
            accelerationInput = 0f;
            steeringInput = 0f;
            isBraking = false;
            UpdateWheelMeshes();
            return;
        }

        Vector2 moveInput = moveAction.ReadValue<Vector2>();

        steeringInput = moveInput.x;
        accelerationInput = moveInput.y;
        isBraking = brakeAction.IsPressed();
        MostrarImagenStop();        //mostrar la imagen de stop clonando y modificando la del coche actual
    }
    private void FixedUpdate() {

        if(!WheelsAssigned())
            return;
        if (!isOn) {
            StopMotorForce();
            ApplyBrakeTorque(0f);
        }
        HandleMotor();
        HandleSteering();
        HandleBraking();
    }
    private void HandleBraking() {

        float currentBrakeForce = 0f;
        if (isBraking) {
            currentBrakeForce=breakForce;
        }
        else if (Mathf.Abs(accelerationInput)<deadZoneAcceleration) {
            currentBrakeForce = autoBrakeForce;
        }
        ApplyBrakeTorque(currentBrakeForce);
        if (isBraking) {
            ApplyExtraBrake();
        }
        else if (Mathf.Abs(accelerationInput) < deadZoneAcceleration) {
            ApplyAutoBrake();
        }
    }
    private void ApplyBrakeTorque(float currentBrakeForce) {
        frontLeftWheel.brakeTorque = currentBrakeForce;
        frontRightWheel.brakeTorque = currentBrakeForce;
        rearLeftWheel.brakeTorque = currentBrakeForce;
        rearRightWheel.brakeTorque = currentBrakeForce;
    }
    private void ApplyExtraBrake() {
        if (rb == null) {
            return;
        }
        float speed = rb.linearVelocity.magnitude;
        if (speed < minSpeedForExtraBrake) { 
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            return;
        }
        rb.linearVelocity *= brakeVelocityMultiplier;
    }
    private void ApplyAutoBrake() {
        if (rb == null) {
            return;
        }
        float speed = rb.linearVelocity.magnitude;
        if (speed < minSpeedForExtraBrake) {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            return;
        }
        rb.linearVelocity *= autoBrakeVelocityMultiplier;
    }
    private void UpdateWheelMeshes() {

        UpdateSingleWheel(frontLeftWheel, frontLeftMesh);
        UpdateSingleWheel(frontRightWheel, frontRightMesh);
        UpdateSingleWheel(rearLeftWheel, rearLeftMesh);
        UpdateSingleWheel(rearRightWheel, rearRightMesh);

    }
    private void UpdateSingleWheel(WheelCollider Wheel, Transform Mesh) {
        
        if (Wheel == null || Mesh == null)
            return;

        Vector3 position;
        Quaternion rotation;

        Wheel.GetWorldPose(out position, out rotation);
        Mesh.position = position;
        Mesh.rotation = rotation;
    }
    private void HandleSteering() {
        float steerAngle = steeringInput * maxSteerAngle;
        frontLeftWheel.steerAngle = steerAngle;
        frontRightWheel.steerAngle = steerAngle;
    }
    private void HandleMotor() {
        if (isBraking) {
            StopMotorForce();
            return;
        }
        if (Math.Abs(accelerationInput) < deadZoneAcceleration) {
            StopMotorForce();
            return;
        }
        float motorTorque = accelerationInput * motorForce;
        rearLeftWheel.motorTorque = motorTorque;
        rearRightWheel.motorTorque = motorTorque;
    }
    private void StopMotorForce() {
        rearLeftWheel.motorTorque = 0;
        rearRightWheel.motorTorque = 0;
        frontLeftWheel.motorTorque = 0;
        frontRightWheel.motorTorque = 0;
    }
    private bool WheelsAssigned() {
        if (frontLeftWheel == null || frontRightWheel == null ||
            rearLeftWheel == null || rearRightWheel == null) 
            return false;
        else
            return true;
    }
    private void MostrarImagenStop() {
        if (imageStop == null)
            return;
        imageStop.SetActive(isBraking);
    }
}