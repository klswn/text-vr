using UnityEngine;
using System.Collections;

public class MessagesManager : MonoBehaviour {
	public GameObject WorldSpaceMessage;
	public GameObject ScreenSpaceMessage;
	public GameObject OutgoingMessage;
	private GameObject[] messageQueue;
	private Vector3 firstWorld = new Vector3 (-3.49f, 2.64f, 2.67f);
	private Vector3 secondWorld = new Vector3 (-3.68f, 1.28f, 2.73f);
	private Vector3 secondWorldWithTitle = new Vector3 (-3.682f, 1.051f, 2.733f);
	private Vector3 firstScreen = new Vector3 (0.0f, 0.0f, 0.0f);
	private bool created1 = false;
	private bool created2 = false;
	private bool created3 = false;
	public string format = "World";

	void Update () {
		// TODO: connect to messaging API
		if (Time.time > 1 && !created1) {
			string sndr = "John Doe";
			string msg = "Hey this is the first message";
			created1 = true;
			createIncomingMessage(sndr, msg);
		}

		if (Time.time > 4 && !created2) {
			created2 = true;
			createIncomingMessage("Jane Doe", "Hey this is the second message");
		}

		if (Time.time > 6 && !created3) {
			created3 = true;
			createIncomingMessage("Jane Doe", "Oh I forgot to say this is the third message");
		}
	}

	void createIncomingMessage(string sender, string message) {
		messageQueue = GameObject.FindGameObjectsWithTag ("incomingMessage");
		bool noOutGoingMessage = GameObject.FindGameObjectsWithTag ("outgoingMessage").Length == 0;

		if (noOutGoingMessage) {
			if (this.format.Equals ("World")) {
				GameObject newMessage = Instantiate (WorldSpaceMessage);
				newMessage.GetComponent<MessageController> ().Initialize (sender, message, firstWorld, this.format);
			} else if (this.format.Equals ("Screen")) {
				GameObject screenOverlay = GameObject.FindGameObjectWithTag ("screenOverlay");
				GameObject newMessage = Instantiate (ScreenSpaceMessage);
				float posX = (Screen.width / 2.0f) - (Screen.width * 0.05f) - 200f;
				float posY = (Screen.height / 2.0f) - (Screen.height * 0.1f) - 100f;
				firstScreen = new Vector3 (posX, posY, 0.0f);
				newMessage.GetComponent<MessageController> ().Initialize (sender, message, format: this.format);
				newMessage.transform.SetParent (screenOverlay.transform, false);
				newMessage.transform.localPosition = firstScreen;
			}

			updateMessageQueue (sender, messageQueue.Length);
		}


	}

	void updateMessageQueue(string sender, int index) {
		if (index == 1) {
			GameObject existingMsg = messageQueue [0];
			cleanUpQueue (sender, existingMsg);
		} else if (index == 2) {
			GameObject oldestMsg = messageQueue [0];
			GameObject existingMsg = messageQueue [1];
			StartCoroutine(oldestMsg.GetComponent<MessageController> ().Remove (this.format));
			cleanUpQueue (sender, existingMsg);
		} else if (index > 2) {
			Debug.Log ("More than two messages error");
		}
	}

	void cleanUpQueue(string sender, GameObject existingMsg) {
		string existingSender = existingMsg.GetComponent<MessageController>().getSender ();
		Vector3 secondPos = new Vector3(0.0f, 0.0f, 0.0f);
		Vector3 secondPosWithTitle = new Vector3(0.0f, 0.0f, 0.0f);;

		if (this.format.Equals ("World")) {
			if (existingSender.Equals(sender)) {
				existingMsg.GetComponent<MessageController> ().updatePosition (secondWorld, 0.4f);
				existingMsg.GetComponent<MessageController> ().removeSenderText ();
			} else {
				existingMsg.GetComponent<MessageController> ().updatePosition (secondWorldWithTitle, 0.4f);
			}
		} else if (this.format.Equals ("Screen")) {
			secondPos = firstScreen;
			secondPos.y -= 200.0f;
			secondPosWithTitle = firstScreen;
			secondPosWithTitle.y -= 230.0f;

			if (existingSender.Equals(sender)) {
				existingMsg.transform.localPosition = secondPos;
				existingMsg.GetComponent<MessageController> ().removeSenderText ();
			} else {
				existingMsg.transform.localPosition = secondPosWithTitle;
			}
		}
	}

	private void removeAllMessages() {
		messageQueue = GameObject.FindGameObjectsWithTag ("incomingMessage");
		foreach (GameObject message in messageQueue) {
			StartCoroutine(message.GetComponent<MessageController> ().Remove (this.format));
		}
	}

	public void createOutgoingMessage(string sender) {
		// get camera position and offset new message spawn point
		float cameraPosX = GameObject.FindWithTag("Player").transform.position.x;
		Debug.Log ("CameraPosX: " + cameraPosX);
		float startPosX = cameraPosX - 4.0f;

		// create outgoing message UI
		GameObject newOutgoingMessage = Instantiate (OutgoingMessage);
		newOutgoingMessage.GetComponent<OutgoingMessageController> ().Initialize (sender, startPosX);

		removeAllMessages ();
	}
}
