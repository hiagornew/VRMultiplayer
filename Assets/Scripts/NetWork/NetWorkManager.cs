using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

[System.Serializable]
public class DefaultRoom
{
	public string name;
	public int sceneIndex;
	public int maxPlayer;
}

public class NetWorkManager : MonoBehaviourPunCallbacks
{
	[SerializeField]
	private List<DefaultRoom> rooms;
	[SerializeField]
	private string nameRoom;

	[SerializeField]
	private GameObject roomUI;
    // Start is called before the first frame update
    void Start()
    {
        //ConnectToServer();
    }

	

	public void ConnectToServer()
	{
        PhotonNetwork.ConnectUsingSettings();
        Debug.Log("Try Connect to Server");
	}

	public override void OnConnectedToMaster()
	{
		Debug.Log("Connected To Server");
		base.OnConnectedToMaster();
		PhotonNetwork.JoinLobby();
		
	}

	public override void OnJoinedLobby()
	{
		base.OnJoinedLobby();
		roomUI.SetActive(true);
	}

	public void initializeRoom(int defaultroomIndex)
	{
		DefaultRoom roomSettings = rooms[defaultroomIndex];
		//LOAD THE SCENES
		PhotonNetwork.LoadLevel(roomSettings.sceneIndex);
		//CREATE THE ROOM
		RoomOptions roomOptions = new RoomOptions();
		roomOptions.MaxPlayers = (byte)roomSettings.maxPlayer;
		roomOptions.IsVisible = true;
		roomOptions.IsOpen = true;

		PhotonNetwork.JoinOrCreateRoom(roomSettings.name, roomOptions, TypedLobby.Default);
	}

	public override void OnJoinedRoom()
	{
		Debug.Log("Joined a Room");
		base.OnJoinedRoom();
	}

	public override void OnPlayerEnteredRoom(Player newPlayer)
	{
		Debug.Log("A new Plaer joined the room");
		base.OnPlayerEnteredRoom(newPlayer);
	}


}
