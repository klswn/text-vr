using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ControllerInput : BaseInputModule {

    [Header(" [Runtime variables]")]
    [Tooltip("Indicates whether or not the gui was hit by any controller this frame")]
    public bool GuiHit;

    [Tooltip("Indicates whether or not a button was used this frame")]
    public bool ButtonUsed;

    [Tooltip("Generated cursors")]
    public RectTransform[] Cursors;

    private GameObject[] CurrentPoint;
    private GameObject[] CurrentPressed;
    private GameObject[] CurrentDragging;

    private PointerEventData[] PointEvents;

    private bool Initialized = false;

    [Tooltip("Generated non rendering camera (used for raycasting ui)")]
    public Camera ControllerCamera;

    private SteamVR_ControllerManager ControllerManager;
    private SteamVR_TrackedObject[] Controllers;
    private SteamVR_Controller.Device[] ControllerDevices;

    protected override void Start () {
        base.Start();

        ControllerCamera = new GameObject("Controller UI Camera").AddComponent<Camera>();
        ControllerCamera.clearFlags = CameraClearFlags.Nothing; //CameraClearFlags.Depth;
        ControllerCamera.cullingMask = 0; // 1 << LayerMask.NameToLayer("UI"); 

        ControllerManager = GameObject.FindObjectOfType<SteamVR_ControllerManager>();
        Controllers = new SteamVR_TrackedObject[] { ControllerManager.left.GetComponent<SteamVR_TrackedObject>(), ControllerManager.right.GetComponent<SteamVR_TrackedObject>() };
        ControllerDevices = new SteamVR_Controller.Device[Controllers.Length];
        Cursors = new RectTransform[Controllers.Length];
    }
	
    public override void Process()
    {
        throw new System.NotImplementedException();
    }
}
