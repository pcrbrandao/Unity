using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastShoot : MonoBehaviour {

	public int gunDamage = 1;
	public float fireRate = 0.25f;
	public float weaponRange = 50f;
	public float hitForce = 100f;
	public Transform gunEnd;

	private Camera fpsCam;
	private WaitForSeconds shotDuration = new WaitForSeconds (0.07f);
	private AudioSource gunAudio;
	private LineRenderer laserLine;
	private float nextFire;

	// Use this for initialization
	void Start () {
		laserLine = GetComponent<LineRenderer> ();
		gunAudio = GetComponent<AudioSource> ();
		fpsCam = GetComponentInParent<Camera> ();
	}
	
	// Update is called once per frame
	void Update () {

		// Se o botão foi pressionado e já se passou o tempo
		// mínimo para o próximo tiro, nextFire
		// o tiro deve ser disparado
		if (Input.GetButtonDown ("Fire1") && Time.time > nextFire) {
			nextFire = Time.time + fireRate; // atualiza nextFire com o time atual e fireRate
			StartCoroutine(ShotEffect()); // faz o disparo
			// é necessário obter os pontos inicial e final para o tiro
			// o alvo é sempre o centro da câmera. A linha abaixo obtém um ponto relativo
			// à câmera e converte-o para o world space. 0.5, 0.5 é o centro da viewport
			Vector3 rayOrigin = fpsCam.ViewportToWorldPoint (new Vector3 (0.5f, 0.5f, 0));
			// se um objeto for atingido, obter a informação dele
			RaycastHit hit;
			// determinar o início e o fim da laserLine
			laserLine.SetPosition(0, gunEnd.position);

			// Physics.Raycast retorna verdadeiro se o raio criado por ele
			// for atingido por algo.
			// o parametro hit será atualizado com o objeto atingido
			Vector3 direcaoDoTiro = fpsCam.transform.forward;
			bool atingiuAlgo = Physics.Raycast(rayOrigin, direcaoDoTiro, out hit, weaponRange);
			// há duas possibilidades para o ponto final do laser:
			// um ponto em um objeto, ou nada. Se atingir alguma coisa
			if (atingiuAlgo) {
				// atribuir o ponto ao segundo ponto de laserLine
				laserLine.SetPosition (1, hit.point);

				// verificar se há uma shootablebox
				// se houver, chama a função de causar dano
				ShootableBox health = hit.collider.GetComponent<ShootableBox>();
				if (health != null) {
					health.Damage (gunDamage);
				}
				// verifica se há um rigidBody. Se houver aplica uma
				// força, simulando o impacto do tiro
				if (hit.rigidbody != null) {
					hit.rigidbody.AddForce (-hit.normal * hitForce);
				}
			} else {
				// atribuir ao segundo ponto, o ponto de origem
				// mais o comprimento máximo do raio na direção do tiro
				laserLine.SetPosition(1, rayOrigin + (direcaoDoTiro * weaponRange));
			}
		}
	}

	// ligar e desligar o laser, isto é, fazer o disparo, com coroutine
	private IEnumerator ShotEffect() {

		gunAudio.Play(); // emitir o som do tiro
		laserLine.enabled = true; // laserLine inicia desligada. Aqui ela precisa ser ligada
		yield return shotDuration; // espera um pouco (shotDuration)
		laserLine.enabled = false; // para desligar o laserLine
	}
}
