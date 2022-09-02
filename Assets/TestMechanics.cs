using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMechanics : MonoBehaviour
{
	[SerializeField]
	private Rigidbody rb;

	private Vector3 _correctedForce;
	[SerializeField]
	private Vector3 force;
	[SerializeField]
	private float forceMultiplier;

	[SerializeField]
	private ForceMode forceMode;

	[SerializeField]
	private bool rotate;

	[SerializeField]
	private bool frente;
	[SerializeField]
	private bool cima;

	private Vector3 auxUp;

	private void Start()
	{
		rb = GetComponent<Rigidbody>();
		_correctedForce = transform.rotation * force;
		auxUp = this.transform.up.normalized;
		//PrintDebug();
	}

	private void FixedUpdate() {


		if (rotate)
		{
			Quaternion deltaRotation = Quaternion.Euler(force * rb.velocity.magnitude * Time.fixedDeltaTime);
			rb.MoveRotation(rb.rotation * deltaRotation);
		}
		if(cima)
		{
			rb.AddForce(force * forceMultiplier, forceMode);
		}
		if (frente)
		{
			rb.AddForce(auxUp * forceMultiplier, forceMode);
		}
		
		

	}

	public void PrintDebug()
	{
		Debug.Log("VECTOR UP: " + this.transform.up);
		Debug.Log("VECTOR UP: " + this.transform.up.normalized);
	}
	
}
