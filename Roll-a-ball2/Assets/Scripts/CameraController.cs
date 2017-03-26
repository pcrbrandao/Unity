using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// A camera deve acompanhar o player
// Como filha do player ela acompanharia
// o movimento, mas também a rotação
// o que não seria o esperado
// O objetivo é que ela acompanhe o player
// nos eixos x, z
public class CameraController : MonoBehaviour {

	public GameObject player;

	private Vector3 offset; // é a distância da câmera para o player

	// Use this for initialization
	void Start () {
		// transform.position é a posição do objeto ao qual o
		// script está associado
		offset = transform.position - player.transform.position;
	}
	
	// Update is called once per frame, após processar todos os objetos
	void LateUpdate () {
		// a posição da câmera deve ser atualizada
		// em cada frame
		transform.position = player.transform.position + offset;
	}
}
