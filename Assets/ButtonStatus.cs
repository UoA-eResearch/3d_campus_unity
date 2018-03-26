using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonStatus : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

	public bool isHeld;

	public void OnPointerDown(PointerEventData e)
	{
		isHeld = true;
		Debug.Log("button down");
	}

	public void OnPointerUp(PointerEventData e)
	{
		isHeld = false;
		Debug.Log("button up");
	}
}
