using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Desenha uma linha por onde o raio irá
// somente durante o desenvolvimento para testes
public class RayViewer : MonoBehaviour {

	public float weaponRange = 50f;

	private Camera fpsCam;

	// Use this for initialization
	void Start () {
		// obtém a câmera do objeto parente ao qual
		// esse script estiver associado
		fpsCam = GetComponentInParent<Camera> ();
	}
	
	// Update is called once per frame
	void Update () {
		// calcular o ponto de origem da linha
		// e desenhá-la usando o Debug.DrawRay
		// Obtém o ponto a partir do centro da câmera
		Vector3 lineOrigin = fpsCam.ViewportToWorldPoint(new Vector3 (0.5f, 0.5f, 0));
		Vector3 direcaoDoRaio = fpsCam.transform.forward * weaponRange;
		Debug.DrawRay (lineOrigin, direcaoDoRaio, Color.green);
	}
}
