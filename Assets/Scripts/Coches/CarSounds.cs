using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class CarSounds : MonoBehaviour {

    public Rigidbody carRigidbody;

    public AudioSource engineAudioSource;
    public AudioSource claxonAudioSource;
    public AudioSource engineOnAudioSource;
    public AudioSource brakingAudioSource;

    public float minPitch = 0.8f;
    public float maxPitch = 2.0f;
    public float maxSpeed = 50f;
    public float pitchSmoothSpeed = 3.0f;
    public CarController2 carController;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        
        if (carRigidbody == null)
            carRigidbody = GetComponent<Rigidbody>();

        engineAudioSource.loop = true;
        engineOnAudioSource.loop = false;
        claxonAudioSource.loop = false;
        brakingAudioSource.loop = false;

        engineAudioSource.Stop();
        engineOnAudioSource.Stop();
        claxonAudioSource.Stop();
        brakingAudioSource.Stop();

    }
    // Update is called once per frame
    void Update() {
        
    }
    private IEnumerator SonidoArranqueMotor() { 
    
        engineOnAudioSource.Play();
        yield return new WaitWhile(() => engineOnAudioSource.isPlaying);
        engineAudioSource.Play();
    }
    public void OnArrancar(InputValue playerValue) {

        if (playerValue.isPressed) {

            carController.isOn = !carController.isOn;

            if (carController.isOn) {
                StartCoroutine(SonidoArranqueMotor());
            }
            else {
                engineAudioSource.Stop();
                engineOnAudioSource.Stop();
            }
        }
    }
    public void OnClaxon(InputValue playerValue) {
        if (playerValue.isPressed && !claxonAudioSource.isPlaying && carController.isOn) { 
            claxonAudioSource.Play();
        }
    }
    /*public void OnJump(InputAction.CallbackContext context) {
        if (context.performed) {
                brakingAudioSource.Play();
        }
        else if (context.canceled) {
                brakingAudioSource.Stop();
        }
    }*/
    public void OnJump(InputValue playerValue) {

        if (playerValue.isPressed && !brakingAudioSource.isPlaying && carController.isOn) {
            brakingAudioSource.Play();
        }
        if (!playerValue.isPressed && brakingAudioSource.isPlaying) {
            brakingAudioSource.Stop();
        }
    }
}