using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using  UnityStandardAssets.Characters.FirstPerson;


public class Player : MonoBehaviour {

    [Header("ESTADO")]
    [SerializeField] int vidaActual = 15;
    [SerializeField] int vidaMaxima = 15;
    [SerializeField] public static bool estaVivo = true; // si no ponemos private sera private por defecto


    // PARA ALMACENAR LOS TIPOS DE ARMA CON UN TIPO ARMA(SCRIPT)
    [Header("TIPOS DE ARMA")]
    private const int NUM_ARMAS = 4; // LA HACEMOS CONSTANTE PQ NO LA VAMOS A MODIFICAR
    //[SerializeField] GameObject[] armas = new GameObject[NUM_ARMAS]; // seran o prefabs o GameObjects
    // SI PONEMOS EL NUMERO 4 DIRECTAMENTE ESTO SE LLAMA HARDCODE
    // VARIABLE PARA SABER QUE ARMA TENGO ACTIVA,
    [SerializeField] int armaActiva = 0;
    [SerializeField] Arma[] armas = new Arma[NUM_ARMAS];

    [Header("REFERENCIAS")]
    [SerializeField] GameObject enemigo;

    [Header("REFERENCIAS DEL TEXTO")]  // Lo voy a cambiar para ponerlo sobre la Escena
    [SerializeField] TextMesh tm;       // YA NO LAS USO
    [SerializeField] TextMesh numeroEnemigos;

    [Header("REFERENCIAS DEL TEXTO NUEVO")]  // Lo voy a cambiar para ponerlo sobre la Escena
    [SerializeField] Text tmNuevo;
    [SerializeField] Text numeroEnemigosNuevo;


    //[Header("SANGRE DEL PLAYER")]
    //[SerializeField] GameObject sangre;

    //bool sangreActiva = false;
    bool disparo = true;

    [Header("MUNICION")]
    [SerializeField] public int municionActual = 100;
    [SerializeField] int municionMaxima = 100;

    // cojo referencia script
    [SerializeField] HUDScript hs;

   FirstPersonController controller;

    //Incrementar la Vida del personaje
    public void Start() {

        // RECOJO EL QUAD SANGRE Y LO MUESTRO
        //GameObject sangre = GameObject.Find("Sangre");

        // HACEMOS LA LLAMADA A LA 0 PARA QUE ME ACTIVE EL ARMA 0
        ActivarArma(armaActiva);
        
        // PARA CONTROLAR LA SENSIBILIDAD DEL RATON A LA HORA DE DISPARAR
        //controller = GetComponent<FirstPersonController>();   
        //controller.CambiarSensibilidadRaton(1,1);     
        
      
    }


    public void Update() {

        if (estaVivo) {

            // ACTUALIZAMOS LAS VIDAS
            tmNuevo.text = "Vidas: " + vidaActual; // LE PONEMOS "" PARA QUE LO TRANFORME A TEXTO
            //numeroEnemigos.text = "Enemigos: " + Enemigo.numEnemigos;
            numeroEnemigosNuevo.text = "Enemigos: " + Enemigo.numEnemigos;

            // DEPENDIENDO QUE TECLA PULSE ACTIVAREMOS UN ARMA U OTRA
            Debug.Log("Player municion: " + municionActual);

            if (Input.GetMouseButtonDown(0) && municionActual > 0) { // boton izquierdo del raton
                Disparar();
            } else if (Input.GetKeyDown(KeyCode.Alpha1)) {
                armaActiva = 0;
                ActivarArma(armaActiva);
                hs.ActivarArma(0); // OTRA FORMA DE HACERLO
            } else if (Input.GetKeyDown(KeyCode.Alpha2)) {
                armaActiva = 1;
                ActivarArma(armaActiva);
                hs.ActivarArma(1);// OTRA FORMA DE HACERLO
            } else if (Input.GetKeyDown(KeyCode.Alpha3)) {
                armaActiva = 2;
                ActivarArma(armaActiva);
                hs.ActivarArma(2);// OTRA FORMA DE HACERLO
            } else if (Input.GetKeyDown(KeyCode.Alpha4)) {
                armaActiva = 3;
                ActivarArma(armaActiva);
            }
        }
    }

