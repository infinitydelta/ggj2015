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
		if(numbatteries < 2 && other.name == "AABattery")
		{
			other.gameObject.transform.parent = this.transform;
			this.rigidbody.mass += other.rigidbody.mass;
			Destroy (other.gameObject.rigidbody);
			if(numbatteries == 0)
			{
				other.transform.localPosition = new Vector3(-0.01134826f,-0.04417972f,-0.004475062f);
			}
			else
			{
				other.transform.localPosition = new Vector3(0.01134826f,-0.04417972f,-0.004475062f);
			}
			other.transform.rotation = this.transform.rotation;
			numbatteries++;
		}
	}
}
