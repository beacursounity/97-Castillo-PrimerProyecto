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

    void Start()
    {
        // RECOGO SU AUDIO PARA PODER REPRODUCIRLO
        audioBalaTorreta  = GetComponent<AudioSource>();

        // RECOGO SUS PARTICULAS
        //particulasDisparo = GetComponent<ParticleSystem>();

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


    
    // Update is called once per frame
    void Update () {
		
	}
}
