using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class KeyController : MonoBehaviour {
    public string normalValue = " ";
    public string shiftValue = " ";

	// Use this for initialization
	void Start () {
        gameObject.GetComponentInChildren<Text>().text = shiftValue;
	}
	
	// Update is called once per frame
	void Update () {

            if (KeyboardController.shift)
            {
                gameObject.GetComponentInChildren<Text>().text = shiftValue;
            } else
            {
                gameObject.GetComponentInChildren<Text>().text = normalValue;
            }
	}

    public string getValue() {
        return gameObject.GetComponentInChildren<Text>().text;
    }
}
