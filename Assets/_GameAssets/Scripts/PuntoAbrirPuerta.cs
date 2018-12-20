using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuntoAbrirPuerta : MonoBehaviour {
    GameObject puertaDcha;
    private void Awake() {
        puertaDcha = GameObject.Find("Door_TorreDcha");
    }
    private void OnTriggerEnter(Collider other) {
        Debug.Log("BANDERA ONTRIGGERENTER");
        if (other.gameObject.name == "Player") {
            Debug.Log("llamada a la funcion AbrirPuerta");
            puertaDcha.GetComponent<Puerta>().AbrirPuerta();
        }
    }
}
