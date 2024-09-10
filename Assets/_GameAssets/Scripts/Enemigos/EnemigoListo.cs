using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoListo : EnemigoMovil {

    // Use this for initialization
    void Start()
    {

        // RECOGEMOS LA VIDA MAXIMA PARA PODER HACER EL SLIDER
        maxVidaEListo = vidaEnemigoListo;

    }

    // Update is called once per frame
    void Update () {

        distanciaDeteccion = 20;// DISTANCIA PARA PODER VER A NUESTRO PLAYER
        // ROTAMOS AL ENEMIGO
        Vector3 distancia = GetDistancia();
       
        // EVALUAMOS SI LA DISTANCIA ES MENOR QUE LA DISTANCIA DE ATAQUE Y ATACA
        if (distancia.sqrMagnitude < (distanciaDeteccion * distanciaDeteccion)) {        
            transform.LookAt(player.transform.position);
        }

        // AVANZAMOS
        Avanzar(); // ESTE AVANZAR SERA DISTINTO QUE EL DEL E.TONTO
    }
}
