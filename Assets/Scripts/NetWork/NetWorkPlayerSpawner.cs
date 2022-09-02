﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class NetWorkPlayerSpawner : MonoBehaviourPunCallbacks
{

	private GameObject spawnedPlayerPrefab;
	public override void OnJoinedRoom()
	{
		base.OnJoinedRoom();
		spawnedPlayerPrefab = PhotonNetwork.Instantiate("PlayerTutorial", transform.position, transform.rotation);
	}

	public override void OnLeftRoom()
	{
		base.OnLeftRoom();
		PhotonNetwork.Destroy(spawnedPlayerPrefab);
	}
}
