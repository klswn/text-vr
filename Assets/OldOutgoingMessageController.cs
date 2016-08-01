using UnityEngine;
using System;
using System.Collections;
using System.IO;
using System.Web;
using System.Text;
using UnityEngine.Windows.Speech;

public class OldOutgoingMessageController : MonoBehaviour {
	private string recipient;
	private float curAnimDuration = 0.0f;
	private float animDuration;
	private Vector3 startPos;
	private Vector3 endPos;
	public AudioClip rec;

    //private DictationRecognizer dictationRecognizer;

	public void Initialize (string r, Vector3 initialEndPos) {
		this.recipient = r;
		string text = "To: " + r;
		GetComponentInChildren<SenderText> ().setSender (text);
		this.startPos = transform.position;
		this.endPos = initialEndPos;
		// starts the initial animation
		this.animDuration = 0.35f;

        PhraseRecognitionSystem.Shutdown();

        DictationRecognizer dictationRecognizer = new DictationRecognizer();

        dictationRecognizer.DictationResult += DictationRecognizer_DictationResult;

        dictationRecognizer.Start();
	}

    private void DictationRecognizer_DictationResult(string text, ConfidenceLevel confidence) {
        Debug.Log(text);
    }

	// Update is called once per frame
	void Update () {
		if (curAnimDuration < animDuration) {
			Animate ();
		}

		if (Input.GetKeyDown ("space")) {
			//StopRecording ();
			StartCoroutine (Remove ());
		}
	}

	//private void StartRecording() {
	//	rec = Microphone.Start (null, false, 10, 44100);
	//	while(!(Microphone.GetPosition(null) > 0)) {}
	//	GetComponent<AudioSource>().PlayOneShot(rec);
	//}

	//private void StopRecording() {
	//	Microphone.End (null);
	//	GetComponent<AudioSource> ().Stop ();
	//	SavWav.Save ("tmpWav", rec);
	//}

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
