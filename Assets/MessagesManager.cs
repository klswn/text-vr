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
	private float posXBuffer = -0.20f;
	private float posYBuffer = 0.20f;
	public string format = "World";

	private string sndr = "John Doe";
	private string sndr2 = "Jane Doe";

	private string msg = "Hey this is a message!";
	private string msg2 = "This is a different message!";

	void Update () {
		// TODO: connect to messaging API
		if (Input.GetKeyDown("space")) {
            //createOutgoingMessage (sndr);
            if (Random.value < 0.5f)
            {
                createIncomingMessage(sndr, msg);
            }
            else
            {
                createIncomingMessage(sndr2, msg2);
            }
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
				float posX = (Screen.width / 2.0f) + (Screen.width * this.posXBuffer) - 150f;
				float posY = (Screen.height / 2.0f) - (Screen.height * this.posYBuffer) - 75f;
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
			secondPos.y -= 155.0f;
			secondPosWithTitle = firstScreen;
			secondPosWithTitle.y -= 180.0f;

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
		//Vector3 startPos = GameObject.FindWithTag("MainCamera").transform.position;
		//startPos.x -= 4.5f;
		//startPos.y = 2.0f;
		//startPos.z += 0.25f;

        // create outgoing message UI
        Instantiate(OutgoingMessage);
		//GameObject newOutgoingMessage = Instantiate (OutgoingMessage);
		//newOutgoingMessage.GetComponent<OutgoingMessageController> ().Initialize (sender, startPos);

		removeAllMessages ();
	}
}
