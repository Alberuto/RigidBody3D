using System.Collections;
using UnityEngine;

public class bombSpawner : MonoBehaviour {

    [SerializeField] 
        private GameObject bomb;

    [SerializeField]
        private Transform player;

    [SerializeField]
        private float distanciaMaxima = 2f;

    [SerializeField]
        private float altura = 20f;

    [SerializeField]
        private float intervalo = 2f;

    public PlayerMove pm;


    void Start() {

        StartCoroutine(Spawn());
    }
    public IEnumerator Spawn() {

        while (true) {

            /*if (pm.muerto)
                break;*/

            Vector2 direccion2D = Random.insideUnitCircle.normalized;
            Vector3 direccion3D = new Vector3(direccion2D.x, 0 , direccion2D.y);
            float distancia = Random.Range(0, distanciaMaxima);
            Vector3 posicionSpawn = player.position + direccion3D * distancia;
            posicionSpawn.y = altura;

            Instantiate(bomb, posicionSpawn, Quaternion.identity);
            yield return new WaitForSeconds(intervalo);
        }
    }
}