    // METODO PARA LLAMAR DESACTIVARARMAS Y LUEGO MEDIANTE PARAMETRO LE PASO EL
    // ARMA QUE QUIERO ACTIVAR SEGUN EL TECLADO
    private void ActivarArma(int armaActiva) {
        DesactivarArmas();
        armas[armaActiva].gameObject.SetActive(true);
    } 

    // DESACTIVAMOS TODAS LAS ARMAS
    private void DesactivarArmas() {
        foreach (var arma in armas) {
            arma.gameObject.SetActive(false);
        }
    }

    //Incrementar la Vida del personaje
    public void IncrementarVida(int incrementoVida) {
        Debug.Log("incremento vida " + incrementoVida);

        vidaActual = vidaActual + incrementoVida;
        Debug.Log("Vida Actual " + vidaActual);
        if (vidaActual > vidaMaxima) vidaActual = vidaMaxima;
    }


    //Incrementar Municion
    public void IncrementarMunicion(int incrementoMunicion) {
        //Debug.Log("incremento vida " + incrementoVida);

        municionActual = municionActual + incrementoMunicion;
        Debug.Log("Vida Actual " + municionActual);
        if (municionActual > municionMaxima) municionActual = municionMaxima;
    }

    // El enemigo reciben un daño
    public void Recibirdanyo(int danyo) {
        // Restamos la vida 
        vidaActual = vidaActual - danyo;

        // vida si es 0 se tiene que morir el jugador
        if (vidaActual <= 0) {
            vidaActual = 0;
            Morir();
        }
    }

    /*private void OnMouseDown() {
        Debug.Log("PULSADO CON EL RATON PLAYER");
        // es el daño que producimos al Enemigo
        Recibirdanyo(10);
    }*/

    // El Player 
    public void Morir() {

        Debug.Log("HE MUERTO");
        estaVivo = false;

        // RECOJO LA SANGRE Y LLAMO A LAS PARTICULAS DE SANGRE
        GameObject sangre = GameObject.Find("Sangre");
        //Debug.Log("Recojo Nombre sangre " + sangre.name);
        sangre.GetComponent<ParticleSystem>().Play();


        // ESPERO UN POCO PARA QUE SE VEA LA SANGRE Y SE DESTRUYA
        // EL PLAYER
        Invoke("Destruir", 3);

    }

    public void Destruir() {
        Debug.Log("DENTRO METODO DESTRUIR");

        Destroy(gameObject);
        // LLAMAMOS A LA ESCENA DE INICIO DEL JUEGO
        SceneManager.LoadScene(0);
    }

    //  
    public void Disparar() {

        // SI EL ARMA ES LA FLECHA NO DEJARE QUE DISPARE SIEMPRE
        if (armaActiva == 0) {
            if (disparo) {
                // VAMOS A COGER EL ARMA ACTIVA Y APRETAMOS EL GATILLO
                armas[armaActiva].ApretarGatillo();
                //  QUITAMOS UNA BALA 
                municionActual = municionActual - 1;
                disparo = false;
            } else {
                disparo = true;
            }
        }
        else if (armaActiva == 2){ // bomba nueva forma de disparar.
            // VAMOS A COGER EL ARMA ACTIVA Y APRETAMOS EL GATILLO
            armas[armaActiva].ApretarGatilloBomba();
            // POR SI ME CAMBIA AL ARCO INICIALIZO LA VARIABLE
            disparo = false;
            //  QUITAMOS UNA BALA 
            municionActual = municionActual - 1;
        } 
        else {
            // VAMOS A COGER EL ARMA ACTIVA Y APRETAMOS EL GATILLO
            armas[armaActiva].ApretarGatillo();
            // POR SI ME CAMBIA AL ARCO INICIALIZO LA VARIABLE
            disparo = false;
            //  QUITAMOS UNA BALA 
            municionActual = municionActual - 1;
        }
    }

}