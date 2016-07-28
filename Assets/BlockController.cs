using UnityEngine;
using System.Collections;

public class BlockController : MonoBehaviour {
	public AudioClip sound;

	private AudioSource source;

	// Use this for initialization
	void Awake () {
		source = GetComponent<AudioSource> ();
	}

	private void updateColor() {
		Color randomColor = new Color(Random.value, Random.value, Random.value);

		GetComponent<Renderer> ().material.color = randomColor;
	}

	private void playSound() {
		source.PlayOneShot (sound, 0.4f);
	}

	public void hit() {
		this.updateColor ();
		this.playSound ();
	}
}
