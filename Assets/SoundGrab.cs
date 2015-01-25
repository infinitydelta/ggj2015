using UnityEngine;
using System.Collections;

public class SoundGrab : MonoBehaviour {
    public AudioClip SuccessClip;
    public AudioClip FailureClip;
    public AudioClip ThrowClip;
    AudioSource mySource;
    PlayerGrab myPlayerGrab;
    bool grabDownLastFrame = false;
    bool itemGrabbedLastFrame = false;
	// Use this for initialization
	void Start () {
        mySource = GetComponent<AudioSource>();
        myPlayerGrab = GetComponentInParent<PlayerGrab>();
	}
	
	// Update is called once per frame
	void Update () {
        if (myPlayerGrab.grabButtonDown && !grabDownLastFrame)
        {
            if (myPlayerGrab.itemIsGrabbed) //Grab Success
                mySource.PlayOneShot(SuccessClip);
            else if (itemGrabbedLastFrame)  //Throw
                mySource.PlayOneShot(ThrowClip);
            else  //Grab failure
                mySource.PlayOneShot(FailureClip);
        }
        grabDownLastFrame = myPlayerGrab.grabButtonDown;
        itemGrabbedLastFrame = myPlayerGrab.itemIsGrabbed;
	}
}
