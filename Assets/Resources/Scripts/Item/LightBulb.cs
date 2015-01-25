using UnityEngine;
using System.Collections;

public class LightBulb : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag != "Player" && (col.gameObject.GetComponent<ObjectScript>()!=null||GetComponent<ObjectScript>()!=null))
        {
            GameObject.Find("WinHandler").GetComponent<WinConditions>().numLightbulbs--;
            Destroy(gameObject);
        }
    }
}
