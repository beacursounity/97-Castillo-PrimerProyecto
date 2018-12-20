using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puerta : MonoBehaviour {

    [SerializeField] bool abierta = false;
    bool primeraVez = false;

    int numRotaciones;

	// Use this for initialization
	void Start () {
		//StartCoroutine(OpenDoor());
	}

    // Update is called once per frame
    void Update() {
        if (primeraVez) {
            Debug.Log("ES LA PRIMERA VEZ");
            if (abierta == false) {
                Debug.Log("Se esta abriendo");
                transform.Rotate(new Vector3(0, -1 , 0));
                numRotaciones += 1;
                Debug.Log("Rotaciones " + numRotaciones);
                if (numRotaciones == 90) {
                    abierta = true;
                    primeraVez = false;
                    numRotaciones = 0;
                    Invoke("CerrarPuerta", 4);
                }
            } else {
                Debug.Log("Se esta cerrando");
                numRotaciones = 0;
                transform.Rotate(new Vector3(0, 1, 0));
                numRotaciones += 1;
                if (numRotaciones == 90) {
                    abierta = false;
                    primeraVez = false;
                    numRotaciones = 0; 
                }
            }
        }

    }

    public void AbrirPuerta()
    {
        Debug.Log("Abrir Puerta");
        abierta = false;
        primeraVez = true;
    }

    public void CerrarPuerta() {
        Debug.Log("Cerrar Puerta");
        abierta = true;
        primeraVez = true;
    }

    // O TAMBIEN SE PUEDE HACER CON CORRUTINA
    // SON LLAMADAS SUCESIVAS EN EL PUNTO DONDE
    // SE QUEDO, LE DAMOS UNA PAUSA 
    /*public IEnumerator OpenDoor() {
        for ( int i= 0;i < 128; i++) {

            transform.Rotate(new Vector3(0, -1, 0));
            yield return null;
        }
    }*/
}
