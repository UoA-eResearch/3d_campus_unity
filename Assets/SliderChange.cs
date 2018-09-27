using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderChange : MonoBehaviour {

	public GameObject targetGo;
	private Material material;

	public void Start() {
		material = targetGo.GetComponent<Renderer>().material;
	}
 
	public void OnValueChange(float value) {
		Debug.Log(value);
		material.color = new Color(1, 1, 1, value);
	}
}
