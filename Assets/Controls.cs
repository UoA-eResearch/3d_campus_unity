using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour {
	public float touchLookSpeed = .1f;
	public float touchMoveSpeed = 1;
	public float mouseLookSpeed = 3.5f;
	public float mouseMoveSpeed = 2000f;
	private Rigidbody _rigidbody;
	private Vector2 touchStartPos;
	private float pinchStartDist;
	private int lastFinger;
	private Vector3 startRotation;

	private void Start()
	{
		_rigidbody = GetComponent<Rigidbody>();
	}

	void Update()
	{
		if (Input.touchSupported)
		{
			// Track a single touch as a direction control.
			if (Input.touchCount == 1)
			{
				Touch touch = Input.GetTouch(0);

				// Handle finger movements based on TouchPhase
				switch (touch.phase)
				{
					//When a touch has first been detected, record the starting position
					case TouchPhase.Began:
						// Record initial touch position.
						touchStartPos = touch.position;
						startRotation = transform.localEulerAngles;
						lastFinger = touch.fingerId;
						break;

					//Determine if the touch is a moving touch
					case TouchPhase.Moved:
						if (touch.fingerId != lastFinger) return;
						// Determine direction by comparing the current touch position with the initial one
						var direction = touch.position - touchStartPos;
						var lookVector = new Vector3(startRotation.x + direction.y * touchLookSpeed, startRotation.y - direction.x * touchLookSpeed, 0);
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
						break;

					case TouchPhase.Ended:
						// Report that the touch has ended when it ends
						break;
				}
			}
			else if (Input.touchCount == 2)
			{
				Touch touch1 = Input.GetTouch(0);
				Touch touch2 = Input.GetTouch(1);
				switch (touch1.phase)
				{
					case TouchPhase.Began:
						pinchStartDist = Vector2.Distance(touch1.position, touch2.position);
						break;
					//Determine if the touch is a moving touch
					case TouchPhase.Moved:
					case TouchPhase.Stationary:
						var pinchDiff = Vector2.Distance(touch1.position, touch2.position);
						var diff = pinchDiff - pinchStartDist;
						_rigidbody.AddForce(transform.forward * diff * touchMoveSpeed);
						break;
				}
			}
		}
		else
		{
			if (Input.GetMouseButton(0))
			{
				var lookVector = new Vector3(transform.localEulerAngles.x + Input.GetAxis("Mouse Y") * mouseLookSpeed, transform.localEulerAngles.y - Input.GetAxis("Mouse X") * mouseLookSpeed, 0);
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
			_rigidbody.AddForce(transform.forward * Input.GetAxis("Mouse ScrollWheel") * mouseMoveSpeed);
		}
	}
}
