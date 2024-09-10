using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarraVida : MonoBehaviour
{

   Slider slider;

   Camera camara;

    [SerializeField] GameObject player;

    private void Awake()
    {
        // RECOJO LA CAMARA DEL PLAYER/FIRSTPERSONCONTOLLER
        camara = player.transform.GetChild(0).GetComponent<Camera>();
        // SLIDER QUE TIENE LA TORRETA
        slider = GetComponent<Slider>();
    
    }

    public void ActualizarBarraVida(float valorActual, float valorMaximo)
    {
        slider.value = valorActual / valorMaximo;   
    }

    // Update is called once per frame
    void Update()
    {
        // QUE ROTE JUNTO A LA CAMARA DEL FIRSTPERSONCONTROLLER
        transform.rotation = camara.transform.rotation;
    }
}
