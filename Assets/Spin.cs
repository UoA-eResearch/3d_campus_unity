using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour {

	private float speed = 90f;

	void Update()
	{
		transform.Rotate(transform.up, speed * Time.deltaTime);
	}
}
