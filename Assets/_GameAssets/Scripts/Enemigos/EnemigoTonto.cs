using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoTonto : EnemigoMovil {  

    // Use this for initialization
    void Start () {
		//velocidad 
        // VA A EMPEZAR A ANDAR Al 1 SEGUNDOS DE EMPEZAR 
        // Y LO REPITE CADA 2
        //InvokeRepeating("RotarAleatoriamente",inicioRotacion,tiempoRotacion);
	}
	
	// Update is called once per frame
	void Update () {
        distanciaDeteccion = 0;// NUESTRO ENEMIGO TONTO NO DETECTARA A NUESTRO PLAYER
        Avanzar(); // ESTE AVANZAR SERA DISTINTO QUE EL DEL E.TONTO
        
	}

    /*private void OnCollisionEnter(Collision collision) {
        //Debug.Log("ha entrado");

        // METODO QUE ROTE ALEATORIAMENTE
        RotarAleatoriamente();

        // PARA SABER EL NOMBRE CON QUIEN NO HEMOS CHOCADO
        // SI ES PLAYER LO DESTRUIMOS 
        if (collision.gameObject.name == "Player")
        {
            estaVivo = false;
           
            // RECOGEMOS SU COMPONENTE PLAYER Y ASI PODREMOS LLAMAR A RECIBIRDANYO
            // LA VARIABLE DANYO TENDRIA QUE ESTAR EN PROTECTE EN EL PADRE PARA 
            // QUE PUEDA RECOGERLA SI NO DARIA ERROR
            collision.gameObject.GetComponent<Player>().Recibirdanyo(danyo);

            // SE HACE UN INSTANTIATE EN MODO DE EJECUCION Y LUEGO SE PUEDE HACER UN PLAY
            //ParticleSystem ps = Instantiate(explosion, transform);
            //ps.Play(); // PLAY PARA QUE SALGAN LAS PARTICULAS
            // DESACTIVAMOS
            //GetComponent<Renderer>().enabled = false; // EL RENDERER EL QUE DIBUJA EL OBJETO
            //Invoke("Destruir",1);

            // el Destroyinmediate se destruye y el Destroy 
            //termina de ejecutar si hay algo por detras de codigo

            // SE HACE UN INSTANTIATE EN MODO DE EJECUCION Y LUEGO SE PUEDE HACER UN PLAY
            // HACEMOS EL INSTANTIATE PERO DESDE LA POSICION 
            ParticleSystem ps = Instantiate(explosion, transform.position,Quaternion.identity);
            estaVivo = false;
            ps.Play(); // PLAY PARA QUE SALGAN LAS PARTICULAS
            // YA NO HACE FALTA DESACTIVAR EL RENDER YA QUE NO HACEMOS EL TRANSFORM
            //GetComponent<Renderer>().enabled = false; // EL RENDERER EL QUE DIBUJA EL OBJETO
            //Invoke("Destruir", 1);
            Destruir();
        }


    }
*/
    

}

