using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public float speedH = 2.0f;
	public float speedV = 2.0f;

	private float yaw = -90.0f;
	private float pitch = 0.0f;

	// Update is called once per frame
	void Update () {
		yaw += speedH * Input.GetAxis ("Mouse X");
		pitch -= speedV * Input.GetAxis ("Mouse Y");

		transform.eulerAngles = new Vector3 (pitch, yaw, 0.0f);

		if (Input.GetMouseButtonDown (0)) {
			RaycastHit hitInfo = new RaycastHit ();

			bool hit = Physics.Raycast (Camera.main.ScreenPointToRay (Input.mousePosition), out hitInfo);

			if (hit) {
				GameObject cube = hitInfo.transform.gameObject;
				if (cube.name.Equals ("Cube")) {
					Color randomColor = new Color(Random.value, Random.value, Random.value);

					cube.GetComponent<Renderer> ().material.color = randomColor;
				}
			}
		}

//		Debug.Log ("SCREEN WIDTH: " + Screen.width);
//		Debug.Log ("SCREEN HEIGHT: " + Screen.height);

	}
}
