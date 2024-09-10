using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoMovil : Enemigo {

    [Header("Enemigo Movil")]
    [SerializeField] int inicioRotacion = 1;
    [SerializeField] int tiempoRotacion = 2;

    bool pausarRotarAleatoriamente = false;

    void Start()
    {
        //InvokeRepeating("RotarAleatoriamente", inicioRotacion, tiempoRotacion);
    }

 
    protected void RotarAleatoriamente() {
        // PAUSAR ROTACION PARA CUANDO ESTE COLISIONANDO CON EL PLAYER NO AFECTE
        if (!pausarRotarAleatoriamente)
        {
            // LE VAMOS HACER UN EJE DE ROTACION ALEATORIO enter 0 y 360
            float rotacion = Random.Range(0f, 360F);

            // VAMOS A ROTAR SOBRE EL EJE DE LA Y 
            transform.eulerAngles = new Vector3(0, rotacion, 0);
        }
    }

    // METODO AVANZAR COMUN PARA TODOS LOS ENEMIGOS
    // LO PONEMOS PROTECT PARA QUE PODAMOS LLAMARLO EN EL E.TONTO Y E.LISTO
    // YA QUE LOS DOS VAN HACER LO MISMO.
    protected void Avanzar() {
        // SOLO SI ESTA VIVO AVANZA Y EL PLAYER TODAVIA VIVE
        if (estaVivo && Player.estaVivo) {
           //transform.Translate(Vector3.forward * Time.deltaTime * velocidad);
        }   
    }

    private void OnCollisionEnter(Collision collision) {

        //print("CHOCA CON  "+collision.gameObject.name);
        // PARA SABER EL NOMBRE CON QUIEN NO HEMOS CHOCADO
        // SI ES PLAYER LO DESTRUIMOS 
        if (collision.gameObject.name == "Player" && Player.estaVivo)
          
        {
            // CUANDO CHOCA CON EL ENEMIGO TONTO ESTE SE DA LA VUELTA
            if (gameObject.name == "EnemigoTonto")
            {
                // PAUSAR EL INVOKEREPEATING PARA QUE SOLO SE USE ESTA ROTACION DEL ENEMIGO
                pausarRotarAleatoriamente = true;

                // PARA QUE ROTE EN SENTIDO CONTRARIO AL QUE SE DIRIGE EL ENEMIGO
                float rotacionNuevaY = 0f;

                if (transform.rotation.y == 0f)
                {
                    rotacionNuevaY = -180f;
                }
                else rotacionNuevaY = player.transform.rotation.y * -1;

                // MODIFICAMOS SU ROTACION  
                transform.eulerAngles = new Vector3(transform.rotation.x, rotacionNuevaY, transform.rotation.z);
            }

            // DAÑO AL PLAYER
            collision.gameObject.GetComponent<Player>().Recibirdanyo(danyoAlPlayer);

            // DAÑO AL ENEMIGO
            Recibirdanyo(gameObject.tag, danyoAlEnemigo);

            // DEJAMOS QUE EL INVOKEREPEATING SIGA SU CURSO POR SI HA CHOCADO CON EL ENEMIGOTONTO
            pausarRotarAleatoriamente = false;
     
        }
        else if (collision.gameObject.name != "Terrain")
        {
            // METODO QUE ROTE ALEATORIAMENTE
            RotarAleatoriamente();
        }

    }

     // no me funciona mirarlo mas despacio
     //private void OnParticleCollision(GameObject other) {
     //   print("ha colisionado con las particulas "+ other.gameObject.name);
     //}
 }
