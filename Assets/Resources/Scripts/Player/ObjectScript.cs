using System.CodeDom.Compiler;
using UnityEngine;
using System.Collections;

public class ObjectScript : MonoBehaviour
{

    public PlayerHealth  parent;
    public float damage;
    private float waitForRemoveTime = .2f;
    
    
	// Use this for initialization
	void Start ()
	{
	    //StartCoroutine(remove());
	}
	
	// Update is called once per frame
	void Update () {

    }

    void OnCollisionEnter(Collision col)
    {

        StartCoroutine(delayedDestroy());
        
    }

    void OnCollisionStay(Collision col)
    {

        StartCoroutine(delayedDestroy());
    }

    IEnumerator delayedDestroy()
    {
        yield return new WaitForEndOfFrame();
        Destroy(this);
    }

    IEnumerator remove()
    {
        yield return new WaitForSeconds(waitForRemoveTime);
        parent.health -= damage;
        Destroy(this);
    }
    
}
