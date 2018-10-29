using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCamera : MonoBehaviour {

	public bool faceCamera = true;
	public bool scaleWithDistance;
	public bool scalexz;
	private float scaleMin = 0;
	private float scaleFactor = .04f;
	private float scaleMax = 1;
	
	// Update is called once per frame
	void Update () {
		if (faceCamera)
		{
			transform.LookAt(Camera.main.transform);
		}
		if (scaleWithDistance)
		{
			float dist = Vector3.Distance(transform.position, Camera.main.transform.position);
			dist *= scaleFactor;
			dist = Mathf.Clamp(dist, scaleMin, scaleMax);
			var scale = Vector3.one * dist;
			if (scalexz)
			{
				scale.y = transform.localScale.y;
			}
			transform.localScale = scale;
		}
	}
}
