﻿using System;
using UnityEngine;
using System.Collections;
using System.Linq;

public class WinConditions : MonoBehaviour
{

    public float numLightbulbs;
    private GameObject directLight;
	// Use this for initialization
	void Start ()
	{
	    numLightbulbs = GameObject.FindGameObjectsWithTag("Bulb").Count();
	    directLight = GameObject.Find("Directional light");
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

	public void lightsOut()
	{
		Instantiate(Resources.Load("Prefabs/GUI/WinScreens/LightsOut"), transform.position, Quaternion.identity);
        directLight.SetActive(false);
	}

	public void CeilingDoor()
	{
		Instantiate(Resources.Load("Prefabs/GUI/WinScreens/CeilingEscapeWin"), transform.position, Quaternion.identity);
	}

    IEnumerator killFadeIn(String text)
    {
        yield return new WaitForEndOfFrame();
    }
}
