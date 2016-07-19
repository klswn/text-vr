using UnityEngine;
using System.Collections;

public class OutgoingMessageController : MonoBehaviour {
	private string recipient;
	private float curAnimDuration = 0.0f;
	private float animDuration;
	private Vector3 startPos;
	private Vector3 endPos;

	public void Initialize (string r, float startPosX) {
		this.recipient = r;
		string text = "To: " + r;
		GetComponentInChildren<SenderText> ().setSender (text);
		startPos = transform.position;
		startPos.x = startPosX;
		endPos = new Vector3 (startPosX, 2.0f, 0.25f);
		// starts the initial animation
		animDuration = 0.35f;
	}

	// Update is called once per frame
	void Update () {
		if (curAnimDuration < animDuration) {
			Animate ();
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
}
