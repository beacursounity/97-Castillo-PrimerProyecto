using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortadaScript: MonoBehaviour {

    //[SerializeField] RectTransform[] rt;  // para recoger transform de nube....
    //[SerializeField] float speed = 30f;

	// ENTRAMOS EN LA PRIMERA ESCENA
	public void StartGame () {
        SceneManager.LoadScene(1);
	}

    // SALIMOS DEL JUEGO
    public void ExitGame () {
        Application.Quit();
	}

    public void DeleteGame () {
        print("ha borrado los datos del Playerprefs");
       PlayerPrefs.DeleteAll();
	}
    

}