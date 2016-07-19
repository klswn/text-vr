using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MessageText : MonoBehaviour {
	public string getMessage() {
		return gameObject.GetComponent<Text> ().text;
	}

	public void setMessage(string msg) {
		gameObject.GetComponent<Text>().text = msg;
	}
}
