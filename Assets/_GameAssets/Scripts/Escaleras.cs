using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Escaleras : MonoBehaviour {
    GameObject puertaDcha;
    private void Awake() {
        puertaDcha = GameObject.Find("Door_TorreDcha");
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("PUERTA ONTRIGGERENTER");
        if (other.gameObject.name == "Player")
        {
            Debug.Log("llamada a la funcion AbrirPuerta");
            puertaDcha.GetComponent<Puerta>().AbrirPuerta();
        }
    }
    /*private void OnTriggerExit(Collider other) {
        
        if (other.gameObject.name == "Player") {
            Debug.Log("llamada a la funcion CerrarPuerta");
            puertaDcha.GetComponent<Puerta>().CerrarPuerta();
        }
        
    }*/
}
