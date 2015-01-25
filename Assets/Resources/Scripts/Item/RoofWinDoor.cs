using UnityEngine;
using System.Collections;

public class RoofWinDoor : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other){
		if (other.tag == "Player") {
			GameObject.Find("WinHandler").GetComponent<WinConditions>().CeilingDoor();
		}
	}
}
