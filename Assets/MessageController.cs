using UnityEngine;
using System.Collections;

public class MessageController : MonoBehaviour {
	private float lifetime = 0.0f;
	private float curAnimDuration = 0.0f;
	private Vector3 startPos;
	private Vector3 endPos;
	private float animDuration;
	private string sender;
	private GameObject MessagesManager;

	public void Initialize (string s, string m, Vector3 introEnd = default(Vector3), string format = "World") {
		// set sender class variable and text
		this.sender = s;
		GetComponentInChildren<SenderText> ().setSender (s);
		// set message text
		GetComponentInChildren<MessageText> ().setMessage (m);
		MessagesManager = GameObject.FindGameObjectWithTag ("messagesManager");
		startPos = transform.position;
		Debug.Log ("introEnd: " + introEnd);
		if (introEnd != default(Vector3)) {
			endPos = introEnd;
			// starts the initial animation
			animDuration = 0.25f;
		}
	}

	void Update () {
		checkLifetime ();

		if (curAnimDuration < animDuration) {
			Animate ();
		}
	}

	private void checkLifetime() {
		lifetime += Time.deltaTime;
		if (lifetime > 8) {
			StartCoroutine(Remove ());
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

	public void removeSenderText() {
		// destroy the sender component
		Destroy(transform.GetChild(1).gameObject);
	}

	public IEnumerator Remove(string format = "World") {
		Debug.Log ("remove called on " + this.sender);
		Vector3 newEndPos = transform.position;

		if (format.Equals ("World")) {
			newEndPos.z = 7.5f;
		} else if (format.Equals ("Screen")) {
			newEndPos.x = 5000f;
		} else {
			Debug.Log ("Format Error in Remove()");
		}

		updatePosition (newEndPos, 0.15f);
		yield return new WaitForSeconds (1);
		Destroy (gameObject);
	}

	public void createOutgoingMessage() {
		Debug.Log ("hey");
		MessagesManager.GetComponent<MessagesManager> ().createOutgoingMessage (this.sender);
	}

	public string getSender() {
		return this.sender;
	}
}
