using UnityEngine;
using System.Collections;

public class LoveMaking : MonoBehaviour {
	private PlayerMotion player_motion;
	private Transform player;
	private ParticleSystem particles;
	private float love_amount;
	bool born;
	// Use this for initialization
	void Start () {
		love_amount = 0f;
		born = false;
        player = GameObject.Find("Player2").transform;
        player_motion = GameObject.Find("Player1").GetComponent<PlayerMotion>();
        particles = GetComponent<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update () {
			if (player_motion.getOnOtherPlayer () && !born) {
				transform.position = new Vector3(player.position.x,player.position.y,player.position.z);
				particles.emissionRate = 30;
			    love_amount += Time.deltaTime;
			}
            else {
					particles.emissionRate = 0;
			}
			if(love_amount > 10f && !born){ //spawn baby in here
			    born = true;
			    love_amount = 0;
                GameObject newBaby = (GameObject)Instantiate(Resources.Load("Prefabs/Objects/Baby"), transform.position, transform.rotation);
                newBaby.transform.localEulerAngles = new Vector3(-1f, newBaby.transform.localEulerAngles.y, newBaby.transform.localEulerAngles.z);
                GameObject.Find("WinHandler").GetComponent<WinConditions>().baby();
			}
		}
}
