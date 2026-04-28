using System.Collections;
using UnityEngine;

public class FlashEffect : MonoBehaviour {
    public GameObject flashCanvas;
    public float flashDuration = 0.25f;

    public void PlayFlash() { 
        if (flashCanvas == null)
            return;

        StartCoroutine(FlashRoutine());
    }
    private IEnumerator FlashRoutine() {
        flashCanvas.SetActive(true);
        yield return new WaitForSeconds(flashDuration);
        flashCanvas.SetActive(false);
    }
}