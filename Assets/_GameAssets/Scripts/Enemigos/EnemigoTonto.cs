using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UIElements.UxmlAttributeDescription;
using UnityEngine.PlayerLoop;

public class EnemigoTonto : EnemigoMovil
{  

    // Use this for initialization
    void Start () {

        // RECOGEMOS LA VIDA MAXIMA PARA PODER HACER EL SLIDER
        maxVidaETonto = vidaEnemigoTonto;

    }

    // Update is called once per frame
    void Update () {

        distanciaDeteccion = 0;// NUESTRO ENEMIGO TONTO NO DETECTARA A NUESTRO PLAYER
        Avanzar(); // ESTE AVANZAR SERA DISTINTO QUE EL DEL E.TONTO
        
	}
    

}

