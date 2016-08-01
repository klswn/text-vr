using UnityEngine;
using System.Collections;

public class KeyboardController : MonoBehaviour {
    public static bool shift = true;
    public GameObject OutgoingText;
    public AudioClip clickSound;
    private AudioSource source;

    // Use this for initialization
    void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    public void handleKey(string character)
    {
        source.PlayOneShot(clickSound, 0.4f);

        if (character.Equals("back"))
        {
            OutgoingText.GetComponent<OutgoingText>().deleteText();
        }
        else if (character.Equals("Shift"))
        {
            toggleShift();
        }
        else
        {
            OutgoingText.GetComponent<OutgoingText>().appendText(character);
        }
    }

    public void toggleShift() {
        shift = !shift;
    }               
}
