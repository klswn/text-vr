using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HandController : MonoBehaviour {
	private Valve.VR.EVRButtonId triggerButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;

	private SteamVR_Controller.Device controller { get { return SteamVR_Controller.Input((int)trackedObj.index); } }
	private SteamVR_TrackedObject trackedObj;

	// Use this for initialization
	void Start() {
		trackedObj = GetComponent<SteamVR_TrackedObject>();
	}

	// Update is called once per frame
	void Update() {
		if (controller == null) {
			Debug.Log("Controller not initialized");
			return;
		}

		if (controller.GetPressDown(triggerButton)) {
			Debug.Log ("trigger down");
		}
	}
}