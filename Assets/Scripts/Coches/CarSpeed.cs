using TMPro;
using UnityEngine;

public class CarSpeed : MonoBehaviour {

    private Rigidbody carRigidbody;
    public TextMeshProUGUI carSpeedText;
    public float speed { get; private set; }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {

        if  (carRigidbody==null)
             carRigidbody = GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void Update() {
        speed = carRigidbody.linearVelocity.magnitude * 3.6f;
        carSpeedText.text = speed.ToString("0")+"km/h";
    }
}