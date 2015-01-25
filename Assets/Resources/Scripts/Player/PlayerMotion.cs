using UnityEngine;
using System.Collections;

public class PlayerMotion : MonoBehaviour {
	public Camera pcam; //player camera
	public float forward_move_speed = 10; //forward move speed
	public float backward_move_speed = 10; //backward move speed
	public float strafe_move_speed = 10; //strafe move speed
	public float right_left_look_speed = 5; //right left look speed 
	public float up_down_look_speed = 5; //up down look speed
	public float look_bounds = 60; //look bounds
	public float jump_force = 100; //jump force
    public int controllerNumber;
	float udRot;
	float rlRot;
	bool grounded = false;
	bool jumping = false;
    private bool onOtherPlayer;

	// Use this for initialization
	void Start ()
	{
	    udRot = transform.localEulerAngles.z;
	    rlRot = transform.eulerAngles.y;
	}

	public void GroundEnter() {
		grounded = true;
	}
	public void GroundExit() {
		grounded = false;
	}
	public void GroundStay() {
		grounded = true;
	}

	// Update is called once per frame
	void Update () {

        Debug.Log(onOtherPlayer);


		//camera rotation
		float drlRot = 0; //right/left rotation
        drlRot = Input.GetAxis("p" + controllerNumber + "_LookX"); 

		float dudRot = 0; //up/down rotation
        dudRot = Input.GetAxis("p" + controllerNumber + "_LookY");

		udRot += dudRot * up_down_look_speed;
		rlRot += drlRot * right_left_look_speed;
		udRot = Mathf.Clamp(udRot,-look_bounds,look_bounds);
        transform.rotation = Quaternion.Euler(0, rlRot, 0);
        pcam.transform.rotation = Quaternion.Euler(udRot, rlRot, 0);
	}

	void FixedUpdate(){
		//movement
		if (Input.GetKey(KeyCode.Escape)){ //temp for testing
			Screen.lockCursor = false;
		}else{
			Screen.lockCursor = true;
		}

        if (Input.GetAxis("p" + controllerNumber + "_MoveY") > 0)
        {
            rigidbody.AddForce(Input.GetAxis("p" + controllerNumber + "_MoveY") * new Vector3(pcam.transform.forward.x, 0f, pcam.transform.forward.z).normalized * forward_move_speed);
        }
        else if (Input.GetAxis("p" + controllerNumber + "_MoveY") < 0)
        {
            rigidbody.AddForce(Input.GetAxis("p" + controllerNumber + "_MoveY") * new Vector3(pcam.transform.forward.x, 0f, pcam.transform.forward.z).normalized * backward_move_speed);
        }
        rigidbody.AddForce(Input.GetAxis("p" + controllerNumber + "_MoveX") * new Vector3(pcam.transform.right.x, 0f, pcam.transform.right.z).normalized * strafe_move_speed);

		
		//jumping
        if (Input.GetAxis("p" + controllerNumber + "_Jump") != 1.0f)
        {
			jumping=false;
		}

		
		if(grounded){
            if (Input.GetAxis("p" + controllerNumber + "_Jump") == 1.0f && jumping == false)
            {
				rigidbody.AddForce(Vector3.up * jump_force);
                jumping = true;
			}
		}
	}

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Player")
        {
            if(col.transform.position.y<transform.position.y-0.5)
                onOtherPlayer = true;
            else
            {
                onOtherPlayer = false;
            }
        }
    }

    void OnCollisionExit(Collision col)
    {
        if (col.gameObject.tag == "Player")
        {
            onOtherPlayer = false;
        }
    }

    public bool getOnOtherPlayer()
    {
        return onOtherPlayer;
    }

}
