using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.Events;

public class DestroyObjectsNetWork : MonoBehaviourPun
{

	[SerializeField]
	private float timeToDestroy;
	[SerializeField]
	private bool destroyAfterStart;

	[SerializeField]
	private UnityEvent onDestroy;


	private void Start()
	{
		if (destroyAfterStart)
		{
			Invoke("DestrotObject", timeToDestroy);
			
		}
	}

	public void DestrotObject()
	{
		Debug.Log("Try Destroy");
		this.GetComponent<PhotonView>().RPC("DestroyRPC", RpcTarget.AllBuffered);
	}

	IEnumerator DestoyObjectDelay(GameObject obj,float time)
	{
		yield return new WaitForSeconds(time);
		obj.GetComponent<PhotonView>().RPC("DestroyRPC", RpcTarget.AllBuffered);
		
	}

	[PunRPC]
	public void DestroyRPC()
	{
		Destroy(this.gameObject,timeToDestroy);
		onDestroy?.Invoke();
	}

}
