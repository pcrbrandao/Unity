using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	public GameObject[] hazards;
	public Vector3 spawnValues;
	public int hazardCount;
	public float spawnWait;
	public float startWait;
	public float waitWave;

	public Text scoreText;
	public Text restartText;
	public Text gameOverText;

	private int score;
	private bool gameOver;
	private bool restart;

	void Start() {
		gameOver = false;
		restart = false;
		restartText.text = "";
		gameOverText.text = "";

		StartCoroutine(SpawnWaves ());
		score = 0;
		UpdateScore ();
	}

	void Update() {
		if (restart) {
			if (Input.GetKey (KeyCode.R)) {
				SceneManager.LoadScene ("Main");
			}
		}
	}

	public void GameOver() {
		gameOverText.text = "Game Over";
		gameOver = true;
	}

	public void AddScore(int newScoreValue) {
		score += newScoreValue;
		UpdateScore ();
	}

	IEnumerator SpawnWaves() {
		
		yield return new WaitForSeconds (startWait);

		while (true) {
			for (int i = 0; i < hazardCount; i++) {

				GameObject hazard = hazards[Random.Range(0, hazards.Length)];

				Vector3 spawnPosition = new Vector3 (Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
				Quaternion spawnRotation = Quaternion.identity;
				Instantiate (hazard, spawnPosition, spawnRotation);

				yield return new WaitForSeconds (spawnWait);
			}
			yield return new WaitForSeconds (waitWave);

			if (gameOver) {
				restartText.text = "Pressione 'R' para reiniciar";
				restart = true;
				break;
			}
		}
	}

	void UpdateScore() {
		scoreText.text = "Score: " + score;
	}
}
