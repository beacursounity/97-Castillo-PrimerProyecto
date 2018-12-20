using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIScript : MonoBehaviour {

    // EMPEZAR EL JUEGO
	public void StartGame () {
        // CARGAMOS LA ESCENA 1 QUE ES EL JUEGO
        SceneManager.LoadScene(1);
	}
	
    // FINALIZAR JUEGO
	public void ExitGame () {
        Application.Quit();
	}
}
