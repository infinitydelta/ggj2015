using UnityEngine;
using System.Collections;

public class Jump : MonoBehaviour {

	public PlayerMotion player_motion;

	void OnTriggerEnter(Collider other) {
		player_motion.GroundEnter();
	}
	void OnTriggerExit(Collider other) {
		player_motion.GroundExit();
	}
	void OnTriggerStay(Collider other) {
		player_motion.GroundStay();
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
