﻿using UnityEngine;
using System.Collections;

public class PlayerGrab : MonoBehaviour {
    public float throwForce = 300f;
    public float grabBreakTolerance = 1.3f;
    public float grabEaseValue = 1.5f;
    public float grabAngularEaseValue = 8f;
    public float floorYPosition = 0f;
    public float ceilingYPosition = 10f;
    public float wallMinXPosition = -5f;
    public float wallMaxXPosition = 5f;
    public float wallMinZPosition = -5f;
    public float wallMaxZPosition = 5f;

    Transform grabTransform;
    int controllerNumber;
    [HideInInspector] public bool grabButtonDown = false;
    [HideInInspector] public bool itemIsGrabbed = false;
    GameObject myGrabbedGameObject;
    Camera pcam;
    Collider myGrabbedCollider;
    SpringJoint grabbedSpring;

	// Use this for initialization
	void Start () {
        grabTransform = transform.FindChild("PlayerCam").FindChild("GrabPoint");
        controllerNumber = GetComponent<PlayerMotion>().controllerNumber;
        pcam = GetComponent<PlayerMotion>().pcam;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetAxis("p" + controllerNumber + "_Grab") == 1f)
        {
            if (grabButtonDown == false)
            {
                if (itemIsGrabbed)
                {
                    myGrabbedGameObject.rigidbody.useGravity = true;
                    foreach (Collider col in myGrabbedGameObject.GetComponents<Collider>())
                    {
                        Physics.IgnoreCollision(col, collider, false);
                    }
                    foreach (Collider col in myGrabbedGameObject.GetComponentsInChildren<Collider>())
                    {
                        Physics.IgnoreCollision(col, collider, false);
                    }
                    myGrabbedGameObject.rigidbody.AddForce(throwForce * pcam.transform.forward);
                    myGrabbedGameObject.AddComponent<ObjectScript>();
                    myGrabbedGameObject = null;
                    itemIsGrabbed = false;
                }
                else
                {
                    int layerMask = ~((1 << 8) | (1 << 9) | (1 << 10));
                    RaycastHit hit;
                    if (Physics.Linecast(transform.position, grabTransform.position, out hit, layerMask))
                    {
                        if (hit.rigidbody != null)
                        {
                            myGrabbedCollider = hit.collider;
                            myGrabbedGameObject = hit.transform.gameObject;
                            myGrabbedGameObject.rigidbody.velocity = Vector3.zero;
                            myGrabbedGameObject.rigidbody.angularVelocity = Vector3.zero;
                            itemIsGrabbed = true;
                        }
                    }
                }
                grabButtonDown = true;
            }
        }
        if (Input.GetAxis("p" + controllerNumber + "_Grab") != 1f)
        {
            grabButtonDown = false;
        }
        if (itemIsGrabbed)
        {
			if(myGrabbedGameObject.rigidbody == null)
			{
				myGrabbedGameObject = null;
				itemIsGrabbed = false;
			}
			else
			{
	            myGrabbedGameObject.rigidbody.useGravity = false;
                foreach (Collider col in myGrabbedGameObject.GetComponents<Collider>())
                {
                    Physics.IgnoreCollision(col, collider, true);
                }
                foreach (Collider col in myGrabbedGameObject.GetComponentsInChildren<Collider>())
                {
                    Physics.IgnoreCollision(col, collider, true);
                }

	            //Break grab if object is moving violently
	            if (myGrabbedGameObject.rigidbody.velocity.magnitude > grabBreakTolerance)
	            {
	                myGrabbedGameObject.rigidbody.useGravity = true;
                    foreach (Collider col in myGrabbedGameObject.GetComponents<Collider>())
                    {
                        Physics.IgnoreCollision(col, collider, false);
                    }
                    foreach (Collider col in myGrabbedGameObject.GetComponentsInChildren<Collider>())
                    {
                        Physics.IgnoreCollision(col, collider, false);
                    }
	                myGrabbedGameObject = null;
	                itemIsGrabbed = false;
	            }
	            else
	            {
	                Vector3 positionDiff = (grabTransform.position - myGrabbedCollider.bounds.center);
	                Vector3 angleDiff = new Vector3(0f, pcam.transform.eulerAngles.y - myGrabbedGameObject.transform.eulerAngles.y, 0f);
	                if (angleDiff.x > 180f)
	                    angleDiff.x -= 360f;
	                if (angleDiff.x < -180f)
	                    angleDiff.x += 360f;
	                if (angleDiff.y > 180f)
	                    angleDiff.y -= 360f;
	                if (angleDiff.y < -180f)
	                    angleDiff.y += 360f;
	                if (angleDiff.z > 180f)
	                    angleDiff.z -= 360f;
	                if (angleDiff.z < -180f)
	                    angleDiff.z += 360f;

	                myGrabbedGameObject.rigidbody.velocity = positionDiff / grabEaseValue;
	                myGrabbedGameObject.rigidbody.angularVelocity = angleDiff / grabAngularEaseValue;

	                myGrabbedGameObject.transform.position = myGrabbedGameObject.transform.position + (Time.deltaTime * 60f) * myGrabbedGameObject.rigidbody.velocity;
	                myGrabbedGameObject.transform.eulerAngles = myGrabbedGameObject.transform.eulerAngles + (Time.deltaTime * 60f) * myGrabbedGameObject.rigidbody.angularVelocity;
	                //Prevent clipping

                    float minBoundX = float.MaxValue;
                    float maxBoundX = float.MinValue;
                    float minBoundY = float.MaxValue;
                    float maxBoundY = float.MinValue;
                    float minBoundZ = float.MaxValue;
                    float maxBoundZ = float.MinValue;
	                foreach (Collider col in myGrabbedGameObject.GetComponents<Collider>()) {
                        if (col.bounds.min.x < minBoundX)
                            minBoundX = col.bounds.min.x;
                        if (col.bounds.max.x > maxBoundX)
                            maxBoundX = col.bounds.max.x;
                        if (col.bounds.min.y < minBoundY)
                            minBoundY = col.bounds.min.y;
                        if (col.bounds.max.y > maxBoundY)
                            maxBoundY = col.bounds.max.y;
                        if (col.bounds.min.z < minBoundZ)
                            minBoundZ = col.bounds.min.z;
                        if (col.bounds.max.z > maxBoundZ)
                            maxBoundZ = col.bounds.max.z;
	                }
	                foreach (Collider col in myGrabbedGameObject.GetComponentsInChildren<Collider>()) {
                        if (col.bounds.min.x < minBoundX)
                            minBoundX = col.bounds.min.x;
                        if (col.bounds.max.x > maxBoundX)
                            maxBoundX = col.bounds.max.x;
                        if (col.bounds.min.y < minBoundY)
                            minBoundY = col.bounds.min.y;
                        if (col.bounds.max.y > maxBoundY)
                            maxBoundY = col.bounds.max.y;
                        if (col.bounds.min.z < minBoundZ)
                            minBoundZ = col.bounds.min.z;
                        if (col.bounds.max.z > maxBoundZ)
                            maxBoundZ = col.bounds.max.z;
	                }
                    if (minBoundY + (Time.deltaTime * 60f) * myGrabbedGameObject.rigidbody.velocity.y < floorYPosition)
                    {
                        myGrabbedGameObject.transform.position = new Vector3(myGrabbedGameObject.transform.position.x, floorYPosition + myGrabbedGameObject.transform.position.y - minBoundY, myGrabbedGameObject.transform.position.z);
                        myGrabbedGameObject.rigidbody.velocity = new Vector3(myGrabbedGameObject.rigidbody.velocity.x, 0f, myGrabbedGameObject.rigidbody.velocity.z);
                    }
                    if (maxBoundY + (Time.deltaTime * 60f) * myGrabbedGameObject.rigidbody.velocity.y > ceilingYPosition)
                    {
                        myGrabbedGameObject.transform.position = new Vector3(myGrabbedGameObject.transform.position.x, ceilingYPosition + myGrabbedGameObject.transform.position.y - maxBoundY, myGrabbedGameObject.transform.position.z);
                        myGrabbedGameObject.rigidbody.velocity = new Vector3(myGrabbedGameObject.rigidbody.velocity.x, 0f, myGrabbedGameObject.rigidbody.velocity.z);
                    }
                    if (minBoundX + (Time.deltaTime * 60f) * myGrabbedGameObject.rigidbody.velocity.x < wallMinXPosition)
                    {
                        myGrabbedGameObject.transform.position = new Vector3(wallMinXPosition + myGrabbedGameObject.transform.position.x - minBoundX, myGrabbedGameObject.transform.position.y, myGrabbedGameObject.transform.position.z);
                        myGrabbedGameObject.rigidbody.velocity = new Vector3(0f, myGrabbedGameObject.rigidbody.velocity.y, myGrabbedGameObject.rigidbody.velocity.z);
                    }
                    if (maxBoundX + (Time.deltaTime * 60f) * myGrabbedGameObject.rigidbody.velocity.x > wallMaxXPosition)
                    {
                        myGrabbedGameObject.transform.position = new Vector3(wallMaxXPosition + myGrabbedGameObject.transform.position.x - maxBoundX, myGrabbedGameObject.transform.position.y, myGrabbedGameObject.transform.position.z);
                        myGrabbedGameObject.rigidbody.velocity = new Vector3(0f, myGrabbedGameObject.rigidbody.velocity.y, myGrabbedGameObject.rigidbody.velocity.z);
                    }
                    if (minBoundZ + (Time.deltaTime * 60f) * myGrabbedGameObject.rigidbody.velocity.z < wallMinZPosition)
                    {
                        myGrabbedGameObject.transform.position = new Vector3(myGrabbedGameObject.transform.position.x, myGrabbedGameObject.transform.position.y, wallMinZPosition + myGrabbedGameObject.transform.position.z - minBoundZ);
                        myGrabbedGameObject.rigidbody.velocity = new Vector3(myGrabbedGameObject.rigidbody.velocity.x, myGrabbedGameObject.rigidbody.velocity.y, 0f);
                    }
                    if (maxBoundZ + (Time.deltaTime * 60f) * myGrabbedGameObject.rigidbody.velocity.z > wallMaxZPosition)
                    {
                        myGrabbedGameObject.transform.position = new Vector3(myGrabbedGameObject.transform.position.x, myGrabbedGameObject.transform.position.y, wallMaxZPosition + myGrabbedGameObject.transform.position.z - maxBoundZ);
                        myGrabbedGameObject.rigidbody.velocity = new Vector3(myGrabbedGameObject.rigidbody.velocity.x, myGrabbedGameObject.rigidbody.velocity.y, 0f);
                    }
	            }
			}
        }
	}
}
