using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneradorEnemigoBoss : MonoBehaviour {

    // RECOGEMOS LA REFERENCIA DEL PREFAB
    [SerializeField] GameObject prefabEnemigoBoss;

    // PONEMOS UNAS PARTICULAS PARA CUANDO APAREZCA EL BOSS
    [SerializeField] ParticleSystem particulas;
    //[SerializeField]  particulas;

    // RECOGER PUNTOGENBOSS1
    Transform punto1;
    // RECOGER PUNTOGENBOSS2
    Transform punto2;
    // RECOGER PUNTOGENBOSS2
    Transform punto3;

    private void Awake() {
        // RECOGER LOS PUNTOS PARA PODER GENERAR AL BOSS EN PUNTOS DISTINTOS CADA
        // VEZ QUE EJECUTEMOS EL JUEGO 
        // RECOGER PUNTOGENBOSS1
        punto1 = GameObject.Find("PuntoGenBoss1").transform;
        // RECOGER PUNTOGENBOSS2
        punto2 = GameObject.Find("PuntoGenBoss2").transform;
        // RECOGER PUNTOGENBOSS3
        punto3 = GameObject.Find("PuntoGenBoss3").transform;
    }


    // Update is called once per frame
    void Update () {

        // AL SER UNA VARIABLE ESTATICA Y PUBLICA DENTRO DE SU ENTORNO
        // TENDRE QUE HACERLE LA REFERENCIA A ENEMIGO.NUMENEMIGOS
        if (Enemigo.numEnemigos == 0) {
            //Debug.Log("YA NO TENGO ENEMIGOS E INSTANCIO");

            // RECOGER X DESDE EL PUNTO1 AL PUNTO2
            float x = Random.Range(punto1.position.x, punto3.position.x);
            Debug.Log("x: "+ x);
            // RECOGER Z DESDE PUNTO2 AL PUNTO3
            float z = Random.Range(punto2.position.z, punto3.position.z);
            Debug.Log("z: " + z);
            // VECTOR RESULTANTE DE LA POSICION QUE ELIJA
            Vector3 posicionBoss = new Vector3(x, transform.position.y, z);

            // ANTES DE QUE APAREZCA EL BOSS MOSTRAMOS UNAS PARTICULAS 
            particulas = Instantiate(particulas, posicionBoss, Quaternion.identity);
            particulas.Play();

            GameObject newEnemigoBoss = Instantiate(prefabEnemigoBoss, posicionBoss, Quaternion.identity);

            // PARA QUE NO ME VUELVA A GENERAR MAS JEFES
            Enemigo.numEnemigos = -1;
        }

    }



}
