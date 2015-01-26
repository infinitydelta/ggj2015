using UnityEngine;
using System.Collections;

public class SoundDamage : MonoBehaviour {
    public AudioClip[] myClips;
    AudioSource mySource;
    PlayerHealth myHealth;
    float myLastHealth;

	// Use this for initialization
	void Start () {
        mySource = GetComponent<AudioSource>();
        myHealth = GetComponentInParent<PlayerHealth>();
        myLastHealth = myHealth.health;
	}
	
	// Update is called once per frame
	void Update () {
        if (myHealth.health < myLastHealth)
        {
            mySource.PlayOneShot(myClips[Random.Range(0,myClips.Length-1)]);
        }
        myLastHealth = myHealth.health;
	}
}
