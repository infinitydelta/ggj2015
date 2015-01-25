using System;
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
        GameObject temp = (GameObject)Instantiate(title, title.transform.position, Quaternion.identity);
        UnityEngine.UI.Text text = temp.GetComponent<UnityEngine.UI.Text>();
        temp.GetComponent<Title>().setupChildren(11, true, 6, 8, 11, 12, "Player 1 Wins", 0, 2, 11, 12);
    }

    public void playerTwoWinsKill()
    {
        GameObject temp = (GameObject)Instantiate(title, title.transform.position, Quaternion.identity);
        temp.GetComponent<Title>().setupChildren(11,true,6,8,11,12,"Player 2 Wins",0,2,11,12);

    }

    IEnumerator killFadeIn(String text)
    {
        yield return new WaitForEndOfFrame();
    }
}
