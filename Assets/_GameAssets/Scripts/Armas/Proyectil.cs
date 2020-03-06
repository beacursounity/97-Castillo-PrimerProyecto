using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proyectil : MonoBehaviour {

    [SerializeField] int danyoproyectil;
    [SerializeField] ParticleSystem particulasEnemigoEstatico;
    [SerializeField] ParticleSystem particulasRestoEnemigos;

  

    // El DAÑO DEPENDERA DE TIPO DE ARMA 5 MENOS DAÑO Y TENDRA MAS VIDAS Y
    // SI VAMOS BAJANDO HASTA 1 SERAN MAS DAÑO Y SE MORIRAN PORQUE TENDRA MENOS VIDAS
    // FLECHA = 1
    // ESPADA = 2
    // CARGADOR FONTAL(BOMBA) = 4
    // ARMA CORTA = 3

    private void Start() {
        
        // VAMOS DESTRUIR UN PROYECTIL CADA 5 SEGUNDOS
        Invoke("Destruir", 5);
     
    }

    // DA UN COLLIDER POR COLISION
    private void OnTriggerEnter(Collider other) {
        // RECOJO EL GAMEOBJECT QUE ES CON EL QUE COLISIONA LA BALA
        GameObject objetivoImpacto = other.gameObject;
        Debug.Log(objetivoImpacto.tag);
        string f ="1";
    
        // LOS BUSCAMOS POR SU TAG PARA SABER CONTRA QUIEN GOLPEO
        if (objetivoImpacto.tag == "Enemigo") {
            Debug.Log("HA COLISIONADO CON EL ENEMIGO");
            //Debug.Log("DAÑO PROYECTIL "+danyoproyectil);
            // INSTANCIAMOS EL PREFAB
            particulasRestoEnemigos = Instantiate(particulasRestoEnemigos, transform.position, Quaternion.identity);

            //  PARA QUE APAREZCAN LAS PARTICULAS
            particulasRestoEnemigos.Play();

            // RECOGEMOS SU COMPONENTE PARA PODER QUITAR LA VIDA DEL ENEMIGO
            objetivoImpacto.GetComponent<Enemigo>().Recibirdanyo(danyoproyectil);

        } else if (objetivoImpacto.tag == "EnemigoBoss") {
            Debug.Log("HA COLISIONADO CON EL ENEMIGO BOSS");

            // INSTANCIAMOS EL PREFAB
            particulasRestoEnemigos = Instantiate(particulasRestoEnemigos, transform.position, Quaternion.identity);

            //  PARA QUE APAREZCAN LAS PARTICULAS
            particulasRestoEnemigos.Play();

            // RECOGEMOS SU COMPONENTE PARA PODER QUITAR LA VIDA DEL ENEMIGO
            objetivoImpacto.GetComponent<EnemigoBoss>().Recibirdanyo(danyoproyectil);
    
        } else if (objetivoImpacto.tag == "Player") {
            Debug.Log("MATADO PLAYER");
            // RECOGEMOS SU COMPONENTE PARA PODER QUITAR LA VIDA DEL PLAYER 
            // PORQUE HA SIDO EL ENEMIGO ESTATICO QUE NOS HA TIRADO UN BOMBA
            objetivoImpacto.GetComponent<Player>().Recibirdanyo(danyoproyectil);

        } else if (objetivoImpacto.tag == "EnemigoEstatico") {
            Debug.Log("COLISIONA CON ENEMIGO ESTATICO Y SALEN LAS PARTICULAS");
            // INSTANCIAMOS EL PREFAB
            particulasEnemigoEstatico = Instantiate(particulasEnemigoEstatico, transform.position, Quaternion.identity);

            // PARA QUE APAREZCAN LAS PARTICULAS
            particulasEnemigoEstatico.Play();

            Invoke("DestruirExplosion", 2);
        }

        // LO DESTRUIMOS ESPERANDO UN POCO A QUE EL SISTEMA DE PARTICULAS APAREZCA,
        // QUE SUENE EL PLAY....
        Invoke("Destruir",4);
    }

    
    private void Destruir() {   
        Destroy(this.gameObject);
    }

}
