using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SenderText : MonoBehaviour {
	public string getSender() {
		return gameObject.GetComponent<Text>().text;
	}

	public void setSender(string sndr) {
		gameObject.GetComponent<Text>().text = sndr;
	}
}
