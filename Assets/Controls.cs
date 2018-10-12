using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.EventSystems;

public class Controls : MonoBehaviour
{
	public float touchLookSpeed = .1f;
	public float touchMoveSpeed = 1;
	public float mouseLookSpeed = 3.5f;
	public float mouseMoveSpeed = 2000f;
	public float clickTime = .2f;
	private float clickStartTime;
	private Rigidbody _rigidbody;
	private Vector2 touchStartPos;
	private float pinchStartDist;
	private int lastFinger;
	private Vector3 startRotation;
	private Vector3 startPosition;
	public GameObject targetGO;
	private float teleportStartTime;
	Quaternion gotoStartRotation;
	public float teleportTime = 2f;
	private bool usingUI = false;
	public Transform compass;
	private Vector3 orbitPoint;

	[DllImport("__Internal")]
	private static extern void ShowYoutube(string str);

	private void Start()
	{
		_rigidbody = GetComponent<Rigidbody>();
		Debug.Log("Touch supported = " + Input.touchSupported);
		Debug.Log("Platform: " + Application.platform);
	}

	public void GoTo(GameObject go)
	{
		if (go != null)
		{
			targetGO = go;
		} else
		{
			targetGO = gameObject;
		}
		teleportStartTime = Time.time;
		startPosition = transform.position;
		gotoStartRotation = transform.rotation;
	}

	void Update()
	{
		if (Application.platform == RuntimePlatform.Android)
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
						clickStartTime = Time.time;
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
						if (touch.fingerId == lastFinger && Time.time - clickStartTime < clickTime)
						{
							Ray ray = Camera.main.ScreenPointToRay(touch.position);
							RaycastHit hit;
							if (Physics.Raycast(ray, out hit))
							{
								if (hit.collider.tag == "photosphere")
								{
									targetGO = hit.collider.gameObject;
									teleportStartTime = Time.time;
									startPosition = transform.position;
								}
							}
						}
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
			if (Input.GetMouseButtonDown(0))
			{
				clickStartTime = Time.time;
				usingUI = EventSystem.current.IsPointerOverGameObject();
				if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.LeftControl))
				{
					Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
					RaycastHit hit;
					if (Physics.Raycast(ray, out hit))
					{
						orbitPoint = hit.point;
					}
				}
			}
			else if (Input.GetMouseButton(0) && !usingUI)
			{
				if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.LeftControl))
				{
					transform.LookAt(orbitPoint);
					_rigidbody.AddRelativeForce(Input.GetAxis("Mouse X") * mouseMoveSpeed / 4, 0, Input.GetAxis("Mouse Y") * mouseMoveSpeed / 4);
				}
				else
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
			}
			else if (Input.GetMouseButtonUp(0) && Time.time - clickStartTime < clickTime)
			{
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				RaycastHit hit;
				if (Physics.Raycast(ray, out hit))
				{
					if (hit.collider.tag == "photosphere")
					{
						targetGO = hit.collider.gameObject;
						teleportStartTime = Time.time;
						startPosition = transform.position;
					}
					if (hit.collider.tag == "disc") {
						ShowYoutube(hit.collider.name);
					}
				}
			}
			else if (Input.GetMouseButton(1))
			{
				_rigidbody.AddRelativeForce(Input.GetAxis("Mouse X") * mouseMoveSpeed / 4, 0, Input.GetAxis("Mouse Y") * mouseMoveSpeed / 4);
			}
			_rigidbody.AddForce(transform.forward * Input.GetAxis("Mouse ScrollWheel") * mouseMoveSpeed);
		}
		if (targetGO != null)
		{
			var t = (Time.time - teleportStartTime) / teleportTime;
			if (targetGO == gameObject)
			{
				var north = Quaternion.Euler(transform.rotation.eulerAngles.x, -180, transform.rotation.eulerAngles.z);
				transform.rotation = Quaternion.Lerp(gotoStartRotation, north, t);
				var angle = Quaternion.Angle(transform.rotation, north);
				if (angle < .01)
				{
					targetGO = null;
				}
			}
			else
			{
				transform.position = Vector3.Lerp(startPosition, targetGO.transform.position, t);
				transform.rotation = Quaternion.Lerp(gotoStartRotation, targetGO.transform.rotation, t);
				var d = Vector3.Distance(transform.position, targetGO.transform.position);
				if (d < .01)
				{
					targetGO = null;
				}
			}
		}
		compass.rotation = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.y - 180);
	}
}
