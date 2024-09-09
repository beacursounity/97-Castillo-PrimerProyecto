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
    bool esconderArma = true;

    [Header("REFERENCIAS")]
    //[SerializeField] GameObject enemigo;

    [Header("REFERENCIAS DEL TEXTO")]
    [SerializeField] Text vidas;
    [SerializeField] Text numeroEnemigos;

    [Header("SANGRE")]  
    [SerializeField] Image sangre;
    float r,g, b, a, colorAlphaInicial;
 

    //[Header("SANGRE DEL PLAYER")]
    //[SerializeField] GameObject sangre;

    //bool sangreActiva = false;

    [Header("MUNICION")]
    public int municionActual = 100;
    [SerializeField] int municionMaxima = 100;
    bool disparo = true; // PARA SABER SI DISPARO O NO

    // REFERENCIA DEL SCRIPT    
    [SerializeField] HUDScript hs;

    [Header("SONIDO GRITO")]
    SoundManager soundManager;

    private void Awake()
    {
        // BUSCAMOS NUESTRO GO QUE CONTIENE TODOS LOS SONIDOS
        soundManager = FindObjectOfType<SoundManager>();    
    }

    //Incrementar la Vida del personaje
    public void Start() {
      
        //RECOGER LOS COLORES DEL CANVAS SANGRE
        r = sangre.color.r;
        g = sangre.color.g;
        b = sangre.color.b;
        a = sangre.color.a;
        colorAlphaInicial = sangre.color.a;


        // HACEMOS LA LLAMADA A LA 0 PARA QUE ME ACTIVE EL ARMA 0
        ActivarArma(armaActiva);

        // ESCONDEMOS LAS ARMAS
        armas[armaActiva].gameObject.SetActive(false);

        // PARA CONTROLAR LA SENSIBILIDAD DEL RATON A LA HORA DE DISPARAR
        //controller = GetComponent<FirstPersonController>();   
        //controller.CambiarSensibilidadRaton(1,1);     


    }


    public void Update() {

        if (estaVivo) {

            // ACTUALIZAMOS LAS VIDAS
            vidas.text = "Vidas: " + vidaActual; // LE PONEMOS "" PARA QUE LO TRANFORME A TEXTO
            // ACTUALIZAMOS LOS ENEMIGOS
            numeroEnemigos.text = "Enemigos: " + Enemigo.numEnemigos;

            // DEPENDIENDO QUE TECLA PULSE ACTIVAREMOS UN ARMA U OTRA
            //Debug.Log("Player municion: " + municionActual);

            // SI DOY A LA "A" MUESTRO O ESCONDO EL ARMA
            if (Input.GetKeyDown(KeyCode.Q)) 
            {
                esconderArma = !esconderArma;

                if (esconderArma) DesactivarArmas();
                else
                {
                    ActivarArma(armaActiva);
                }
             
            }

           if (!esconderArma) {
                if (Input.GetMouseButtonDown(0) && municionActual > 0)
                { // boton izquierdo del raton
                    Disparar();
                }
                else if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    armaActiva = 0;
                    ActivarArma(armaActiva);
                    hs.ActivarArma(0); // OTRA FORMA DE HACERLO
                }
                else if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    armaActiva = 1;
                    ActivarArma(armaActiva);
                    hs.ActivarArma(1);// OTRA FORMA DE HACERLO
                }
                else if (Input.GetKeyDown(KeyCode.Alpha3))
                {
                    armaActiva = 2;
                    ActivarArma(armaActiva);
                    hs.ActivarArma(2);// OTRA FORMA DE HACERLO
                }
                else if (Input.GetKeyDown(KeyCode.Alpha4))
                {
                    armaActiva = 3;
                    ActivarArma(armaActiva);
                }
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

        // MANDAMOS LA POSICION DEL ARRAY QUE QUIERO REPRODUCIR Y EL VOLUMEN DEL SONIDO
        soundManager.SeleccionAudio(0, 2f);

        // MUESTRA LA SANGRE DEL PLAYER
        StartCoroutine("MostrarSangre");

        
        // vida si es 0 se tiene que morir el jugador
        if (vidaActual <= 0) {
            vidaActual = 0;
            Morir();
        }
     
    }

    // CUANDO EL ENEMIGO O LAS BALAS DE LA TORRETA LE TOQUE MOSTRAMOS LA SANGRE
    IEnumerator MostrarSangre()
    {
        // MOSTRAR LA SANGRE POCO A POCO
        for (int i = 1; i < 60; i++)
        {
            
            a += 0.01f;
            // CAMBIAR COLOR
            CambiarColorSangre();

            yield return new WaitForSeconds(0.01f);
        }

        // VOLVEMOS A DEJAR TRANSPARENTE LA SANGRE POCO A POCO
        for (int i = 60; i > 1; i--)
        {

            a -= 0.01f;
            // CAMBIAR COLOR
            CambiarColorSangre();

            yield return new WaitForSeconds(0.01f);
        }


        // LO PONEMOS MENOR QUE LO ANTERIOR PARA QUE DESAPAREZCA LA IMAGEN DE LA SANGRE
        //a -= 0.001f;
        // TIENE QUE ESTAR ENTRE LOS VALORES DADOS 0 Y 1.
        // SI ES MENOR QUE 0 DARA 0 Y SI ES MAYOR QUE 1 DA 1
        //a = Mathf.Clamp(a, 0, 1f);
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

        // ESPERO UN POCO PARA QUE SE VEA LA SANGRE Y SE DESTRUYA
        // EL PLAYER
        Invoke("Destruir", 2);

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

    void CambiarColorSangre()
    {
        Color c = new Color(r, g, b, a);
        sangre.color = c;

    }

}