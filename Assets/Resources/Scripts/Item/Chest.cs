using UnityEngine;
using System.Collections;

public class Chest : MonoBehaviour 
{
	bool opened = false;
	public GameObject key;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other)
	{
		//Debug.Log(other.name);
		if(!opened && other.gameObject == key)
		{
			opened = true;
			this.GetComponent<Animator>().SetTrigger("open");
		}
	}
}
