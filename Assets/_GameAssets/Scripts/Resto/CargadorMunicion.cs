using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CargadorMunicion : MonoBehaviour {

    [SerializeField] int speedRotacion = 1;
    [SerializeField] int municion = 1;


    //int rotacion = 0;

    // MOVIMIENTO CAJAVIDA
    // Use this for initialization
    void Start () {
       
    }

    // Update is called once per frame
    void Update() 
    {
        // ESTA ES OTRA MANERA DE HACERLO MAS EFICIENTE SIN USAR VARIABLES
        // YA QUE DEPENDIENDO DE QUE TIPO DE VARIABLES TENGAMOS OCUPARAN MAS O MENOS
        // COMO INT, SHORT, LONG Y PUEDE HABER PROBLEMAS
        transform.Rotate(new Vector3(0, 1, 0)); // Rota a partir donde este rotara sobre el eje
                                                // de las Y
              
    }


    private void OnCollisionEnter(Collision collision) {
          
        GameObject colisionador = collision.gameObject;
   
        if (colisionador.name == "Player") {
            Debug.Log("INCREMENTAMOS LA MUNICION");

            Player player = colisionador.GetComponent<Player>();
            // SI LA MUNICION ACTUAL ES < 100 ME DEJA COGER LA MUNICION
            Debug.Log("Municion " + player.municionActual);

            if (player.municionActual < 100) { 
                player.IncrementarMunicion(municion);

                // AUDIO
                this.GetComponent<AudioSource>().Play();

                // PARTICULAS
                this.GetComponent<ParticleSystem>().Play();

                Invoke("Destruir", 2);
            }

        }
    }

    private void Destruir()
    {
       Destroy(this.gameObject);
            
    }

}
