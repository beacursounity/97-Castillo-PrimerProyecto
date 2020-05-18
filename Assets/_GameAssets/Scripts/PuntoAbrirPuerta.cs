using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PuntoAbrirPuerta : MonoBehaviour {
    [SerializeField] GameObject puertaDcha;

    Animator miAnimatorPuerta;
    void Star() {
        print("Awake PuntoAbrirPuerta");
        miAnimatorPuerta = puertaDcha.GetComponent<Animator>();
        miAnimatorPuerta.SetBool("Abierta",false);
    }
    void OnTriggerEnter(Collider other) {
        print("BANDERA ONTRIGGERENTER");
        if (other.gameObject.name == "Player") {
            //Debug.Log("llamada a la funcion AbrirPuerta");
            //puertaDcha.GetComponent<Puerta>().AbrirPuerta();
            miAnimatorPuerta.SetBool("Abierta",true);
        }
    }
}
