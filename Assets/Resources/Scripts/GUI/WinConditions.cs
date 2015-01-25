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

    public void doorWin()
    {
        
    }

    public void chestOpen()
    {
        
    }

    public void baby()
    {
        Instantiate(Resources.Load("Prefabs/GUI/WinScreens/BabyWin"), transform.position, Quaternion.identity);
    }

    IEnumerator killFadeIn(String text)
    {
        yield return new WaitForEndOfFrame();
    }
}
