using System;
using UnityEngine;
using System.Collections;

public class AnimateTexture : MonoBehaviour
{
    public float frameDuration = .5f;
    public String name = "bread";
    public int playerID = 1;

    private Texture[] frames = new Texture[6];
    private Material mat;
    private int curFrameStart;
    private int curFrame;
    private GameObject otherPlayer;
    private Camera otherCamera;


    enum directions
    {
        front = 2,
        back = 0,
        side = 4
    }

	// Use this for initialization
	
    void Start ()
    {
        mat = GetComponentInChildren<Renderer>().material;
        frames[0] = Resources.Load<Texture>("Sprites/" + name + "_walk_back1");
        frames[1] = Resources.Load<Texture>("Sprites/" + name + "_walk_back2");
        frames[2] = Resources.Load<Texture>("Sprites/" + name + "_walk_front1");
        frames[3] = Resources.Load<Texture>("Sprites/" + name + "_walk_front2");
        frames[4] = Resources.Load<Texture>("Sprites/" + name + "_walk_side1");
        frames[5] = Resources.Load<Texture>("Sprites/" + name + "_walk_side2");
        mat.SetTexture(0,frames[4]);
        StartCoroutine(playFrames());

        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        if (players[0].GetComponent<PlayerMotion>().controllerNumber != playerID)
        {
            otherPlayer = players[0];
        }
        else
        {
            otherPlayer = players[1];
        }
                otherCamera = otherPlayer.GetComponentInChildren<Camera>();

    }
	
	// Update is called once per frame
	void Update ()
	{
        transform.LookAt(transform.position + otherCamera.transform.rotation * Vector3.back,otherCamera.transform.rotation * Vector3.up);
        float angleBetween = Vector3.Angle(transform.forward, otherPlayer.transform.forward);
        Debug.Log(angleBetween);
	    if (angleBetween > 135 &&curFrameStart!=(int)directions.front)
	    {
	        curFrameStart = (int)directions.front;
	        curFrame = curFrameStart;   
            mat.SetTexture(0,frames[(int)directions.front]);
	    }
        else if (angleBetween < 45 && curFrameStart != (int)directions.back)
        {
            curFrameStart = (int)directions.back;
            curFrame = curFrameStart;
            mat.SetTexture(0, frames[(int)directions.back]);
        }
	}

    private IEnumerator playFrames()
    {
        while (true)
        {
            yield return new WaitForSeconds(frameDuration);
            if (curFrame == curFrameStart)
            {
                curFrame ++;
                mat.SetTexture(0, frames[curFrame]);
            }
            else
            {
                curFrame--;
                mat.SetTexture(0,frames[curFrame]);
            }
        }
    }
}
