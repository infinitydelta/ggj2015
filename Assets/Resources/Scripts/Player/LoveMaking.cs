using UnityEngine;
using System.Collections;

public class LoveMaking : MonoBehaviour {
	public PlayerMotion player_motion;
	public Transform player;
	public ParticleSystem particles;
	// Use this for initialization
	void Start () {
		//player = transform.Find ("Player2");
	}
	
	// Update is called once per frame
	void Update () {
			if (player_motion.getOnOtherPlayer ()) {
				transform.position = new Vector3(player.position.x,player.position.y,player.position.z);
				particles.emissionRate = 30;
			} else {
					particles.emissionRate = 0;
			}
		}
}
