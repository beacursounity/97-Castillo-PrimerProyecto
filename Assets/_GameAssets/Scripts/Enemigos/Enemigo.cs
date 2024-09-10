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
    public float vidaEnemigoTonto;
    public float vidaEnemigoListo;
    public float vidaEnemigoEstatico;
    public float vidaEnemigoBoss;
    //[SerializeField] bool enPosicion = false;

    [SerializeField] protected int velocidad = 10; // DEL MOVIMIENTO DEL ENEMIGO MOVIL

    [Header("ATAQUE")]
    [SerializeField] protected int distanciaDeteccion; // PARA EL ENEMIGO QUE SE MUEVE
    [SerializeField] protected int danyoAlPlayer = 2;  // que realiza a nuestro Player
    [SerializeField] protected int danyoAlEnemigo = 2; // que realiza el Player a nuestro al Enemigo 


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
    public static int numEnemigos = 8;

    //int speed;
    //int distanciaExplosion; // 
    // LE HACEMOS DAÑO AL ENEMIGO Y NO SE REGENERA

    [Header("SONIDOS ENEMIGOS")]
    SoundManager soundManager;

    [Header("ANIMACIONES MUERTE ENEMIGOS")]
    public AnimatorOverrideController amimacionTorreMuerte;
    public AnimatorOverrideController amimacionEnemigo;


    [Header("SLIDER VIDAS ENEMIGOS")]
    private BarraVida barraVida;

    [Header("MUERTE ENEMIGOS")]
    protected bool muerteEnemigoEstatico;


    Animator animacionEnemigo;

    private void Awake() {
        // LO COGEMOS ASI PORQUE EL PLAYER ES UN PREFAB Y ASI COGERA MEJOR
        // LA REFERENCIA.
        player = GameObject.Find("Player");

        // BUSCAMOS NUESTRO GO QUE CONTIENE TODOS LOS SONIDOS
        soundManager = FindObjectOfType<SoundManager>();

        // RECOGEMOS EL COMPONENTE DE LA BARRAVIDA.CS PARA PODER LLAMAR A SUS METODOS
        barraVida = GetComponentInChildren<BarraVida>();


        // RECOJO EL ANIMATOR QUE EN ESTE CASO NO TIENE NINGUN ANIMACION
        // PERO LUEGO LO NECESITAMOS PARA HACER ANIMACION QUE SE DESTRUYA
        animacionEnemigo = GetComponent<Animator>();


    }

  
    /*protected virtual void Update()
    {
       int vida = 100;
    }*/

    // PARA RECOGER LA DISTACIA ENTRE EL PLAYER Y LA POSICION DEL TRANSFORM DEPENDIENDO
    // DESDE DONDE SE LLAME
    protected Vector3 GetDistancia() {
        return player.transform.position - transform.position;
    }

    // TODOS LOS ENEMIGOS RECIBEN UN DAÑO
    // EL DAÑO LO DA EL ARMA QUE TENDREMOS QUE DECIDIRLO NOSOTROS
    public void Recibirdanyo(string tipoEnemigo, int danyo)
    {
     
        Debug.Log("Recibir daño " + danyo);

        // RESTAMOS VIDA, ACTUALIZAMOS BARRA DE VIDA, REPRODUCIMOS SONIDO
        if (tipoEnemigo == "EnemigoListo")
        {
            vidaEnemigoListo = vidaEnemigoListo - danyo;

            // ACTUALIZAMOS EL SLIDER
            barraVida.ActualizarBarraVida(vidaEnemigoListo, maxVidaEListo);

            // MANDAMOS LA POSICION DEL ARRAY QUE QUIERO REPRODUCIR Y EL VOLUMEN DEL SONIDO
            soundManager.SeleccionAudio(2, 0.1f);

            if (vidaEnemigoListo <= 0)
            {
                vidaEnemigoListo = 0;
                Morir();
            }
        }
        else if (tipoEnemigo == "EnemigoTonto") // OK
        {
           
            vidaEnemigoTonto = vidaEnemigoTonto - danyo;

            // ACTUALIZAMOS EL SLIDER
            barraVida.ActualizarBarraVida(vidaEnemigoTonto, maxVidaETonto);

            // MANDAMOS LA POSICION DEL ARRAY QUE QUIERO REPRODUCIR Y EL VOLUMEN DEL SONIDO
            soundManager.SeleccionAudio(2, 0.1f);

            if (vidaEnemigoTonto <= 0)
            {
                vidaEnemigoTonto = 0;       
                Morir();
            }
        }
        else if (tipoEnemigo == "EnemigoEstatico") //OK
        {

            vidaEnemigoEstatico = vidaEnemigoEstatico - danyo;

            // ACTUALIZAMOS EL SLIDER
            barraVida.ActualizarBarraVida(vidaEnemigoEstatico, maxVidaEEstatico);

            // MANDAMOS LA POSICION DEL ARRAY QUE QUIERO REPRODUCIR Y EL VOLUMEN DEL SONIDO
            soundManager.SeleccionAudio(1, 0.1f);

            if (vidaEnemigoEstatico <= 0)
            {
                // METODO QUE ESTA EN EL SCRIPT DE ENEMIGO
                MuerteEnemigoEstatico();

                vidaEnemigoEstatico = 0;

                // QUITAMOS EN NUMERO DE ENEMIGOS DEL CANVAS 
                numEnemigos = numEnemigos - 1;


            }
        }
        else if (tipoEnemigo == "EnemigoBoss")
        {
            vidaEnemigoBoss = vidaEnemigoBoss - danyo;

            // ACTUALIZAMOS EL SLIDER
            barraVida.ActualizarBarraVida(vidaEnemigoBoss, maxVidaEBoss);

            // MANDAMOS LA POSICION DEL ARRAY QUE QUIERO REPRODUCIR Y EL VOLUMEN DEL SONIDO
            soundManager.SeleccionAudio(2, 0.1f);

            if (vidaEnemigoBoss <= 0)
            {
                vidaEnemigoBoss = 0;
                Morir();
            }
        }


    }

    // PARA TODOS LOS ENEMIGOS MUEREN EXCEPTO PARA EL ENEMIGO ESTATICO
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

        // ACTIVO LA ANIMACION DE MUERTE ENEMIGO
        animacionEnemigo.runtimeAnimatorController = amimacionEnemigo;
        print("animacion " + animacionEnemigo);

        // QUITAMOS LA BARRA DE VIDA
        Canvas barraVida = this.GetComponentInChildren<Canvas>();

        // QUITAMOS EL CANVAS PARA QUE NO SE VEA LA BARRA DE VIDA
        barraVida.enabled = false;

        // QUITAMOS EL NUMERO ENEMIGOS PARA QUE APAREZCA EL BOSS CUANDO
        // YA NO TENGAMOS ENEMIGOS
        if (numEnemigos > 0) {
            numEnemigos = numEnemigos - 1;
        }

        // QUITADO PARA PROBAR LA ANIMACION
        //Destroy(this.gameObject);

        // CUANDO NOS CARGUEMOS AL BOSS SE TERMINARA TB EL JUEGO
        if (vidaEnemigoBoss == 0) {
            Debug.Log("ENEMIGO.MORIR: VAMOS A CARGARNOS A NUESTRO BOSS");
            // LLAMAMOS A LA ESCENA DE INICIO DEL JUEGO
            SceneManager.LoadScene(0);
        }

    }


    // HE CREADO ESTE METODO PARA PODER CAMBIAR LA ANIMACION DE LA TORRETA
    // EN EL HIJO ENEMIGOESTATICO.CS YA QUE LO LLAMO AL METODO DESDE AQUI
    protected virtual void MuerteEnemigoEstatico()
    {
        // VACIO
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
