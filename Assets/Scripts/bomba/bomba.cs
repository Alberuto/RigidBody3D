using UnityEngine;

public class bomba : MonoBehaviour {

    private void OnCollisionEnter(Collision collision) {

        Destroy(gameObject);
    }
    private void Start() {
        



    }
}