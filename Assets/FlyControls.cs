using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class FlyControls : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		float x = CrossPlatformInputManager.GetAxis("Horizontal");
		float y = CrossPlatformInputManager.GetAxis("Vertical");
		float rx = CrossPlatformInputManager.GetAxis("Mouse X");
		float ry = CrossPlatformInputManager.GetAxis("Mouse Y");
		Debug.Log(rx);
		transform.Translate(x, y, 0);
		transform.Rotate(rx, ry, 0);
	}
}
