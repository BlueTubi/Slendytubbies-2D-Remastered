using System.Collections;
using Photon;
using UnityEngine;

public class Connect1C : Photon.MonoBehaviour
{
	private bool receivedRoomList;

	private void Awake()
	{
		PhotonNetwork.ConnectUsingSettings("1.0");
	}

	private void OnGUI()
	{
		if (!PhotonNetwork.connected && !PhotonNetwork.connecting)
		{
			GUILayout.Label("Connection status: " + PhotonNetwork.connectionStateDetailed);
			GUILayout.BeginVertical();
			if (GUILayout.Button("Connect"))
			{
				PhotonNetwork.ConnectUsingSettings("1.0");
			}
			GUILayout.EndVertical();
			return;
		}
		GUILayout.Label("Connection status: " + PhotonNetwork.connectionStateDetailed);
		if (PhotonNetwork.room != null)
		{
			GUILayout.Label("Room: " + PhotonNetwork.room.name);
			GUILayout.Label("Players: " + PhotonNetwork.room.playerCount + "/" + PhotonNetwork.room.maxPlayers);
		}
		else
		{
			GUILayout.Label("Not inside any room");
		}
		GUILayout.Label("Ping to server: " + PhotonNetwork.GetPing());
	}

	private void OnConnectedToPhoton()
	{
		StartCoroutine(JoinOrCreateRoom());
	}

	private void OnDisconnectedFromPhoton()
	{
		receivedRoomList = false;
	}

	private IEnumerator JoinOrCreateRoom()
	{
		float timeOut = Time.time + 2f;
		while (Time.time < timeOut && !receivedRoomList)
		{
			yield return 0;
		}
		if (PhotonNetwork.room == null)
		{
			string roomName = "TestRoom" + Application.loadedLevelName;
			PhotonNetwork.CreateRoom(roomName, new RoomOptions
			{
				maxPlayers = 4
			}, null);
		}
	}

	private void OnReceivedRoomListUpdate()
	{
		Debug.Log("We received a room list update, total rooms now: " + PhotonNetwork.GetRoomList().Length);
		string text = "TestRoom" + Application.loadedLevelName;
		RoomInfo[] roomList = PhotonNetwork.GetRoomList();
		foreach (RoomInfo roomInfo in roomList)
		{
			if (roomInfo.name == text)
			{
				PhotonNetwork.JoinRoom(roomInfo.name);
				break;
			}
		}
		receivedRoomList = true;
	}
}
