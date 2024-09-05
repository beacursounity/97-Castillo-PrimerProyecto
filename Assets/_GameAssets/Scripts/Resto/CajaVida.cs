using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CajaVida : MonoBehaviour {

    [SerializeField] int speedRotacion = 1;
    [SerializeField] int vida = 1;
    int rotacion = 0;

    [Header("MOVIMIENTO CAJAVIDA")]
    int deltaY = 0;
    bool subiendo = true;

   /* [Header("TAMAÑO CAJAVIDA")]
    int tamanyo = 0;
    bool cambiatamanyo = true;
    */
    //[Header("EXPLOSION CAJAVIDA")]
    //[SerializeField] ParticleSystem explosionSalud;

    //int speed;
    //int distanciaExplosion; // 

    // MOVIMIENTO CAJAVIDA
    // Use this for initialization
    void Start () {
        subiendo = true;
        deltaY = 0;
    }

    // Update is called once per frame
    void Update()
    {
        /*rotacion = rotacion + speedRotacion;
        // VAMOS HACER QUE LA ROTACION TENGA UN TOPE
        if (rotacion >= 360) rotacion = 0;

        transform.eulerAngles = new Vector3(0, rotacion, 0);
        */

        // ESTO TB FUNCIONA
        //transform.rotation = Quaternion.Euler(new Vector3(0, rotacion, 0));
        //rotacion = rotacion + 1;

        // ESTA ES OTRA MANERA DE HACERLO MAS EFICIENTE SIN USAR VARIABLES
        // YA QUE DEPENDIENDO DE QUE TIPO DE VARIABLES TENGAMOS OCUPARAN MAS O MENOS
        // COMO INT, SHORT, LONG Y PUEDE HABER PROBLEMAS
        transform.Rotate(new Vector3(0, 1, 0)); // Rota a partir donde este rotara sobre el eje
                                                // de las Y
       // if (gameObject.name == "CajaVida4") { 
        // PARA QUE SUBA Y BAJE
        if (subiendo)
        {
            deltaY++;
            transform.Translate(Vector3.up * Time.deltaTime);
        }
        else
        {
            deltaY--;
            transform.Translate(Vector3.up * Time.deltaTime * -1);
        }

        if (deltaY >= 40)
        { 
            subiendo = false;
        }
        else if (deltaY <= 0)
        {
            subiendo = true;
        }

    }


    private void OnCollisionEnter(Collision collision) {
          
        GameObject colisionador = collision.gameObject;
       // Debug.Log(colisionador.name);

        if (colisionador.name == "Player")
        {
            Debug.Log("HA ENTRADO PARA INCREMENTAR VIDA PLAYER");
            //Debug.Log("HA COLISIONADO CON PLAYER");
            // RECOJO EL COMPONENTE PLAYER
            Player player = colisionador.GetComponent<Player>();
            // SUMO 1 A INCREMENTAR VIDA
            player.IncrementarVida(vida);
     
            // AUDIO
            this.GetComponent<AudioSource>().Play();
            // PARTICULAS
            this.GetComponent<ParticleSystem>().Play();
            Invoke("Destruir", 2);

        }
    }

    private void Destruir() {
        Destroy(this.gameObject);
    }
    

}
