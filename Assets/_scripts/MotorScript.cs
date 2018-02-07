using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotorScript : MonoBehaviour {
	float speed;
	float accelerationAmount;
	float deaccelerationAmount;
	float steerSensivity;
	float leanModifier;
	float leanReturnSpeed;
	float leanDeadzone1;
	float leanDeadzone2;
	bool leanResetY;
	bool leanResetZ;
	// Use this for initialization
	void Start () {
		speed = 0f;
		accelerationAmount = 1f;
		deaccelerationAmount = accelerationAmount * 0.5f;
		steerSensivity = 40f;
		leanModifier = 0.1f;
		leanReturnSpeed = steerSensivity * 2f;
		leanDeadzone1 = 359.9f;
		leanDeadzone2 = 0.1f;
		leanResetY = false;
		leanResetZ = false;
	}

	// Update is called once per frame
	void Update () {
		MoveMotor ();
		RotateMotor ();
		Debug.Log (transform.rotation.eulerAngles.z);
	}


	void MoveMotor(){
		if (Input.GetKey (KeyCode.W)) {
			speed = speed + accelerationAmount * Time.deltaTime;
		} else {
			speed = speed - deaccelerationAmount * Time.deltaTime;
		}

		if (speed < 0f) {
			speed = 0f;
		}
		transform.Translate (0f, 0f, speed);
	}

	void RotateMotor(){
		leanResetY = true;
		leanResetZ = true;
		if (Input.GetKey (KeyCode.A)) {
			if (transform.eulerAngles.y > 270f && transform.eulerAngles.y < 360f || transform.eulerAngles.y == 0f) {
				transform.eulerAngles = new Vector3 (0f, transform.eulerAngles.y - steerSensivity * Time.deltaTime * leanModifier, transform.eulerAngles.z);
				leanResetY = false;
			}
			if (transform.eulerAngles.z >= 0f && transform.eulerAngles.z < 45f) {
				transform.eulerAngles = new Vector3 (0f, transform.eulerAngles.y, transform.eulerAngles.z + steerSensivity * Time.deltaTime);
				leanResetZ = false;
			}
		} else if (Input.GetKey (KeyCode.D)) {
			if (transform.eulerAngles.y >= 0f && transform.eulerAngles.y < 90f) {
				transform.eulerAngles = new Vector3 (0f, transform.eulerAngles.y + steerSensivity * Time.deltaTime * leanModifier, transform.eulerAngles.z);
				leanResetY = false;
			}
			if (transform.eulerAngles.z > 315f && transform.eulerAngles.z < 360f || transform.eulerAngles.y >= 0f && transform.eulerAngles.y < 1f) {
				transform.eulerAngles = new Vector3 (0f, transform.eulerAngles.y, transform.eulerAngles.z - steerSensivity * Time.deltaTime);
				leanResetZ = false;
			}
		}

		if (transform.localEulerAngles.y > 200f && leanResetY == true) {
			transform.eulerAngles = new Vector3 (0f, transform.eulerAngles.y + leanReturnSpeed * Time.deltaTime * leanModifier, transform.eulerAngles.z);

		} else if (transform.localEulerAngles.y > 0f && transform.localEulerAngles.y < 150f && leanResetY == true) {
			transform.eulerAngles = new Vector3 (0f, transform.eulerAngles.y - leanReturnSpeed * Time.deltaTime * leanModifier, transform.eulerAngles.z - leanReturnSpeed * Time.deltaTime);
	
		}

		if (transform.localEulerAngles.z > 250f && leanResetZ == true) {
			transform.eulerAngles = new Vector3 (0f, transform.eulerAngles.y, transform.eulerAngles.z + leanReturnSpeed * Time.deltaTime);
		} else if (transform.localEulerAngles.z > 1f && transform.localEulerAngles.z < 100f && leanResetZ == true) {
			transform.eulerAngles = new Vector3 (0f, transform.eulerAngles.y, transform.eulerAngles.z - leanReturnSpeed * Time.deltaTime);
		}

		if (transform.eulerAngles.z < 0f ) {
			transform.eulerAngles = new Vector3(0f, transform.eulerAngles.y,0f);
		}

		if (transform.eulerAngles.y < 0f) {
			transform.eulerAngles = new Vector3(0f, 0f, transform.eulerAngles.z);
		}
	}
}

