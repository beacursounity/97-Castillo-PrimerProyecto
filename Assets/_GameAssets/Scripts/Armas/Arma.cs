using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arma : MonoBehaviour {

    bool activa;

    // HAY QUE RECOGER LA REFERENCIA DEL PUNTO DE GENERACION Y DE LA BALA
    [SerializeField] GameObject puntoGeneracion;
    [SerializeField] GameObject prefabBala;

    [SerializeField] int potenciaDisparo = 100; // tendre que modificarlo en el editor
                                                //[SerializeField] int tipoDanyoArma = 1;

    [SerializeField] AudioSource audioBalaTorreta;
    //[SerializeField] ParticleSystem particulasDisparo;

    [Header("ARMA CARGADOR FRONTAL/BOMBAS")]
    // NUEVAS VARIABLES PARA HACER OTRO TIPO DE DISPARO PARA LA BOMBA
    public int contadorBombas = 30;
    public float tiempoVida = 30f; // 30 segundos
    public int velocidadDisparoBomba = 100;
    // Quaternion se usan para representar las rotaciones
    Quaternion[] bombas;
    public float esparcir = 0;
    ParticleSystem particulasBombas;


    void Start()
    {
        // RECOGO SU AUDIO PARA PODER REPRODUCIRLO
        audioBalaTorreta  = GetComponent<AudioSource>();

        // RECOGO SUS PARTICULAS
        //particulasDisparo = GetComponent<ParticleSystem>();

        // CREAMOS UNA TABLA (contadorBombas) BOMBAS PARA IR GUARDANDO SUS ROTACIONES ALEATORIAS
        bombas = new Quaternion[contadorBombas];
        //particulasBombas = GetComponent<ParticleSystem>();

              
    }

    public void ApretarGatillo()
    {
        GameObject nuevaBala = Instantiate(prefabBala, puntoGeneracion.transform.position, puntoGeneracion.transform.rotation);

        // AUDIO DEL PROYECTIL
        audioBalaTorreta.Play();
        // PARA HACER LA FUERZA CON RESPECTO AL MUNDO 
        nuevaBala.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * potenciaDisparo);
 
        // PARTICULAS
        //particulasDisparo.Play();
    }

    // METODO PARA QUE SALGAN MAS BOMBAS EN UN SOLO DISPARO Y QUE SALGA CADA UNA CON
    // UNA ROTACION DISTINTA
    public void ApretarGatilloBomba()
    {

       // CON PARTICULAS QUE NO CONSUMIRA TANTA MEMORIA
       // particulasBombas.Play(); 
       // CON INSTANTIATES CONSUMIRA MAS MEMORIA
        for ( int i = 0; i < contadorBombas ; i++ ) 
        {
            bombas[i] = Random.rotation;
           
            GameObject nuevaBala = Instantiate(prefabBala, puntoGeneracion.transform.position, puntoGeneracion.transform.rotation);
            // DESTRUIMOS LAS BOMBAS AL CAMBO DE 30 SG
            Destroy (nuevaBala, tiempoVida);
            // A CADA BOMBA LE DAMOS UN GIRO DISTINTO 
            nuevaBala.transform.rotation = Quaternion.RotateTowards(nuevaBala.transform.rotation,
                    bombas[i], esparcir);
            nuevaBala.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * potenciaDisparo);
        
        }
        

    }
    
}
