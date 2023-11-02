using System.Collections;
using UnityEngine;

public class Connect1B : MonoBehaviour
{
	private string roomName = "myRoom";

	private Vector2 scrollPos = Vector2.zero;

	private bool connectFailed;

	private void Awake()
	{
		if (!PhotonNetwork.connected)
		{
			PhotonNetwork.ConnectUsingSettings("1.0");
		}
		PhotonNetwork.playerName = "Guest" + Random.Range(1, 9999);
	}

	private void OnGUI()
	{
		if (!PhotonNetwork.connected)
		{
			GUI_Disconnected();
		}
		else if (PhotonNetwork.room != null)
		{
			GUI_Connected_Room();
		}
		else
		{
			GUI_Connected_Lobby();
		}
	}

	private void GUI_Disconnected()
	{
		if (PhotonNetwork.connecting)
		{
			GUILayout.Label("Connecting...");
		}
		else
		{
			GUILayout.Label(string.Concat("Not connected. Check console output. (", PhotonNetwork.connectionState, ")"));
		}
		if (connectFailed)
		{
			GUILayout.Label("Connection failed. Check setup and use Setup Wizard to fix configuration.");
			GUILayout.Label(string.Format("Server: {0}", PhotonNetwork.PhotonServerSettings.ServerAddress));
			GUILayout.Label(string.Format("AppId: {0}", PhotonNetwork.PhotonServerSettings.AppID));
			if (GUILayout.Button("Try Again", GUILayout.Width(100f)))
			{
				connectFailed = false;
				PhotonNetwork.ConnectUsingSettings("1.0");
			}
		}
	}

	private void GUI_Connected_Lobby()
	{
		GUILayout.BeginArea(new Rect((Screen.width - 400) / 2, (Screen.height - 300) / 2, 400f, 300f));
		GUILayout.Label("Main Menu");
		GUILayout.BeginHorizontal();
		GUILayout.Label("Player name:", GUILayout.Width(150f));
		PhotonNetwork.playerName = GUILayout.TextField(PhotonNetwork.playerName);
		if (GUI.changed)
		{
			PlayerPrefs.SetString("playerName" + Application.platform, PhotonNetwork.playerName);
		}
		GUILayout.EndHorizontal();
		GUILayout.Space(15f);
		GUILayout.BeginHorizontal();
		GUILayout.Label("JOIN ROOM:", GUILayout.Width(150f));
		roomName = GUILayout.TextField(roomName);
		if (GUILayout.Button("GO"))
		{
			PhotonNetwork.JoinRoom(roomName);
		}
		GUILayout.EndHorizontal();
		GUILayout.BeginHorizontal();
		GUILayout.Label("CREATE ROOM:", GUILayout.Width(150f));
		roomName = GUILayout.TextField(roomName);
		if (GUILayout.Button("GO"))
		{
			PhotonNetwork.CreateRoom(roomName, new RoomOptions
			{
				maxPlayers = 10
			}, null);
		}
		GUILayout.EndHorizontal();
		GUILayout.BeginHorizontal();
		GUILayout.Label("JOIN RANDOM ROOM:", GUILayout.Width(150f));
		if (PhotonNetwork.GetRoomList().Length == 0)
		{
			GUILayout.Label("..no games available...");
		}
		else if (GUILayout.Button("GO"))
		{
			PhotonNetwork.JoinRandomRoom();
		}
		GUILayout.EndHorizontal();
		GUILayout.Space(30f);
		GUILayout.Label("ROOM LISTING:");
		if (PhotonNetwork.GetRoomList().Length == 0)
		{
			GUILayout.Label("..no games available..");
		}
		else
		{
			scrollPos = GUILayout.BeginScrollView(scrollPos);
			RoomInfo[] roomList = PhotonNetwork.GetRoomList();
			foreach (RoomInfo roomInfo in roomList)
			{
				GUILayout.BeginHorizontal();
				GUILayout.Label(roomInfo.name + " " + roomInfo.playerCount + "/" + roomInfo.maxPlayers);
				if (GUILayout.Button("JOIN"))
				{
					PhotonNetwork.JoinRoom(roomInfo.name);
				}
				GUILayout.EndHorizontal();
			}
			GUILayout.EndScrollView();
		}
		GUILayout.EndArea();
	}

	private void GUI_Connected_Room()
	{
		GUILayout.Label("We are connected to room: " + PhotonNetwork.room);
		GUILayout.Label("Players: ");
		PhotonPlayer[] playerList = PhotonNetwork.playerList;
		foreach (PhotonPlayer photonPlayer in playerList)
		{
			GUILayout.Label("ID: " + photonPlayer.ID + " Name: " + photonPlayer.name);
		}
		if (GUILayout.Button("Leave room"))
		{
			PhotonNetwork.LeaveRoom();
		}
	}

	private void OnJoinedRoom()
	{
		Debug.Log("We have joined a room.");
		StartCoroutine(MoveToGameScene());
	}

	private void OnCreatedRoom()
	{
		Debug.Log("We have created a room.");
	}

	private void OnFailedToConnectToPhoton(object parameters)
	{
		connectFailed = true;
		Debug.Log("OnFailedToConnectToPhoton. StatusCode: " + parameters);
	}

	private IEnumerator MoveToGameScene()
	{
		while (PhotonNetwork.room == null)
		{
			yield return 0;
		}
		Debug.LogWarning("Normally we would load the game scene right now.");
	}

	private void OnLeftRoom()
	{
		Debug.Log("This client has left a game room.");
	}

	private void OnPhotonCreateRoomFailed()
	{
		Debug.Log("A CreateRoom call failed, most likely the room name is already in use.");
	}

	private void OnPhotonJoinRoomFailed()
	{
		Debug.Log("A JoinRoom call failed, most likely the room name does not exist or is full.");
	}

	private void OnPhotonRandomJoinFailed()
	{
		Debug.Log("A JoinRandom room call failed, most likely there are no rooms available.");
	}

	private void OnJoinedLobby()
	{
		Debug.Log("We joined the lobby.");
	}

	private void OnLeftLobby()
	{
		Debug.Log("We left the lobby.");
	}

	private void OnReceivedRoomList()
	{
		Debug.Log("We received a new room list, total rooms: " + PhotonNetwork.GetRoomList().Length);
	}

	private void OnReceivedRoomListUpdate()
	{
		Debug.Log("We received a room list update, total rooms now: " + PhotonNetwork.GetRoomList().Length);
	}
}
