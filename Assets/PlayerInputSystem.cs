using UnityEngine;
using System.Collections;

public class PlayerInputSystem : MonoBehaviour {

	public float movementSpeed = 2.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		bool leftInput = Input.GetKey (KeyCode.A);
		bool rightInput = Input.GetKey (KeyCode.D);
		bool backInput = Input.GetKey (KeyCode.S);
		bool forwardInput = Input.GetKey (KeyCode.W);

		if (leftInput) {
			transform.Translate (Vector3.left * movementSpeed * Time.deltaTime);
		}
		if (rightInput) {
			transform.Translate (Vector3.right * movementSpeed * Time.deltaTime);
		}
		if (forwardInput) {
			transform.Translate (Vector3.forward * movementSpeed * Time.deltaTime);
		}
		if (backInput) {
			transform.Translate (Vector3.back * movementSpeed * Time.deltaTime);
		}
	}
}
