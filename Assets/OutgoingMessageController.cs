using UnityEngine;
using System.Collections;

public class OutgoingMessageController : MonoBehaviour {
	private string recipient;
	private float curAnimDuration = 0.0f;
	private float animDuration;
	private Vector3 startPos;
	private Vector3 endPos;

	public void Initialize (string r, Vector3 initialEndPos) {
		this.recipient = r;
		string text = "To: " + r;
		GetComponentInChildren<SenderText> ().setSender (text);
		this.startPos = transform.position;
		this.endPos = initialEndPos;
		// starts the initial animation
		this.animDuration = 0.35f;

		foreach (string device in Microphone.devices) {
			Debug.Log ("Name: " + device);
		}

		AudioSource aud = GetComponent<AudioSource> ();
		aud.clip = Microphone.Start ("Microphone (2- USB Audio Device)", true, 10, 44100);
		aud.Play ();
	}

	// Update is called once per frame
	void Update () {
		if (curAnimDuration < animDuration) {
			Animate ();
		}

		if (Input.GetKeyDown ("space")) {
			StartCoroutine (Remove ());
		}
	}

	private void Animate() {
		curAnimDuration += Time.deltaTime;
		float p = Mathf.SmoothStep(0.0f, 1.0f, curAnimDuration / animDuration);
		transform.position = Vector3.Lerp (startPos, endPos, p);
	}

	public void updatePosition(Vector3 newPos, float newAnimDuration) {
		startPos = transform.position;
		endPos = newPos;
		curAnimDuration = 0.0f;
		animDuration = newAnimDuration;
	}

	public IEnumerator Remove() {
		Vector3 newEndPos = transform.position;

		newEndPos.y = -2.0f;

		updatePosition (newEndPos, 0.15f);
		yield return new WaitForSeconds (0.25f);
		Destroy (gameObject);
	}
}
