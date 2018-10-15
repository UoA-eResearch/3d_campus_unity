using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCamera : MonoBehaviour {

	public bool scaleWithDistance;
	private float scaleMin = 0;
	private float scaleFactor = .1f;
	private float scaleMax = 2;
	
	// Update is called once per frame
	void Update () {
		transform.LookAt(Camera.main.transform);
		if (scaleWithDistance)
		{
			float dist = Vector3.Distance(transform.position, Camera.main.transform.position);
			dist *= scaleFactor;
			dist = Mathf.Clamp(dist, scaleMin, scaleMax);
			transform.localScale = Vector3.one * dist;
		}
	}
}
