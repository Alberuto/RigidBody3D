using UnityEngine;

public class CarAnalogSpeed : MonoBehaviour {

    public CarSpeed carSpeed;
    public RectTransform needle;
    private int maxSpeedKmh = 200;
    private float minNeedleAngle = 120f;
    private float maxNeedleAngle = -120f;
    private float speedMultiplier = 0.4f;
    void Start() {
        
    }
    void Update() {
        
        if(carSpeed==null || needle==null) return;

        float speed = carSpeed.speed * speedMultiplier;
        float clampedSpeed = Mathf.Clamp(speed, 0, maxSpeedKmh);
        float normalizedSpeed = clampedSpeed / maxSpeedKmh;
        float needleAngle = Mathf.Lerp(minNeedleAngle,maxNeedleAngle, normalizedSpeed);
        needle.localEulerAngles = new Vector3(0f, 0f, needleAngle);
    }
}