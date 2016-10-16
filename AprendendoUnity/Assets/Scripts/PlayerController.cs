using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float speed;
	public GUIText countText;
	public GUIText winText;

	private Rigidbody rb;
	private int count;


	void Start() {

		rb = GetComponent<Rigidbody> ();
		count = 0;
		winText.gameObject.SetActive (false);
		UpdateCount ();
	}

	void FixedUpdate() {

		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");
		Vector3 vetor = new Vector3 (moveHorizontal, 0.0f, moveVertical);

		rb.AddForce (vetor * speed);
	}

	void OnTriggerEnter(Collider other) {
		if(other.gameObject.CompareTag("Pickup")) {
			other.gameObject.SetActive(false);
			count++;
			UpdateCount();
			if(count==12) {
				winText.gameObject.SetActive(true);
			}
		}
	}

	void UpdateCount() {
		countText.text = "Count: " + count.ToString();
	}
}
