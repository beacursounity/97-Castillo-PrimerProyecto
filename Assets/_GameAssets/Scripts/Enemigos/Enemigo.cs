using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// ESTAMOS HACIENDO EL ENEMIGO BASE
public class Enemigo : MonoBehaviour {

    [Header("ESTADO")]
    [SerializeField] protected bool estaVivo = true;
    [SerializeField] int vida = 10;
    [SerializeField] bool enPosicion = false;

    [SerializeField] protected int velocidad = 10;

    [Header("ATAQUE")]
    [SerializeField] protected int distanciaDeteccion; // PARA EL ENEMIGO QUE SE MUEVE
    [SerializeField] protected int danyoAlPlayer = 2; // que realiza a nuestro personaje 

    // Tenemos que tener nuestro personaje para saber la distancia pero respecto del player
    [Header("REFERENCIAS")]

    protected GameObject player; // PARA SABER DONDE ESTA SOLO NECESITO SU TRANSFORM
    protected GameObject enemigoBoss; // PARA SABER DONDE ESTA SOLO NECESITO SU TRANSFORM

    //[SerializeField] Transform posicionFPS; // PARA SABER DONDE ESTA EL TRANSFORM DE NUESTRO PLAYER

    [Header("FX")]
    [SerializeField] protected ParticleSystem explosion;
    

    [Header("NUMERO ENEMIGOS")]
    // NUMERO DE ENEMIGOS QUE TENGO EN LA ESCENA HABRA QUE MODIFICARLO CUANDO ESTE TERMINADO
    // LO PONEMOS ESTATICO Y PUBLICO PARA QUE SE PUEDA VER EN TODO EL PROYECTO 
    // PERO SOLO SEA DE ESTE SCRIPT Y NO DE SUS HIJOS YA QUE SI SON DE SUS HIJOS SERA
    // UNA VARIABLE INDEPENDIENTE A CADA UNO DE ELLOS SI SE LES PONE PROTECTED.
    [SerializeField] public static int numEnemigos = 7;

    //int speed;
    //int distanciaExplosion; // 
    // LE HACEMOS DAÑO AL ENEMIGO Y NO SE REGENERA

    private void Awake() {
        // LO COGEMOS ASI PORQUE EL PLAYER ES UN PREFAB Y ASI COGERA MEJOR
        // LA REFERENCIA.
        player = GameObject.Find("Player");
        
    }

  
  
    protected virtual void Update()
    {
     //   vida = 100;
    }

    // PARA RECOGER LA DISTACIA ENTRE EL PLAYER Y LA POSICION DEL TRANSFORM DEPENDIENDO
    // DESDE DONDE SE LLAME
    protected Vector3 GetDistancia() {
        return player.transform.position - transform.position;
    }

    // El Enemigo muere y ya esta
    public void Morir() {
        //Debug.Log("MURIENDO");
        /*
        1. Indicar que esta muerto 
        2. Sistema de particulas
        3. Gritos horibles de dolor / Despedirse de los seres queridos
        4. Destruir el enemigo
        5.¿Aumentar salud? ¿Incremetar puntuacion? ¿Recompensas?....
        */
        // LANZAMOS LA EXPLOSION
        // LO HEMOS PUESTO EL PS INDEPENDIENTEMENTE PARA PODER HACER EL DESTUIR Y QUE NO SE
        // DESTRUYAN LAS BALAS
        explosion = Instantiate(explosion, transform.position, Quaternion.identity);
        // ESTADOVIVO A FALSE
        estaVivo = false;
        // EXPLOSION
        explosion.Play();


        // QUITAMOS EL NUMERO ENEMIGOS PARA QUE APAREZCA EL BOSS CUANDO
        // YA NO TENGAMOS ENEMIGOS
        if (numEnemigos > 0) {
            Debug.Log("ENEMIGO MUERE. ME QUEDAN " + numEnemigos);
            numEnemigos = numEnemigos - 1;
        }

        Destroy(this.gameObject);
        

        // CUANDO DESTRUYA AL ENEMIGO BOSS TENDREMOS QUE ACABAR LA PARTIDA O CUANDO
        // SE MUERA EL PLAYER

        // CUANDO NOS CARGUEMOS AL BOSS SE TERMINARA TB EL JUEGO
        /*if (transform.tag == "EnemigoBoss") {
            Debug.Log("ENEMIGO.MORIR: VAMOS A CARGARNOS A NUESTRO BOSS");
            // LLAMAMOS A LA ESCENA DE INICIO DEL JUEGO
            SceneManager.LoadScene(0);
        }*/

    }

    // Todos los enemigos reciben un daño
    // EL DAÑO LO DA EL ARMA QUE TENDREMOS QUE DECIDIRLO NOSOTROS
    public void Recibirdanyo(int danyo) 
     {
        //Debug.Log("Recibir daño");
        // Restamos la vida 
        vida = vida - danyo;
        // vida si es 0 se tiene que morir los enemigos
        if (vida <= 0) 
        {
            vida = 0;
            Morir();
        }
        
    }

    /*protected void Destruir()
    {
        Debug.Log("SE TIENE QUE DESTRUIR EL ENEMIGO");
        //Destroy(this.gameObject);
    }*/

    // CUANDO PUSEMOS SOBRE ESA GEOMETRIA SALGA EL DEBU POR CONSOLA
    // 
    /*private void OnMouseDown() {
        Debug.Log("PULSADO CON EL RATON");
        // es el daño que producimos al Player
        Recibirdanyo(10);
    }*/
}
