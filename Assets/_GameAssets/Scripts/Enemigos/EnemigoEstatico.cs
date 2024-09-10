﻿    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoEstatico: Enemigo {

    [SerializeField] float distanciaAtaque = 10.0f;

    [Header("DISPARO")]
    // HAY QUE RECOGER LA REFERENCIA DEL PUNTO DE GENERACION Y DE LA BALA
    [SerializeField] Transform puntoGeneracion; // MAS PRECISO EL TRANSFORM AUNQUE SE PUEDE PONER SU GO
    [SerializeField] GameObject prefabBala;
    // POTENCIA DISPARO
    [SerializeField] int potenciaDisparo = 100;
    float tiempoAtaque;
    float tiempoEntreDisparos = 2.0f;
    // RECOGEMOS EL AUDIO PARA QUE CUANDO DISPARE SE OIGA
    AudioSource audioTorreta;

    // ANIMACION TORRE DESTRUIDA
    Animator animacionTorreTop;


    // Use this for initialization
    void Start () {

        // SUMAMOS 1 AL ENEMIGO YA QUE EL RESTOS DE ENEMIGOS SE GENERAN A TRAVES DE UN GENERADOR DE ENEMIGO
        // Y SE PASA POR PARAMETRO CUANDO ENEMIGOS SE CREAN
        numEnemigos += 1;

        // RECOGEMOS LA VIDA MAXIMA PARA PODER HACER EL SLIDER
        maxVidaEEstatico = vidaEnemigoEstatico;

       // print("Vida estatico "+ maxVidaEEstatico);

        // RECOJO SU AUDIO PARA PODER REPRODUCIRLO
        audioTorreta = GetComponent<AudioSource>();
      
        // LO INICIALIZAMOS ASI PARA QUE AL PRINCIPIO QUE SE ACERQUE DISPARE EL E.ESTATICO
        tiempoAtaque = tiempoEntreDisparos;

        // RECOJO EL ANIMATOR QUE EN ESTE CASO NO TIENE NINGUN ANIMACION
        // PERO LUEGO LO NECESITAMOS PARA HACER ANIMACION QUE SE DESTRUYA
        animacionTorreTop = GetComponent<Animator>();
     

    }

   /* // Update is called once per frame
    protected override void Update()
    {
        base.Update(); 
        //Resto de codigo
    }*/

    // Update is called once per frame
   void Update() {

        //  base.Update(); // LLAMAMOS AL PADRE Y PONEMOS OVERRRIDE EN EL HIJO Y VIRTUAL EN EL PADRE
                           // ESTO NO HARIA FALTA YA QUE EN EL UPDATE PADRE NO TENGO NADA

          // SI EL PLAYER ESTA VIVO CONTINUA DISPARANDO LA TORRETA Y GIRANDO HACIA EL PLAYER
          if (Player.estaVivo) {
              // RECOGEMOS LA DISTANCIA ENTRE ENEMIGO ESTATICO Y PLAYER
              Vector3 distancia = GetDistancia();

              // QUE MIRE EL E.ESTATICO AL PLAYER
              // ESTA BIEN PERO CABECEA
              //transform.LookAt(player.transform.position);
              // PARA QUITAR EL CABECEO
              Vector3 target = new Vector3(player.transform.position.x,
                                           transform.position.y,
                                           player.transform.position.z);
                // transform.LookAt(target);
              transform.GetChild(1).LookAt(target);
                //print("distancia: " + distancia.sqrMagnitude);

                // UNITY RECOMIENDA USAR EL SQRMAGNITUDE Y MULTIPLICAR LAS DOS DISTANCIAS (ARITMETICA)
                // TAMBIEN SE PUEDE USAR EL SQRMAGNITUDE Y SIN MULTIPLICAR PERO NO SERIA LO CORRECTO

              // EVALUAMOS SI LA DISTANCIA ES MENOR QUE LA DISTANCIA DE ATAQUE Y ATACA
              if (distancia.sqrMagnitude < (distanciaAtaque * distanciaAtaque)) {

                  // LLAMAMOS AL INTENTOATAQUE
                  IntentoAtaque();
              }
          }
        
    }

    private void Disparar() {
        // HACEMOS LA INSTANTIATE DE LA BALA EN EL PUNTO DE GENERACION
        GameObject nuevaBala = Instantiate(prefabBala, puntoGeneracion.transform.position, puntoGeneracion.transform.rotation);

        // Y LE PONEMOS MUSICA DEL DISPARO
        audioTorreta.Play();

        // PARA HACER LA FUERZA CON RESPECTO AL MUNDO 
        // RELATIVA A NUESTO OBJECTO Y NO AL MUNDO
        nuevaBala.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * potenciaDisparo);
       
    }

    private void IntentoAtaque() {

        // CUENTO ES EL TIEMPO QUE PASA Y CUANDO PASO EL UMBRAL LO PONGO A 0
        tiempoAtaque += Time.deltaTime; 
        if ( tiempoAtaque >= tiempoEntreDisparos){
            tiempoAtaque = 0;

            Disparar();

        }

    }

    protected override void MuerteEnemigoEstatico()
    { 
        // ACTIVO LA ANIMACION
        animacionTorreTop.runtimeAnimatorController = amimacionTorreMuerte;

        // QUITAMOS LA BARRA DE VIDA
        Canvas barraVida = this.GetComponentInChildren<Canvas>();
           
        // QUITAMOS EL CANVAS PARA QUE NO SE VEA LA BARRA DE VIDA
        barraVida.enabled = false; 
      
    }

}
