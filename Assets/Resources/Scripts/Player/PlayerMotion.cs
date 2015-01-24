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
	float udRot;
	float rlRot;
	bool grounded = false;
	bool keyjumping = false;
	bool contjumping = false;

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
		if(Input.GetAxis("Mouse X")!=0){
			drlRot = Input.GetAxis("Mouse X"); 
		}else if(Input.GetAxis("Horizontal2")!=0){
			drlRot = Input.GetAxis("Horizontal2");
		}

		float dudRot = 0; //up/down rotation
		if(Input.GetAxis("Mouse Y")!=0){
			dudRot = -Input.GetAxis("Mouse Y");
		}else if(Input.GetAxis("Vertical2")!=0){
			dudRot = Input.GetAxis("Vertical2");
		}

		udRot += dudRot * up_down_look_speed;
		rlRot += drlRot * right_left_look_speed;
		udRot = Mathf.Clamp(udRot,-look_bounds,look_bounds);
		pcam.transform.rotation = Quaternion.Euler(udRot,rlRot,0);


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
		
		if(Input.GetKey(KeyCode.W) || Input.GetAxis("Vertical")>0){
			Vector3 mvec = pcam.transform.forward;
			mvec.y = 0;
			mvec.Normalize();
			rigidbody.AddForce(mvec * forward_move_speed);
		}
		
		if(Input.GetKey(KeyCode.A) || Input.GetAxis("Horizontal")<0){
			Vector3 mvec = -pcam.transform.right;
			mvec.y = 0;
			mvec.Normalize();
			rigidbody.AddForce(mvec * strafe_move_speed);
		}
		
		if(Input.GetKey(KeyCode.S) || Input.GetAxis("Vertical")<0){
			Vector3 mvec = -pcam.transform.forward;
			mvec.y = 0;
			mvec.Normalize();
			rigidbody.AddForce(mvec * backward_move_speed);
		}
		
		if(Input.GetKey(KeyCode.D) || Input.GetAxis("Horizontal")>0){
			Vector3 mvec = pcam.transform.right;
			mvec.y = 0;
			mvec.Normalize();
			rigidbody.AddForce(mvec * strafe_move_speed);
		}
		
		//jumping
		if(Input.GetKey(KeyCode.Space) == false){
			keyjumping=false;
		}
		if(Input.GetKey(KeyCode.JoystickButton0) == false){
			contjumping=false;
		}
		
		if(grounded){
			if((Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.JoystickButton0)) && keyjumping == false && contjumping == false){
				rigidbody.AddForce(Vector3.up * jump_force);
				if(Input.GetKey(KeyCode.Space)){
					keyjumping=true;
				}else{
					contjumping=true;
				}
			}
		}
		}

}
