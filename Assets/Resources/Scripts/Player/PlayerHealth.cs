using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour {

    public float damageVelCutoff = 10;
    public float health = 30f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (health <= 0f)
        {
            GameObject newDeadPlayer = (GameObject)Instantiate(Resources.Load("Prefabs/DeadPlayer" + GetComponent<PlayerMotion>().controllerNumber), transform.position, transform.rotation);
            newDeadPlayer.transform.localEulerAngles = new Vector3(-1f, newDeadPlayer.transform.localEulerAngles.y, newDeadPlayer.transform.localEulerAngles.z);
            Destroy(gameObject);
        }
	}

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag != "Player" && col.gameObject.tag != "Environment" && col.rigidbody != null)
        {
            /*ObjectScript temp = col.gameObject.AddComponent<ObjectScript>();
            temp.parent = this;
            temp.damage = col.relativeVelocity.magnitude*col.rigidbody.mass;*/

            if(col.gameObject.GetComponent<ObjectScript>()!=null)
            {
                health -= col.relativeVelocity.magnitude*col.gameObject.rigidbody.mass;
            }
        }
    }
}
