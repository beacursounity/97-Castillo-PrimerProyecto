using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarraVida : MonoBehaviour
{

    [SerializeField] Slider slider;

    [SerializeField] Camera camara;

    public void ActualizarBarraVida(float valorActual, float valorMaximo)
    {
        slider.value = valorActual / valorMaximo;   
    }

    // Update is called once per frame
    void Update()
    {
        // QUE ROTE JUNTO A LA CAMARA
        transform.rotation = camara.transform.rotation;
    }
}
