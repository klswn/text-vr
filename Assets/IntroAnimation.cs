using UnityEngine;
using System.Collections;

public class IntroAnimation : MonoBehaviour {
	private Vector3 startPos;
	public Vector3 endPos;
	public float duration = 0.75f;
	private float currentDuration = 0.0f;

	// Use this for initialization
	void Start () {
		startPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		currentDuration += Time.deltaTime;
		if (currentDuration < duration) {
			float perc = Mathf.SmoothStep(0.0f, 1.0f, currentDuration / duration);
			transform.position = Vector3.Lerp (startPos, endPos, perc);
		}
	}
}
