using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoListo : EnemigoMovil {
  
	
	// Update is called once per frame
	void Update () {

        distanciaDeteccion = 20;// DISTANCIA PARA PODER VER A NUESTRO PLAYER
        // ROTAMOS AL ENEMIGO
        Vector3 distancia = GetDistancia();
        //Debug.Log("distancia: " + distancia);        
        // EVALUAMOS SI LA DISTANCIA ES MENOR QUE LA DISTANCIA DE ATAQUE Y ATACA
        if (distancia.sqrMagnitude < (distanciaDeteccion * distanciaDeteccion)) {
           // Debug.Log("HA ENTRADO");         
            transform.LookAt(player.transform.position);
        }

        // AVANZAMOS
        Avanzar(); // ESTE AVANZAR SERA DISTINTO QUE EL DEL E.TONTO
    }
}
