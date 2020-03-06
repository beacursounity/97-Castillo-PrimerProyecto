using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneradorEnemigos : MonoBehaviour
{

    // RECOGEMOS LA REFERENCIA DEL PREFAB
    [SerializeField] GameObject prefabEnemigo;

    [Header("COSAS DE GENERACION")]
    [SerializeField] float ratioGeneracionEnemigo = 5f;

    private int numEnemigos = 0; // VARIABLE CONTADOR DE ENEMIGOS 
    [SerializeField] int numEnemigosMaximo; // SE PODRIA HACER CONSTANTE

    // Use this for initialization
    void Start()
    {
       // Debug.Log("NUM.MAX.ENEMIGOS "+numEnemigosMaximo);
        InvokeRepeating("GenerateEnemigo", 2, ratioGeneracionEnemigo);
    }

    void GenerateEnemigo()
    {
        // INSTANCIAR EL ENEMIGO TONTO EN UN GO VACIO
        ///GameObject newEnemigo = Instantiate(prefabEnemigo, transform);
        GameObject newEnemigo = Instantiate(prefabEnemigo, transform.position, Quaternion.identity);
        //.position, Quaternion.identity);
        numEnemigos++;
        if (numEnemigos == numEnemigosMaximo)
        {
            CancelInvoke();
        }

    }
}
