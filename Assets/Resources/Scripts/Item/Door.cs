using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour
{
    bool opened = false;
    public float health = 300;
    private Transform bitBroken;
    private Transform veryBroken;
    private Transform completelyBroken;
    private GameObject curEnabled;

	// Use this for initialization
	void Start ()
	{
	    bitBroken = transform.FindChild("DoorBroke1");
	    veryBroken = transform.FindChild("DoorBroke2");
	    completelyBroken = transform.FindChild("DoorGibs");
        curEnabled = transform.FindChild("Cube.001").gameObject;
	}
	
	// Update is called once per frame
	void Update () {
	    if (health < 0)
	    {
	        GameObject.Find("WinHandler").GetComponent<WinConditions>().doorWin();
	        Instantiate(Resources.Load<GameObject>("Prefabs/GUI/WinScreens/EscapeWin"));
	        breakApart();
	    }
	    else if(health < health/3)
	    {
	        
	    }
	}


    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.GetComponent<ObjectScript>()!=null)
        {
            health -= col.relativeVelocity.magnitude*col.gameObject.rigidbody.mass;
        }
    }


    void breakApart()
    {
        //unfinished, add broken up mesh then break apart in script when thats in
        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Key_door") && !opened)
        {
            opened = true;
            GetComponent<Animator>().SetTrigger("open");

        }
    }
}
