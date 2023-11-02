using UnityEngine;

public class WorkerMenu : MonoBehaviour
{
	private string roomName = "myRoom";

	private Vector2 scrollPos = Vector2.zero;

	private bool connectFailed;

	public static readonly string SceneNameMenu = "Lobby";

	public static readonly string SceneNameGame = "Teletubby Cave (Medium)";

	public bool isOffline;

	public void Awake()
	{
		PhotonNetwork.offlineMode = isOffline;
		PhotonNetwork.automaticallySyncScene = true;
		if (PhotonNetwork.connectionStateDetailed == PeerState.PeerCreated)
		{
			PhotonNetwork.ConnectUsingSettings("1.0");
		}
		if (string.IsNullOrEmpty(PhotonNetwork.playerName))
		{
			PhotonNetwork.playerName = "Guest" + Random.Range(1, 9999);
		}
	}

	public void OnGUI()
	{
		if (!PhotonNetwork.connected)
		{
			if (PhotonNetwork.connecting)
			{
				GUILayout.Label("Connecting to: " + PhotonNetwork.ServerAddress);
			}
			else
			{
				GUILayout.Label(string.Concat("Not connected. Check console output. Detailed connection state: ", PhotonNetwork.connectionStateDetailed, " Server: ", PhotonNetwork.ServerAddress));
			}
			if (connectFailed)
			{
				GUILayout.Label("Connection failed. Check setup and use Setup Wizard to fix configuration.");
				GUILayout.Label(string.Format("Server: {0}", new object[1] { PhotonNetwork.ServerAddress }));
				GUILayout.Label("AppId: " + PhotonNetwork.PhotonServerSettings.AppID);
				if (GUILayout.Button("Try Again", GUILayout.Width(100f)))
				{
					connectFailed = false;
					PhotonNetwork.ConnectUsingSettings("1.0");
				}
			}
			return;
		}
		GUI.skin.box.fontStyle = FontStyle.Bold;
		GUI.Box(new Rect((Screen.width - 400) / 2, (Screen.height - 350) / 2, 400f, 300f), "Join or Create a Room");
		GUILayout.BeginArea(new Rect((Screen.width - 400) / 2, (Screen.height - 350) / 2, 400f, 300f));
		GUILayout.Space(25f);
		GUILayout.BeginHorizontal();
		GUILayout.Label("Player name:", GUILayout.Width(100f));
		PhotonNetwork.playerName = GUILayout.TextField(PhotonNetwork.playerName);
		GUILayout.Space(105f);
		if (GUI.changed)
		{
			PlayerPrefs.SetString("PlayerName", PhotonNetwork.playerName);
		}
		GUILayout.EndHorizontal();
		GUILayout.Space(15f);
		GUILayout.BeginHorizontal();
		GUILayout.Label("Roomname:", GUILayout.Width(100f));
		roomName = GUILayout.TextField(roomName);
		if (GUILayout.Button("Create Room", GUILayout.Width(100f)))
		{
			PlayerPrefs.SetString("PlayerName", PhotonNetwork.playerName);
			PhotonNetwork.CreateRoom(roomName, new RoomOptions
			{
				maxPlayers = 10
			}, null);
		}
		GUILayout.EndHorizontal();
		GUILayout.BeginHorizontal();
		GUILayout.FlexibleSpace();
		if (GUILayout.Button("Join Room", GUILayout.Width(100f)))
		{
			PlayerPrefs.SetString("PlayerName", PhotonNetwork.playerName);
			PhotonNetwork.JoinRoom(roomName);
		}
		GUILayout.EndHorizontal();
		GUILayout.Space(15f);
		GUILayout.BeginHorizontal();
		GUILayout.Label(PhotonNetwork.countOfPlayers + " users are online in " + PhotonNetwork.countOfRooms + " rooms.");
		GUILayout.FlexibleSpace();
		if (GUILayout.Button("Join Random", GUILayout.Width(100f)))
		{
			PlayerPrefs.SetString("PlayerName", PhotonNetwork.playerName);
			PhotonNetwork.JoinRandomRoom();
		}
		GUILayout.EndHorizontal();
		GUILayout.Space(15f);
		if (PhotonNetwork.GetRoomList().Length == 0)
		{
			GUILayout.Label("Currently no games are available.");
			GUILayout.Label("Rooms will be listed here, when they become available.");
		}
		else
		{
			GUILayout.Label(string.Concat(PhotonNetwork.GetRoomList(), " currently available. Join either:"));
			scrollPos = GUILayout.BeginScrollView(scrollPos);
			RoomInfo[] roomList = PhotonNetwork.GetRoomList();
			foreach (RoomInfo roomInfo in roomList)
			{
				GUILayout.BeginHorizontal();
				GUILayout.Label(roomInfo.name + " " + roomInfo.playerCount + "/" + roomInfo.maxPlayers);
				if (GUILayout.Button("Join"))
				{
					PhotonNetwork.JoinRoom(roomInfo.name);
				}
				GUILayout.EndHorizontal();
			}
			GUILayout.EndScrollView();
		}
		GUILayout.EndArea();
	}

	public void OnJoinedRoom()
	{
		Debug.Log("OnJoinedRoom");
	}

	public void OnCreatedRoom()
	{
		Debug.Log("OnCreatedRoom");
		PhotonNetwork.LoadLevel(SceneNameGame);
	}

	public void OnDisconnectedFromPhoton()
	{
		Debug.Log("Disconnected from Photon.");
	}

	public void OnFailedToConnectToPhoton(object parameters)
	{
		connectFailed = true;
		Debug.Log(string.Concat("OnFailedToConnectToPhoton. StatusCode: ", parameters, " ServerAddress: ", PhotonNetwork.networkingPeer.ServerAddress));
	}
}
