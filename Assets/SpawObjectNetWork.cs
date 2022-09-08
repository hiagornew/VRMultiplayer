using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawObjectNetWork : MonoBehaviourPunCallbacks
{

	private GameObject spawnedObjectPrefab;
	[SerializeField]
	private GameObject prefabObject;


	private void Start()
	{
		Spaw();
	}

	public void Spaw()
	{
		spawnedObjectPrefab = PhotonNetwork.Instantiate(prefabObject.name, transform.position, transform.rotation);
	}
	
}
