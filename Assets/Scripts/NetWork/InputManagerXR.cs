using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManagerXR : MonoBehaviour
{

	private XRIDefaultInputActions inputaction;

	[SerializeField]
	private bool trigger;

	

		
	// Start is called before the first frame update
	void Start()
    {
		
	}

	private void OnEnable()
	{
		inputaction = new XRIDefaultInputActions();
	
		inputaction.XRILeftHandInteraction.Activate.performed += TriggerLeft;
		inputaction.XRILeftHandInteraction.Activate.canceled += TriggerLeft;
		inputaction.Enable();

	}




	private void TriggerLeft(InputAction.CallbackContext callback)
	{
		trigger  = !trigger;
	}

	public bool GetTriggerLeft()
	{
		return trigger;
	}
	
}
