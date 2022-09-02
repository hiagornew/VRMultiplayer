using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class DestroyObjectsNetWork : MonoBehaviourPun
{

	public static DestroyObjectsNetWork instance;

	private void Awake()
	{
		if(instance == null)
		{
			instance = this;
		}
		else if(instance != this)
		{
			Destroy(this);
		}
		DontDestroyOnLoad(instance);
	}

	public void DestrotObject(GameObject obj, float time = 0)
	{
		StartCoroutine(DestoyObjectDelay(obj, time));
	}

	IEnumerator DestoyObjectDelay(GameObject obj,float time)
	{
		yield return new WaitForSeconds(time);
		obj.GetComponent<PhotonView>().RPC("DestroyRPC", RpcTarget.AllBuffered, obj);
		
	}

	[PunRPC]
	public void DestroyRPC(GameObject obj)
	{
		Destroy(obj);
	}

}
