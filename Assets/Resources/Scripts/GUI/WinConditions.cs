﻿using System;
using UnityEngine;
using System.Collections;

public class WinConditions : MonoBehaviour
{

    private GameObject title;

	// Use this for initialization
	void Start ()
	{
	    title = Resources.Load<GameObject>("Prefabs/GUI/Title");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void playerOneWinsKill()
    {
        
    }

    public void playerTwoWinsKill()
    {
        
    }

    IEnumerator killFadeIn(String text)
    {
        yield return new WaitForEndOfFrame();
    }
}