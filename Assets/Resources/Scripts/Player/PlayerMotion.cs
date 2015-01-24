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

	// Use this for initialization
	void Start () {

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


		//fov
		pcam.fieldOfView = 90 + rigidbody.velocity.magnitude;
	}

	void FixedUpdate(){
		
		//movement
		if (Input.GetKey(KeyCode.Escape)){ //temp for testing
			Screen.lockCursor = false;
		}else{
			Screen.lockCursor = true;
		}
		
		if(Input.GetAxis("p" + controllerNumber + "_MoveY") > 0){
			Vector3 mvec = pcam.transform.forward;
			mvec.y = 0;
			mvec.Normalize();
			rigidbody.AddForce(mvec * forward_move_speed);
		}

        if (Input.GetAxis("p" + controllerNumber + "_MoveX") < 0)
        {
			Vector3 mvec = -pcam.transform.right;
			mvec.y = 0;
			mvec.Normalize();
			rigidbody.AddForce(mvec * strafe_move_speed);
		}

        if (Input.GetAxis("p" + controllerNumber + "_MoveY") < 0)
        {
			Vector3 mvec = -pcam.transform.forward;
			mvec.y = 0;
			mvec.Normalize();
			rigidbody.AddForce(mvec * backward_move_speed);
		}

        if (Input.GetAxis("p" + controllerNumber + "_MoveX") > 0)
        {
			Vector3 mvec = pcam.transform.right;
			mvec.y = 0;
			mvec.Normalize();
			rigidbody.AddForce(mvec * strafe_move_speed);
		}
		
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

}
