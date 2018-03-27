using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour {
	public float lookSpeed = 3.5f;
	public float moveSpeed = 1000;
	private Rigidbody _rigidbody;

	private void Start()
	{
		_rigidbody = GetComponent<Rigidbody>();
	}

	void Update()
	{
		if (Input.GetMouseButton(0))
		{
			var lookVector = new Vector3(transform.localEulerAngles.x + Input.GetAxis("Mouse Y") * lookSpeed, transform.localEulerAngles.y - Input.GetAxis("Mouse X") * lookSpeed, 0);
			float x = lookVector.x;
			x = (x > 180) ? x - 360 : x;
			if (x > 80)
			{
				lookVector.x = 80;
			}
			else if (x < -80)
			{
				lookVector.x = -80;
			}
			transform.localEulerAngles = lookVector;
		}
		_rigidbody.AddForce(transform.forward * Input.GetAxis("Mouse ScrollWheel") * moveSpeed);
	}
}
