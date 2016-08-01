using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public struct PointerEventArgs
{
    public uint controllerIndex;
    public uint flags;
    public float distance;
    public Transform target;
}

public delegate void PointerEventHandler(object sender, PointerEventArgs e);


public class HandController : MonoBehaviour
{
    public bool active = true;
    public Color color;
    public float thickness = 0.002f;
    public GameObject holder;
    public GameObject pointer;
    bool isActive = false;
    public bool addRigidBody = false;
    public Transform reference;
    public event PointerEventHandler PointerIn;
    public event PointerEventHandler PointerOut;
    private List<GameObject> hitKeys = new List<GameObject>();

    Transform previousContact = null;

    private Valve.VR.EVRButtonId triggerButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;

    private SteamVR_Controller.Device controller { get { return SteamVR_Controller.Input((int)trackedObj.index); } }
    private SteamVR_TrackedObject trackedObj;

    // Use this for initialization
    void Start()
    {

        trackedObj = GetComponent<SteamVR_TrackedObject>();

        holder = new GameObject();
        holder.transform.parent = this.transform;
        holder.transform.localPosition = Vector3.zero;

        pointer = GameObject.CreatePrimitive(PrimitiveType.Cube);
        pointer.transform.parent = holder.transform;
        pointer.transform.localScale = new Vector3(thickness, thickness, 100f);
        pointer.transform.localPosition = new Vector3(0f, 0f, 50f);
        BoxCollider collider = pointer.GetComponent<BoxCollider>();
        if (addRigidBody)
        {
            if (collider)
            {
                collider.isTrigger = true;
            }
            Rigidbody rigidBody = pointer.AddComponent<Rigidbody>();
            rigidBody.isKinematic = true;
        }
        else
        {
            if (collider)
            {
                Object.Destroy(collider);
            }
        }
        Material newMaterial = new Material(Shader.Find("Unlit/Color"));
        newMaterial.SetColor("_Color", color);
        pointer.GetComponent<MeshRenderer>().material = newMaterial;
    }

    public virtual void OnPointerIn(PointerEventArgs e)
    {
        if (PointerIn != null)
            PointerIn(this, e);
    }

    public virtual void OnPointerOut(PointerEventArgs e)
    {
        if (PointerOut != null)
            PointerOut(this, e);
    }


    // Update is called once per frame
    void Update()
    {
        if (!isActive)
        {
            isActive = true;
            this.transform.GetChild(0).gameObject.SetActive(true);
        }

        float dist = 100f;

        Ray raycast = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        bool bHit = Physics.Raycast(raycast, out hit);

        if (!bHit)
        {
            previousContact = null;
        }
        if (bHit && hit.distance < 100f)
        {
            dist = hit.distance;
        }

        if (controller != null && hit.transform != null && controller.GetPressDown(triggerButton))
        {
            pointer.transform.localScale = new Vector3(thickness * 15f, thickness * 15f, dist);
            GameObject hitObj = hit.transform.gameObject;
            if (hitObj.name.Equals("Block"))
            {
                hitObj.GetComponent<BlockController>().hit();
            }
            else if (hitObj.name.Equals("WorldSpaceMessage(Clone)") || hitObj.name.Equals("ScreenSpaceMessage(Clone)"))
            {
                hitObj.GetComponent<MessageController>().createOutgoingMessage();
            }
            else if (hitObj.tag.Equals("key"))
            {
                string character = hitObj.GetComponent<KeyController>().getValue();
                GameObject Keyboard = GameObject.FindGameObjectWithTag("keyboard");
                Keyboard.GetComponent<KeyboardController>().handleKey(character);
                hitKeys.Add(hitObj);
                hitObj.GetComponent<Image>().color = new Color(0f, 0f, 0f, 0.7f);
            }
            else if (hitObj.tag.Equals("action")) {
                string cond = "cancel";
                GameObject outgoingMessage = GameObject.FindGameObjectWithTag("outgoingMessage");
                if (hitObj.name.Equals("Send")) cond = "send";
                StartCoroutine(outgoingMessage.GetComponent<OutgoingMessageController>().Remove(cond));
            }
        }
        else if (controller != null && controller.GetPressUp(triggerButton))
        {
            foreach (GameObject key in hitKeys) {
                key.GetComponent<Image>().color = new Color(1,1,1);
            }
            hitKeys.Clear();
        }
        else
        {
            pointer.transform.localScale = new Vector3(thickness, thickness, dist);
        }
        pointer.transform.localPosition = new Vector3(0f, 0f, dist / 2f);
    }
}
