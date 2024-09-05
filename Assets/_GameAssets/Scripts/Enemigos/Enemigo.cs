using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// ESTAMOS HACIENDO EL ENEMIGO BASE
public class Enemigo : MonoBehaviour {

    //PROTECTED PARA PODERLAS VER EN EL HIJOm

    [Header("ESTADO")]
    [SerializeField] protected bool estaVivo = true;

    [Header("VIDAS")]
    protected float maxVidaETonto, maxVidaEListo, maxVidaEEstatico, maxVidaEBoss;
    [SerializeField] float vidaEnemigoTonto;
    [SerializeField] float vidaEnemigoListo;
    public float vidaEnemigoEstatico;
    [SerializeField] float vidaEnemigoBoss;
    //[SerializeField] bool enPosicion = false;

    [SerializeField] protected int velocidad = 10; // DEL MOVIMIENTO DEL ENEMIGO MOVIL

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

    [Header("SONIDOS ENEMIGOS")]
    SoundManager soundManager;

    [Header("MUERTE TORRE")]
   public AnimatorOverrideController amimacionTorreMuerte;

    [Header("SLIDER VIDAS ENEMIGOS")]
    private BarraVida barraVida;

    private void Awake() {
        // LO COGEMOS ASI PORQUE EL PLAYER ES UN PREFAB Y ASI COGERA MEJOR
        // LA REFERENCIA.
        player = GameObject.Find("Player");

        // BUSCAMOS NUESTRO GO QUE CONTIENE TODOS LOS SONIDOS
        soundManager = FindObjectOfType<SoundManager>();

        // RECOGEMOS EL COMPONENTE DE LA BARRAVIDA
        barraVida = GetComponentInChildren<BarraVida>();

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
        // LO HEMOS PUESTO EL PS INDEPENDIENTEMENTE PARA PODER HACER EL DESTRUIR Y QUE NO SE
        // DESTRUYAN LAS BALAS
        explosion = Instantiate(explosion, transform.position, Quaternion.identity);
        // ESTADOVIVO A FALSE
        estaVivo = false;
        // EXPLOSION
        explosion.Play();   


        // QUITAMOS EL NUMERO ENEMIGOS PARA QUE APAREZCA EL BOSS CUANDO
        // YA NO TENGAMOS ENEMIGOS
        if (numEnemigos > 0) {
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
    public void Recibirdanyo(string tipoEnemigo, int danyo) 
     {
        //print("vida antes " + vida);
         Debug.Log("Recibir daño "+ danyo);

        // Restamos la vida 
        if ( tipoEnemigo == "EnemigoListo")
        {
            vidaEnemigoListo = vidaEnemigoListo - danyo;
            if (vidaEnemigoListo <= 0)
            {
                vidaEnemigoListo = 0;
                Morir();
            }
        }
        else if (tipoEnemigo == "EnemigoTonto")
        {
            print("vida Estatico antes " + vidaEnemigoEstatico); //PROBARLO
            vidaEnemigoTonto = vidaEnemigoTonto- danyo;
            print("vida Estatico despues " + vidaEnemigoEstatico);
            if (vidaEnemigoTonto <= 0)
            {
                vidaEnemigoTonto = 0;
                Morir();
            }
        }
        else if (tipoEnemigo == "EnemigoEstatico")
        {
          
            vidaEnemigoEstatico = vidaEnemigoEstatico - danyo;

            print("Max " + maxVidaEEstatico + " vida "+vidaEnemigoEstatico);

            // ACTUALIZAMOS EL SLIDER
            barraVida.ActualizarBarraVida(vidaEnemigoEstatico, maxVidaEEstatico);

            // MANDAMOS LA POSICION DEL ARRAY QUE QUIERO REPRODUCIR Y EL VOLUMEN DEL SONIDO
            soundManager.SeleccionAudio(1, 0.5f);

            if (vidaEnemigoEstatico <= 0)
            {
                // METODO QUE ESTA EN EL SCRIPT DE ENEMIGO
                MuerteEnemigoEstatico();

                vidaEnemigoEstatico = 0;
                
               
            }
        }
        else if (tipoEnemigo == "EnemigoBoss")
        {
            vidaEnemigoBoss = vidaEnemigoBoss - danyo;
            if (vidaEnemigoBoss <= 0)
            {
                vidaEnemigoBoss = 0;
                Morir();
            }
        }


    }

    protected virtual void MuerteEnemigoEstatico()
    {
        print("2");
        // CAMBIO LA ANIMACION DE LA TORRETA PARA QUE SE VEA QUE SE HA MUERTO
        //animacionTorreTop.SetBool("Muerte", true);
        //Morir();
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
