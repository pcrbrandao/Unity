using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	public float speed = 10;
	public Text countText;
	public Text venceuText;

	private Rigidbody rb;
	private int count;

	// Use this for initialization
	void Start () {
		// obtem o rigidbody do objeto associao a esse script
		rb = GetComponent<Rigidbody> ();
		count = 0;
		SetCountText ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// Called before physics update
	void FixedUpdate() {
		// Ler os comandos do teclado (setas) e
		// aplicar uma força na direção desejada
		float moveHorizontal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		// criar o vetor que representa a forca
		// z é o movimento na vertical, y é para cima e para baixo
		Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

		// Obter o objeto ao qual a força será aplicada
		rb.AddForce(movement * speed);
	}

	// se for Premio desativa
	void OnTriggerEnter(Collider other) {

		if (other.gameObject.CompareTag("Premio")) {
			other.gameObject.SetActive (false);
			count++;
			SetCountText ();
			if (count == 12) {
				venceuText.gameObject.SetActive (true);
			}
		}
	}

	void SetCountText() {
		countText.text = "Pontuação: " + count.ToString();
	}
}
