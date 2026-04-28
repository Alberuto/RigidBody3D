using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFlash : MonoBehaviour {

    public Image flashImage;      // Imagen del flash
    public float flashDuration = 0.5f;  // Duración del flash

    private void Start() {
        flashImage.gameObject.SetActive(false);
    }
    public void Flash() {
        flashImage.gameObject.SetActive(true);
        StartCoroutine(FlashCoroutine());
    }
    IEnumerator FlashCoroutine() {
        yield return new WaitForSeconds(flashDuration);
        flashImage.gameObject.SetActive(false);
    }
}