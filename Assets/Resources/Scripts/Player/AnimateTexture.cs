using System;
using UnityEngine;
using System.Collections;

public class AnimateTexture : MonoBehaviour
{
    public float frameDuration = .5f;
    public String name = "bread";

    private Texture[] frames = new Texture[6];
    private Texture[] idleFrames = new Texture[6];
    private Material mat;
    private int curFrameStart;
    private int curFrame;
    private GameObject otherPlayer;
    private Camera otherCamera;
    private Quaternion lookTarget;
    Vector3 origScale;
    GameObject child;
    private IEnumerator curEnum;
    private bool idle = false;
    private float angleBetween;
    private float dotAngle;
    private float rightAngle;

    enum directions
    {
        front = 2,
        back = 0,
        side = 4
    }

	// Use this for initialization
	
    void Start ()
    {
        child = transform.FindChild("Billboard").gameObject;

        origScale = transform.localScale;
        mat = GetComponentInChildren<Renderer>().material;
        frames[0] = Resources.Load<Texture>("Sprites/" + name + "_walk_back1");
        frames[1] = Resources.Load<Texture>("Sprites/" + name + "_walk_back2");
        frames[2] = Resources.Load<Texture>("Sprites/" + name + "_walk_front1");
        frames[3] = Resources.Load<Texture>("Sprites/" + name + "_walk_front2");
        frames[4] = Resources.Load<Texture>("Sprites/" + name + "_walk_side1");
        frames[5] = Resources.Load<Texture>("Sprites/" + name + "_walk_side2");

        idleFrames[0] = Resources.Load<Texture>("Sprites/" + name + "_back_idle1");
        idleFrames[1] = Resources.Load<Texture>("Sprites/" + name + "_back_idle2");
        idleFrames[2] = Resources.Load<Texture>("Sprites/" + name + "_front_idle1");
        idleFrames[3] = Resources.Load<Texture>("Sprites/" + name + "_front_idle2");
        idleFrames[4] = Resources.Load<Texture>("Sprites/" + name + "_idle_side1");
        idleFrames[5] = Resources.Load<Texture>("Sprites/" + name + "_idle_side2");

        mat.SetTexture(0,frames[4]);
        curEnum = playFrames();
        StartCoroutine(curEnum);

        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        if (players[0].GetComponent<PlayerMotion>().controllerNumber != GetComponent<PlayerMotion>().controllerNumber)
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
        if (otherCamera != null)
            lookTarget = otherCamera.transform.rotation;
        child.transform.LookAt(transform.position + lookTarget * Vector3.back, lookTarget * Vector3.up);
        if (otherPlayer != null)
        {
            angleBetween = Vector3.Angle(transform.forward, otherPlayer.transform.forward);
            dotAngle = Vector3.Dot(transform.forward, otherPlayer.transform.forward);
        }
	    if (angleBetween > 135 && curFrameStart != (int) directions.front)
	    {

	        curFrameStart = (int) directions.front;
	        curFrame = curFrameStart;
	        if (idle)
	        {
	            mat.SetTexture(0, frames[(int) directions.front]);
	        }
	        else
	        {
	            mat.SetTexture(0, idleFrames[(int) directions.front]);
	        }
	    }
	    else if ((angleBetween < 45) && curFrameStart != (int) directions.back)
	    {
	        curFrameStart = (int) directions.back;
	        curFrame = curFrameStart;
	        if (idle)
	        {
	            mat.SetTexture(0, frames[(int) directions.back]);
	        }
	        else
	        {
	            mat.SetTexture(0, idleFrames[(int) directions.back]);
	        }
	    }
	    else if (angleBetween > 45 && angleBetween < 135 && curFrameStart != (int) directions.side)
	    {
	        curFrameStart = (int) directions.side;
	        curFrame = (int) directions.side;
	        if (idle)
	        {
	            mat.SetTexture(0, frames[(int) directions.side]);
	        }
	        else
	        {
	            mat.SetTexture(0, idleFrames[(int) directions.side]);
	        }
            if (otherPlayer != null)
	            rightAngle = Vector3.Angle(otherPlayer.transform.right, transform.forward);
	        if (rightAngle > 90)
	            child.transform.localScale = new Vector3(-1*origScale.x, origScale.y, origScale.z);
	        else
	        {
	            child.transform.localScale = new Vector3(origScale.x, origScale.y, origScale.z);
	        }
	    }

	    if (idle && rigidbody.velocity.magnitude < 0.01f)
	    {
	        mat.SetTexture(0, frames[curFrame]);
	        idle = false;

	    }
        else if (!idle && rigidbody.velocity.magnitude > 0.01f)
	    {
	        mat.SetTexture(0, idleFrames[curFrame]);
	        idle = true;
	    }
	}

    private IEnumerator playFrames()
    {
        while (true)
        {
            yield return new WaitForSeconds(frameDuration);
            if (curFrame == curFrameStart)
            {
                if (idle)
                {
                    curFrame ++;
                    mat.SetTexture(0, frames[curFrame]);
                }
                else
                {
                    curFrame ++;
                    mat.SetTexture(0, idleFrames[curFrame]);
                }
            }
            else
            {
                if (idle)
                {
                    curFrame--;
                    mat.SetTexture(0, frames[curFrame]);
                }
                else
                {
                    curFrame --;
                    mat.SetTexture(0,idleFrames[curFrame]);
                }
            }
        }
    }
}
