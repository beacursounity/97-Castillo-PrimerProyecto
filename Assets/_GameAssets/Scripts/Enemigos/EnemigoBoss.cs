using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoBoss : EnemigoMovil {

    [Header("DAÑO DEL PLAYER AL BOSS CUANDO CHOCAN")]
    [SerializeField] int danyoAlBoss = 1;

    private void Start()
    {
        // EL BOSS HACE MAS DAÑO AL PLAYER
        danyoAlPlayer = 8; // que realiza a nuestro personaje
    }

    // Update is called once per frame
    void Update () {
        // ACTUALIZAMOS LAS VIDAS
        //vidaBoss.text = " " + num;

        Debug.Log("update numenemigos "+numEnemigos);

        
        // EL BOSS SU DISTANCIA DE DETECCION ES MAYOR ME VERA ANTES
        distanciaDeteccion = 40;// DISTANCIA PARA PODER VER A NUESTRO PLAYER
                                    // ROTAMOS AL ENEMIGO
        Vector3 distancia = GetDistancia();
        //Debug.Log("distancia: " + distancia);        
        // EVALUAMOS SI LA DISTANCIA ES MENOR QUE LA DISTANCIA DE ATAQUE Y ATACA
        if (distancia.sqrMagnitude < (distanciaDeteccion * distanciaDeteccion))
        {
           //Debug.Log("EL BOSS ME ESTA MIRANDO");
           transform.LookAt(player.transform.position);
        }

        if (Player.estaVivo)
        {
            // AVANZAMOS
            Avanzar();
        }
        else
        {
            // PONEMOS EL SONIDO
            //this.GetComponent<AudioSource>().Play();
            Morir();
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("COLLISION ENTER BOSS");

        // METODO QUE ROTE ALEATORIAMENTE
        RotarAleatoriamente();

        // PARA SABER EL NOMBRE CON QUIEN NO HEMOS CHOCADO
        // SI ES PLAYER LO DESTRUIMOS 
        if (collision.gameObject.name == "Player" && Player.estaVivo )
        {
            Debug.Log("EL BOSS HACE DAÑO AL PLAYER");
            //estaVivo = false;

            // RECOGEMOS SU COMPONENTE PLAYER Y ASI PODREMOS LLAMAR A RECIBIRDANYO
            // LA VARIABLE DANYO TENDRIA QUE ESTAR EN PROTECTED EN EL PADRE PARA 
            // QUE PUEDA RECOGERLA SI NO DARIA ERROR
            // SI ES EL BOSS EL DAÑO QUE LE APLICA AL PLAYER SERA DE 8
            // SI ES CUALQUIER ENEMIGO EL DAÑO QUE LE APLICA AL PLAYER SERA DE 2
            Debug.Log("QUITA VIDA AL PLAYER: "+danyoAlPlayer);
            collision.gameObject.GetComponent<Player>().Recibirdanyo(danyoAlPlayer);

            // CUNSO CHOCA CON EL PLAYER, EL PLAYER LE HHACE UN DAÑO DE 1 MIENTRAS QUE EL
            // BOSS LE HARA UN DAÑO DE 8 AL PLAYER
            Debug.Log("QUITA VIDA AL BOSS: " + danyoAlBoss);
            Recibirdanyo("EnemigoBoss", danyoAlBoss);

  
           
        }
    }

    // CUANDO SIGUE DENTRO EL BOSS EN LA ZONA DE COLISION DEL PLAYER
    // LE SIGUE QUITANDO VIDAS
    private void OnCollisionStay(Collision collision)
    {
        //Debug.Log("COLLISION STAY BOSS");

        // METODO QUE ROTE ALEATORIAMENTE
        //RotarAleatoriamente();

        // PARA SABER EL NOMBRE CON QUIEN NO HEMOS CHOCADO
        // SI ES PLAYER LO DESTRUIMOS 
        if (collision.gameObject.name == "Player" && Player.estaVivo)
        {
            Debug.Log("EL BOSS HACE DAÑO AL PLAYER");
            // SI ES EL BOSS EL DAÑO QUE LE APLICA AL PLAYER SERA DE 8
            // SI ES CUALQUIER ENEMIGO EL DAÑO QUE LE APLICA AL PLAYER SERA DE 2
            Debug.Log("QUITA VIDA AL PLAYER: " + danyoAlPlayer);
            collision.gameObject.GetComponent<Player>().Recibirdanyo(danyoAlPlayer);

            // CUANDO CHOCA CON EL PLAYER, EL PLAYER LE HACE UN DAÑO DE 1 MIENTRAS QUE EL
            // BOSS LE HARA UN DAÑO DE 8 AL PLAYER
            // HABRA QUE ACTIVARLO
          //  Debug.Log("QUITA VIDA AL BOSS: " + danyoAlBoss);
            Recibirdanyo("EnemigoBoss", danyoAlBoss);



        }
    }
}
