using UnityEngine;
using System;
using System.Collections;
using System.IO;
using System.Web;
using System.Text;

public class OutgoingMessageController : MonoBehaviour {
	private string recipient;
	private float curAnimDuration = 0.0f;
	private float animDuration;
	private Vector3 startPos;
	private Vector3 endPos;
	public AudioClip rec;

	public void Initialize (string r, Vector3 initialEndPos) {
		this.recipient = r;
		string text = "To: " + r;
		GetComponentInChildren<SenderText> ().setSender (text);
		this.startPos = transform.position;
		this.endPos = initialEndPos;
		// starts the initial animation
		this.animDuration = 0.35f;

		StartRecording ();
	}

	// Update is called once per frame
	void Update () {
		if (curAnimDuration < animDuration) {
			Animate ();
		}

		if (Input.GetKeyDown ("space")) {
			StopRecording ();
			StartCoroutine (Remove ());
		}
	}

	private void StartRecording() {
		rec = Microphone.Start (null, false, 10, 44100);
		while(!(Microphone.GetPosition(null) > 0)) {}
		GetComponent<AudioSource>().PlayOneShot(rec);
	}

	private void StopRecording() {
		Microphone.End (null);
		GetComponent<AudioSource> ().Stop ();
		SavWav.Save ("tmpWav", rec);

		StartCoroutine (WwwGoogleSpeechRequest ("tmpWav", 44100));
	}

	private IEnumerator WwwGoogleSpeechRequest(string fileName, int sampleRate) {
        const string apiKey = "insertAPIKeyHere";
		const string url = "https://speech.googleapis.com/v1beta1/speech:syncrecognize?key=" + apiKey;
		var encoding = new System.Text.UTF8Encoding ();

		Debug.Log ("entering request method");

		var form = new WWWForm ();

		var headers = form.headers;

		headers ["Method"] = "POST";
		headers ["Content-Type"] = "application/json";
		//headers ["Authorization"] = "Bearer " + "AIzaSyCOUyEG1zENYTO2TaDB6Eqjscy8y13RvIA";

		GoogleSpeechRequest requestObj = new GoogleSpeechRequest ();

		var bytes = File.ReadAllBytes (@"Assets/tmpWav.wav");
		string base64Data = Convert.ToBase64String (bytes);
		Debug.Log (base64Data);
		requestObj.setAudio (base64Data);

		string requestData = JsonUtility.ToJson (requestObj);

		Debug.Log (requestData);

		//form.AddBinaryData ("fileUpload", postData, "flacFile", "audio/x-flac; rate=" + sampleRate);
		var httpRequest = new WWW (url, encoding.GetBytes(requestData), headers);

		yield return httpRequest;

		if (httpRequest.isDone && string.IsNullOrEmpty(httpRequest.error)) {
			string response = httpRequest.text.Substring(1);
			Debug.Log(response);
		}
		else {
			Debug.Log("Request failed");
			Debug.Log (httpRequest.error);
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
