using UnityEngine;
using System.Collections;

public class SoundDeath : MonoBehaviour
{
    public AudioClip[] myClips;
    AudioSource mySource;

    // Use this for initialization
    void Start()
    {
        mySource = GetComponent<AudioSource>();
        mySource.PlayOneShot(myClips[Random.Range(0, myClips.Length - 1)]);
    }
}
