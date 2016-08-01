using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OutgoingText : MonoBehaviour
{
    private bool caretActive = true;
    private string currentString;
    private float timeInterval;

    void Update() {
        timeInterval += Time.deltaTime;
        if (timeInterval > 1)
        {
            toggleCaret();
            timeInterval = 0;
        }
    }

    public void appendText(string letter)
    {
        currentString = gameObject.GetComponent<Text>().text;
        if (caretActive)
        {
            currentString = currentString.Substring(0, currentString.Length - 1) + letter + currentString[currentString.Length-1];
        }
        else {
            currentString += letter;
        }
        gameObject.GetComponent<Text>().text = currentString;
    }

    public void deleteText() {
        currentString = gameObject.GetComponent<Text>().text;
        if (caretActive && currentString.Length > 1)
        {
            currentString = currentString.Remove(currentString.Length - 2, 1);
        }
        else {
            if (currentString.Length > 0) currentString = currentString.Substring(0, currentString.Length - 1);
        }
        gameObject.GetComponent<Text>().text = currentString;
    }

    private void toggleCaret() {
        currentString = gameObject.GetComponent<Text>().text;
        if (caretActive)
        {
            // remove caret
            currentString = currentString.Replace("|", "");
            caretActive = false;
        } else
        {
            // add caret
            currentString += '|';
            caretActive = true;
        }
        gameObject.GetComponent<Text>().text = currentString;
    }
}
