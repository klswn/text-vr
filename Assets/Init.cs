using UnityEngine;
using System.Collections;

public class Init : MonoBehaviour {

	public GameObject WorldSpaceMessage;
	private bool created1 = false;
	private bool created2 = false;

	// Update is called once per frame
	void Update () {
		if (Time.time > 1 && !created1) {
			Instantiate (WorldSpaceMessage);
			created1 = true;
		}
		if (Time.time > 4 && !created2) {
			Instantiate (WorldSpaceMessage);
			created2 = true;
		}
	}
}
