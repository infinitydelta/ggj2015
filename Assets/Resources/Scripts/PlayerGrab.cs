﻿using UnityEngine;
using System.Collections;

public class PlayerGrab : MonoBehaviour {
    public float throwForce = 300f;
    public float grabBreakTolerance = 2.5f;
    public float grabEaseValue = 1.5f;
    public float grabAngularEaseValue = 8f;
    public float floorYPosition = -2f;

    Transform grabTransform;
    int controllerNumber;
    bool grabButtonDown = false;
    bool itemIsGrabbed = false;
    GameObject myGrabbedGameObject;
    Camera pcam;

	// Use this for initialization
	void Start () {
        grabTransform = transform.FindChild("PlayerCam").FindChild("GrabPoint");
        controllerNumber = GetComponent<PlayerMotion>().controllerNumber;
        pcam = GetComponent<PlayerMotion>().pcam;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetAxis("p" + controllerNumber + "_Grab") != 0)
        {
            if (grabButtonDown == false)
            {
                if (itemIsGrabbed)
                {
                    myGrabbedGameObject.rigidbody.useGravity = true;
                    Physics.IgnoreCollision(myGrabbedGameObject.collider, collider, false);
                    myGrabbedGameObject.rigidbody.AddForce(throwForce * pcam.transform.forward);
                    myGrabbedGameObject = null;
                    itemIsGrabbed = false;
                }
                else
                {
                    int layerfilter = 1 << 8;
                    layerfilter = ~layerfilter;
                    RaycastHit hit;
                    if (Physics.Linecast(transform.position, grabTransform.position, out hit, layerfilter))
                    {
                        if (hit.transform.GetComponent<Rigidbody>())
                        {
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
        if (Input.GetAxis("p" + controllerNumber + "_Grab") == 0)
        {
            grabButtonDown = false;
        }
        if (itemIsGrabbed)
        {
            myGrabbedGameObject.rigidbody.useGravity = false;
            Physics.IgnoreCollision(myGrabbedGameObject.collider, collider, true);

            //Break grab if object is moving violently
            if (myGrabbedGameObject.rigidbody.velocity.magnitude > grabBreakTolerance)
            {
                myGrabbedGameObject.rigidbody.useGravity = true;
                Physics.IgnoreCollision(myGrabbedGameObject.collider, collider, false);
                myGrabbedGameObject = null;
                itemIsGrabbed = false;
            }


            //Prevent clipping
            Vector3 positionDiff = (grabTransform.position - myGrabbedGameObject.transform.position);
            myGrabbedGameObject.transform.position = myGrabbedGameObject.transform.position + positionDiff / grabEaseValue;
            if (myGrabbedGameObject.collider.bounds.min.y + positionDiff.y / grabEaseValue < floorYPosition)
            {
                myGrabbedGameObject.transform.position = new Vector3(myGrabbedGameObject.transform.position.x, floorYPosition + myGrabbedGameObject.transform.position.y - myGrabbedGameObject.collider.bounds.min.y, myGrabbedGameObject.transform.position.z);
            }

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
            myGrabbedGameObject.transform.eulerAngles = myGrabbedGameObject.transform.eulerAngles + angleDiff / grabAngularEaseValue;
            myGrabbedGameObject.rigidbody.velocity = Vector3.zero;
            myGrabbedGameObject.rigidbody.angularVelocity = Vector3.zero;
        }
	}
}
