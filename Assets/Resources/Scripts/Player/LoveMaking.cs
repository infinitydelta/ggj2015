using UnityEngine;
using System.Collections;

public class LoveMaking : MonoBehaviour {
	public PlayerMotion player_motion;
	public Transform player;
	public ParticleSystem particles;
	private int love_amount;
	// Use this for initialization
	void Start () {
		love_amount = 0;
	}
	
	// Update is called once per frame
	void Update () {
			if (player_motion.getOnOtherPlayer ()) {
				transform.position = new Vector3(player.position.x,player.position.y,player.position.z);
				particles.emissionRate = 30;
			love_amount++;
			} else {
					particles.emissionRate = 0;
			}
		}
}
