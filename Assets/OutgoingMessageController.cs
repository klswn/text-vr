using UnityEngine;
using System.Collections;

public class OutgoingMessageController : MonoBehaviour {
    private float curAnimDuration = 0.0f;
    private float animDuration;
    private Vector3 startPos;
    private Vector3 endPos;

    public AudioClip enterSound;
    public AudioClip exitSound;
    private AudioSource source;

    // Use this for initialization
    void Start () {
        source = GetComponent<AudioSource>();
        source.PlayOneShot(enterSound, 0.4f);
        this.startPos = transform.position;
        this.endPos = new Vector3(-1.869f, 2.58f, 0.07f);
        // starts the initial animation
        this.animDuration = 0.35f;
    }
	
	// Update is called once per frame
	void Update () {
        if (curAnimDuration < animDuration)
        {
            Animate();
        }
    }

    private void Animate()
    {
        curAnimDuration += Time.deltaTime;
        float p = Mathf.SmoothStep(0.0f, 1.0f, curAnimDuration / animDuration);
        transform.position = Vector3.Lerp(startPos, endPos, p);
    }

    public void updatePosition(Vector3 newPos, float newAnimDuration)
    {
        startPos = transform.position;
        endPos = newPos;
        curAnimDuration = 0.0f;
        animDuration = newAnimDuration;
    }

    public IEnumerator Remove(string cond = "cancel")
    {
        Vector3 newEndPos = transform.position;

        if (cond.Equals("cancel"))
        {
            newEndPos.y = -2.0f;
        }
        else
        {
            newEndPos.z = 9f;
        }

        source.PlayOneShot(exitSound, 0.4f);
        updatePosition(newEndPos, 0.15f);
        yield return new WaitForSeconds(0.25f);
        Destroy(gameObject);
    }
}
