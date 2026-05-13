using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraSwitcher : MonoBehaviour {

    [Header("Camaras")] 
        public List<Camera> camaras;

    private int indiceActual = 0;   

    void Start() {

        ActivarCamara(indiceActual);
    }
    private void ActivarCamara(int indiceActual) {

        for (int i = 0; i < camaras.Count; i++) {

            camaras[i].gameObject.SetActive(i==indiceActual);

            if (i == indiceActual)
                camaras[i].gameObject.tag = "MainCamera";
            else
                camaras[i].gameObject.tag = "Untagged";
        }
    }
    private void cambiarCamara() {
        if (camaras.Count > 0) {
            indiceActual++;

            if(indiceActual >= camaras.Count)
                indiceActual = 0;
            ActivarCamara(indiceActual);
        }
    }
    public void OnCambioCamara(InputValue value) { 

        if (value.isPressed)
            cambiarCamara();
    }
    void Update() {
        
    }
}