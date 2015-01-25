using UnityEngine;
using System.Collections;

public class LoveMaking : MonoBehaviour {
	public PlayerMotion player_motion;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		player_motion.getOnOtherPlayer ();
	}
}
