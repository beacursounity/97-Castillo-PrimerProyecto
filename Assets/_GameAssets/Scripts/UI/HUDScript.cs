using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDScript : MonoBehaviour {

    // PARA QUE GUARDE TODAS LAS IMAGENES
    public Image[] imagenes = new Image[3];

	public void ActivarArma (int posArma) {
        DesactivarArmas();
        imagenes[posArma].GetComponent<Image>().enabled = true;
    }

    public void DesactivarArmas() {
        // desactivamos el script del renderizado
        for (int i = 0; i < imagenes.Length; i++) {
            imagenes[i].GetComponent<Image>().enabled = false;
        }
    }
}
