using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary {

	public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour {

	// private Vector2 startPos;
	// private Vector2 direction;
	// public bool directionChosen;

	public float speed;
	public float tilt;
	public Boundary boundary;

	public GameObject shoot;
	public Transform shootSpawn;
	public float fireRate;

	private float nextFire;

	void Update() {

		if (Input.GetButton ("Fire1") && Time.time > nextFire) {
			nextFire = Time.time + fireRate;
			//GameObject clone = 
			Instantiate (shoot, shootSpawn.position, shootSpawn.rotation);
			AudioSource audio = GetComponent<AudioSource>();
			audio.Play();
		}
	}

	void FixedUpdate() {
		
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");


		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		GetComponent<Rigidbody>().velocity = movement * speed;
		GetComponent<Rigidbody> ().position = new Vector3 (
			Mathf.Clamp(GetComponent<Rigidbody>().position.x, boundary.xMin, boundary.xMax), 
			0.0f, 
			Mathf.Clamp(GetComponent<Rigidbody>().position.z, boundary.zMin, boundary.zMax)
			);
		GetComponent<Rigidbody>().rotation = Quaternion.Euler(0.0f,0.0f,GetComponent<Rigidbody>().velocity.x * -tilt);


		/* Track a single touch as a direction control.
		if (Input.touchCount > 0) {
			Touch touch = Input.GetTouch(0);

			// Handle finger movements based on touch phase.
			switch (touch.phase) {
			// Record initial touch position.
			case TouchPhase.Began:
				startPos = touch.position;
				// directionChosen = false;
				break;

				// Determine direction by comparing the current touch position with the initial one.
			case TouchPhase.Moved:
				direction = touch.position - startPos;

				Vector3 movement = new Vector3 (direction.x, 0.0f, direction.y);
				GetComponent<Rigidbody>().velocity = movement * speed;
				GetComponent<Rigidbody> ().position = new Vector3 (
					Mathf.Clamp(GetComponent<Rigidbody>().position.x, boundary.xMin, boundary.xMax), 
					0.0f, 
					Mathf.Clamp(GetComponent<Rigidbody>().position.z, boundary.zMin, boundary.zMax)
				);
				GetComponent<Rigidbody>().rotation = Quaternion.Euler(0.0f,0.0f,GetComponent<Rigidbody>().velocity.x * -tilt);

				break;

				// Report that a direction has been chosen when the finger is lifted.
			case TouchPhase.Ended:
				// directionChosen = true;
				break;
			}
		}
		*/
	}
}
