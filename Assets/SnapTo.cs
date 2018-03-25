using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapTo : MonoBehaviour {

	private float startTime;
	public GameObject player;

	private void OnTriggerEnter(Collider other)
	{
		startTime = Time.time;
	}

	private void Update()
	{
		var s = Time.time - startTime;
		if (s < 1)
		{
			player.transform.position = Vector3.Lerp(player.transform.position, transform.position, Time.time - startTime);
		}
	}
}
