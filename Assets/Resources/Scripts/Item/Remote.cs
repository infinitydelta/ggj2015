using UnityEngine;
using System.Collections;

public class Remote : MonoBehaviour 
{
	int numbatteries = 0;
	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
	void OnTriggerEnter(Collider other)
	{
		//Debug.Log (other.name);
		if(numbatteries < 2 && other.name == "aabattery")
		{
			other.gameObject.transform.parent = this.transform;
			this.rigidbody.mass += other.rigidbody.mass;
			Destroy (other.gameObject.rigidbody);
			numbatteries++;
		}
	}
}
