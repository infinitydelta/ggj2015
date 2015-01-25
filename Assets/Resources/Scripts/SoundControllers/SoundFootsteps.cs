using UnityEngine;
using System.Collections;

public class SoundFootsteps : MonoBehaviour {
    AudioSource mySource;
    Rigidbody myRigidBody;
    public float sensitivity = 0.32f;
	// Use this for initialization
	void Start () {
        mySource = GetComponent<AudioSource>();
        myRigidBody = GetComponentInParent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        if (myRigidBody.velocity.magnitude == 0.00f)
        {
            mySource.Stop();
        }
        else
        {
            if (!mySource.isPlaying)
                mySource.Play();
            mySource.pitch = sensitivity * myRigidBody.velocity.magnitude;
        }
	}
}
