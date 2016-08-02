using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Collections;

public class GameController : MonoBehaviour {
    private GameObject[] blocks;
    private int selected;
    private int right = 0;
    private int wrong = 0;
    private AudioSource audSource;
    public AudioClip hitSound;
    public AudioClip missSound;
    public GameObject correctScore;
    public GameObject incorrectScore;


    // Use this for initialization
    void Start () {
        correctScore.GetComponentInChildren<Text>().text = right.ToString();
        incorrectScore.GetComponentInChildren<Text>().text = wrong.ToString();
        audSource = GetComponent<AudioSource>();
        blocks = GameObject.FindGameObjectsWithTag("block").OrderBy( block => block.name ).ToArray();
        newColor();
	}

    private void newColor() {
        selected = Random.Range(0, 4);
        Debug.Log("selected in newcolor: " + selected);
        Color randColor = new Color(Random.value, Random.value, Random.value);
        //Color diffColor = randColor;
        Color diffColor = new Color(Random.value, Random.value, Random.value);
        diffColor.r += 0.2f;
        foreach (GameObject block in blocks)
        {
            block.GetComponent<Renderer>().material.color = randColor;
        }
        blocks[selected].GetComponent<Renderer>().material.color = diffColor;
    }

    public void handleBlockClick(int index) {
        Debug.Log("index in handle block: " + index);
        Debug.Log("selected in handle block" + selected);
        if (index == selected)
        {
            // hit
            audSource.PlayOneShot(hitSound, 0.4f);
            right++;
            correctScore.GetComponentInChildren<Text>().text = right.ToString();
            newColor();
        }
        else
        {
            // miss
            audSource.PlayOneShot(missSound, 0.4f);
            wrong++;
            incorrectScore.GetComponentInChildren<Text>().text = wrong.ToString();
        }
    }
}
