using UnityEngine;
using System.Collections;

public class DestroyMessage : MonoBehaviour {
	private float lifetime = 0.0f;

	void Update () {
		lifetime += Time.deltaTime;
		if (lifetime > 8) {
			Destroy (gameObject);
		}
	}
}
