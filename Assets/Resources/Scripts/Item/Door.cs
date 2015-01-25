using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour
{

    public float health = 300;
    
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if (health < 0)
	    {
	        GameObject.Find("WinHandler").GetComponent<WinConditions>().doorWin();
	        Instantiate(Resources.Load<GameObject>("Prefabs/GUI/WinScreens/EscapeWin"));
            breakApart();
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

}
