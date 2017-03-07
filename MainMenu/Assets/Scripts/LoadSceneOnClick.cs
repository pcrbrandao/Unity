using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // precisa incluir isso aqui


// Apenas carrega a cena do index que está na build settings

public class LoadSceneOnClick : MonoBehaviour {

	public void LoadByIndex(int sceneIndex) {
		
		SceneManager.LoadScene (sceneIndex);
	}
}
