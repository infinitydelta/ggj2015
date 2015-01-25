using System;
using UnityEngine;
using System.Collections;

public class WinConditions : MonoBehaviour
{
	// Use this for initialization
	void Start ()
	{
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    public void playerOneWinsKill()
    {
        Instantiate(Resources.Load("Prefabs/GUI/WinScreens/Player1Win"), transform.position, Quaternion.identity);
    }

    public void playerTwoWinsKill()
    {
        Instantiate(Resources.Load("Prefabs/GUI/WinScreens/Player2Win"), transform.position, Quaternion.identity);
    }

    public void chestOpen()
    {
        
    }

    IEnumerator killFadeIn(String text)
    {
        yield return new WaitForEndOfFrame();
    }
}
