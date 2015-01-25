using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour {
    public float health = 100f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (health <= 0f)
        {
            GetComponent<PlayerMotion>().enabled = false;
            GetComponent<PlayerGrab>().enabled = false;
            rigidbody.constraints = RigidbodyConstraints.None;
        }
	}

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag != "Player" && col.gameObject.tag != "Environment")
        {
            health -= col.relativeVelocity.magnitude * col.rigidbody.mass;
        }
    }
}
