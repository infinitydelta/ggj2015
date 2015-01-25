using UnityEngine;
using System.Collections;

public class Music : MonoBehaviour {
	public AudioSource mainAudio;
	public AudioSource bassAudio;
	public AudioClip[] mainClips;
	public AudioClip[] bassClips;
	private int clipToPlay;
	private int bassToPlay;

	// Use this for initialization
	void Start () {
		mainAudio.volume = 1.0f;
		bassAudio.volume = 0.8f;
	}
	
	// Update is called once per frame
	void Update () {
		if (mainAudio.isPlaying == false && bassAudio.isPlaying == false) {

			clipToPlay = Random.Range(0,mainClips.Length-1);
			bassToPlay = Random.Range(0,bassClips.Length-1);

			playSound(mainClips[clipToPlay], bassClips[bassToPlay]);
		}
	}
	void playSound(AudioClip sound, AudioClip bassSound){
		mainAudio.clip = sound;
		bassAudio.clip = bassSound;
		mainAudio.Play();
		bassAudio.Play();
	}
}

