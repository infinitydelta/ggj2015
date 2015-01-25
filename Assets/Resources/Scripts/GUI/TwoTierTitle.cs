using System;
using UnityEngine;
using System.Collections;

public class TwoTierTitle : MonoBehaviour
{

    public float fadeInTime = 2;
    public float stayTime = 4;
    public float fadeOutTime = 4;
    public float endTime = 7;
    public bool reload;
    public String text;
    public String textSecond;
    private float timer;
    private UnityEngine.UI.Text textComp;
    private float curAlpha = 0;
    private GameObject normTitle;
    private UnityEngine.UI.Image overlay;
    private Color startColor;
    
	// Use this for initialization
	void Start ()
	{
        
	    overlay = GetComponentInChildren<UnityEngine.UI.Image>();
	    textComp = GetComponentInChildren<UnityEngine.UI.Text>();
	    textComp.text = text;
	    normTitle = Resources.Load<GameObject>("Prefabs/GUI/Title");
	    startColor = textComp.color;
	}
	
	// Update is called once per frame
	void Update ()
	{
	    timer += Time.deltaTime;
	    if (timer < fadeInTime)
	    {
	        curAlpha = timer/fadeInTime;
	        textComp.color = new Color(startColor.r,startColor.g,startColor.b,curAlpha);
	    }
        else if (timer < stayTime)
        {

        }
        else if (timer < fadeOutTime)
        {
            curAlpha -= (timer - stayTime)/(fadeOutTime - stayTime);
            textComp.color = new Color(startColor.r, startColor.g, startColor.b, curAlpha);
        }
        else if (timer < endTime)
        {
            overlay.enabled = true;
            textComp.text = textSecond;
        }
        else
        {
            if (reload)
            {
                Application.LoadLevel(Application.loadedLevel);
            }
            else
            {
                Destroy(gameObject);
            }
        }
	}

   

}
