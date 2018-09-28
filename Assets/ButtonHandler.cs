using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHandler : MonoBehaviour {

	private Controls controls;
	public GameObject target;

	public void Start() {
		controls = Camera.main.GetComponent<Controls>();
	}

	public void OnClick() {
		Debug.Log("click");
		controls.GoTo(target);
	}
}
