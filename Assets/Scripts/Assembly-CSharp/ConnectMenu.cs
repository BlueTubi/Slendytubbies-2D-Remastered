using System;
using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using Photon;
using UnityEngine;

public class ConnectMenu : Photon.MonoBehaviour
{
	[Serializable]
	public class AllMaps
	{
		public string mapName;

		public Texture2D mapPreview;
	}

	public GUISkin guiSKin;

	public Texture blackScreen;

	public Texture top;

	public Texture bottom;

	public bool maxplayers2;

	public int roundDuration = 600;

	private List<int> maxPlayersOptions = new List<int>();

	public List<AllMaps> allMaps;

	public string totplayers = "25";

	private string newRoomName;

	private string playerName;

	public int maxPlayers;

	private int selectedMap;

	private string gameMode;

	private string deathmatch = "Co-op";

	private string teamdeathmatch = "Versus";

	private string privateword = "Private";

	private bool hidedeathmatch;

	private bool privatematch;

	private Vector2 scroll;

	private Vector2 mapScroll;

	private float fadeValue;

	private int fadeDir;

	private RoomInfo[] allRooms;

	private bool createRoom;

	private bool connectingToRoom;

	private bool setPlayerCount;

	private string connecting = "Connecting...";

	private string lobby = "Lobby";

	private string joinroom = "Join Room";

	private string noroomscreated = "No rooms created...";

	private string createroom = "Create Room";

	private string playername = "Player Name: ";

	private string roomname = "Room Name: ";

	private string custardamount = "Custard Amount: ";

	private string returntolobby = "Return To Lobby";

	private string play = "Play";

	private string loading = "Loading...";

	private string mainmenu = "Main Menu";

	private string test;

	private void Awake()
	{
		Cursor.visible = true;
		test = CryptoPlayerPrefs.GetString("ZWName");
		string @string = CryptoPlayerPrefs.GetString("PlayerType");
		if (test != string.Empty)
		{
			string text = string.Empty;
			string text2 = string.Empty;
			if (@string == "2")
			{
				text = "<color=green>";
				text2 = "</color>";
			}
			if (@string == "3")
			{
				text = "<color=cyan>";
				text2 = "</color>";
			}
			if (@string == "4")
			{
				text = "<color=red>";
				text2 = "</color>";
			}
			if (@string == "6")
			{
				text = "<color=cyan>";
				text2 = "</color>";
			}
			if (@string == "7")
			{
				text = "<color=grey>";
				text2 = "</color>";
			}
			if (@string == "8")
			{
				text = "<color=green>";
				text2 = "</color>";
			}
			if (@string == "9")
			{
				text = "<color=red>";
				text2 = "</color>";
			}
			if (@string == "10")
			{
				text = "<color=yellow>";
				text2 = "</color>";
			}
			if (@string == "11")
			{
				text = "<color=blue>";
				text2 = "</color>";
			}
			test = text + test + text2;
		}
		if (PlayerPrefs.GetInt("language") == 1)
		{
			connecting = "Conectando...";
			lobby = "Lobby";
			joinroom = "Unirse";
			noroomscreated = "No hay salas creadas...";
			createroom = "Crear Sala";
			playername = "Nombre del Jugador: ";
			roomname = "Nombre de la Sala: ";
			custardamount = "Cantidad de Tubipapillas: ";
			returntolobby = "Volver al Lobby";
			play = "Jugar";
			loading = "Cargando...";
			privateword = "Privado";
			mainmenu = "Menú Principal";
		}
		PhotonNetwork.offlineMode = false;
	}

	private void Start()
	{
		PhotonNetwork.isMessageQueueRunning = true;
		Screen.lockCursor = false;
		allRooms = PhotonNetwork.GetRoomList();
		newRoomName = "Room Name " + UnityEngine.Random.Range(111, 999);
		playerName = "Player " + UnityEngine.Random.Range(111, 999);
		maxPlayersOptions.Add(5);
		maxPlayersOptions.Add(10);
		maxPlayersOptions.Add(15);
		maxPlayersOptions.Add(20);
		maxPlayersOptions.Add(25);
		maxPlayers = maxPlayersOptions[2];
		selectedMap = 0;
		if (roundDuration == 0)
		{
			roundDuration = 600;
		}
		gameMode = "Co-op";
		if (PlayerPrefs.HasKey("PlayerName"))
		{
			playerName = PlayerPrefs.GetString("PlayerName");
		}
		Cursor.visible = true;
		Screen.lockCursor = false;
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			Application.LoadLevel(0);
		}
		float num = 3f;
		float num2 = 0f;
		if (!PhotonNetwork.connected)
		{
			if (Time.time - num > num2)
			{
				num2 = Time.time - Time.deltaTime;
			}
			while (num2 < Time.time)
			{
				num2 += num;
				if (PhotonNetwork.connectionState != ConnectionState.Connecting && PhotonNetwork.connectionState != ConnectionState.InitializingApplication && PhotonNetwork.connectionState != ConnectionState.Disconnecting)
				{
					PhotonNetwork.ConnectUsingSettings("1.0");
				}
			}
		}
		if (PhotonNetwork.connected && allRooms.Length != PhotonNetwork.GetRoomList().Length)
		{
			allRooms = PhotonNetwork.GetRoomList();
		}
		if (PhotonNetwork.connected && !setPlayerCount && PhotonNetwork.countOfPlayers != 0)
		{
			StartCoroutine(setPlayers());
		}
	}

	private IEnumerator setPlayers()
	{
		setPlayerCount = true;
		WWWForm form = new WWWForm();
		string id = CryptoPlayerPrefs.GetString("ServerID");
		form.AddField("id", id);
		string players = PhotonNetwork.countOfPlayers.ToString();
		form.AddField("players", players);
		yield return new WWW("http://zeoworks.com/s2d_update_server.php", form);
		UnityEngine.MonoBehaviour.print("Success! Updated player count");
		CryptoPlayerPrefs.SetString("RoomPlayers", players);
	}

	private void OnGUI()
	{
		GUI.skin = guiSKin;
		if (GUI.Button(new Rect(0f, 0f, 150f, 50f), mainmenu))
		{
			Application.LoadLevel("MainMenu");
		}
		GUI.color = Color.white;
		GUI.depth = -2;
		if (!PhotonNetwork.connected)
		{
			GUI.enabled = false;
		}
		else
		{
			GUI.enabled = true;
		}
		GUI.color = new Color(1f, 1f, 1f, 0.9f);
		GUI.DrawTexture(new Rect(Screen.width - bottom.width, Screen.height - bottom.height, bottom.width, bottom.height), bottom, ScaleMode.ScaleToFit);
		GUI.color = Color.white;
		GUILayout.BeginArea(new Rect(Screen.width / 2 - 250, Screen.height / 2 - 220, 500f, 440f), lobby, GUI.skin.GetStyle("window"));
		ShowConnectMenu();
		GUILayout.EndArea();
		if (!PhotonNetwork.connected)
		{
			GUI.color = Color.white;
			GUI.Box(new Rect(Screen.width / 2 - 75, Screen.height / 2 - 15, 150f, 30f), connecting);
		}
		FadeScreen();
	}

	private void ShowConnectMenu()
	{
		GUILayout.Space(10f);
		if (!createRoom)
		{
			scroll = GUILayout.BeginScrollView(scroll, GUILayout.Width(480f), GUILayout.Height(300f));
			if (allRooms != null && allRooms.Length > 0)
			{
				RoomInfo[] array = allRooms;
				foreach (RoomInfo roomInfo in array)
				{
					if (allRooms.Length > 0)
					{
						GUILayout.BeginHorizontal("box");
						GUILayout.Label(roomInfo.name, GUILayout.Width(150f));
						GUILayout.Label((string)roomInfo.customProperties["MapName"], GUILayout.Width(135f));
						GUILayout.Label(roomInfo.playerCount + "/" + roomInfo.maxPlayers, GUILayout.Width(60f));
						GUILayout.FlexibleSpace();
						if (GUILayout.Button(joinroom, GUILayout.Width(100f), GUILayout.Height(50f)))
						{
							PhotonNetwork.JoinRoom(roomInfo.name);
							PhotonNetwork.playerName = playerName;
							connectingToRoom = true;
							CheckPlayerNameAndRoom();
							PlayerPrefs.SetString("PlayerName", playerName);
						}
						GUILayout.EndHorizontal();
					}
				}
			}
			else
			{
				GUILayout.Label(noroomscreated);
			}
			GUILayout.EndScrollView();
			GUILayout.Space(5f);
			GUILayout.BeginHorizontal();
			GUILayout.Label(playername);
			if (test != string.Empty)
			{
				GUILayout.Label("<b>" + test + "</b>", GUILayout.Height(25f));
			}
			else
			{
				playerName = GUILayout.TextField(playerName, 15, GUILayout.Height(25f));
			}
			GUILayout.EndHorizontal();
			GUILayout.FlexibleSpace();
			GUILayout.BeginHorizontal();
			GUILayout.FlexibleSpace();
			if (GUILayout.Button(createroom, GUILayout.Width(130f), GUILayout.Height(50f)))
			{
				createRoom = true;
				CheckPlayerNameAndRoom();
				PlayerPrefs.SetString("PlayerName", playerName);
			}
			GUILayout.EndHorizontal();
			return;
		}
		GUILayout.BeginHorizontal();
		GUILayout.Label(roomname, GUILayout.Width(130f));
		newRoomName = GUILayout.TextField(newRoomName, 15, GUILayout.Height(25f));
		GUILayout.EndHorizontal();
		GUILayout.Space(5f);
		GUILayout.BeginHorizontal();
		GUILayout.Label(custardamount, GUILayout.Width(130f));
		for (int j = 0; j < maxPlayersOptions.Count; j++)
		{
			if (maxPlayers == maxPlayersOptions[j])
			{
				GUI.color = Color.red;
			}
			else
			{
				GUI.color = Color.white;
			}
			if (GUILayout.Button(maxPlayersOptions[j].ToString(), GUILayout.Width(27f), GUILayout.Height(50f)))
			{
				maxPlayers = maxPlayersOptions[j];
			}
		}
		GUI.color = Color.white;
		GUILayout.EndHorizontal();
		GUILayout.Space(5f);
		GUILayout.BeginHorizontal();
		GUILayout.Label("Game Mode: ", GUILayout.Width(130f));
		if (gameMode == "Co-op")
		{
			GUI.color = Color.red;
		}
		if (!hidedeathmatch && GUILayout.Button(deathmatch, GUILayout.Width(140f), GUILayout.Height(50f)))
		{
			gameMode = "Co-op";
		}
		GUI.color = Color.white;
		if (gameMode == "Versus")
		{
			GUI.color = Color.red;
		}
		if (GUILayout.Button(teamdeathmatch, GUILayout.Width(140f), GUILayout.Height(50f)))
		{
			gameMode = "Versus";
		}
		GUILayout.EndHorizontal();
		GUI.color = Color.white;
		GUILayout.Space(5f);
		GUILayout.BeginHorizontal();
		mapScroll = GUILayout.BeginScrollView(mapScroll, false, true, GUILayout.Width(240f), GUILayout.Height(160f));
		for (int k = 0; k < allMaps.Count; k++)
		{
			if (selectedMap == k)
			{
				GUI.color = Color.red;
			}
			else
			{
				GUI.color = Color.white;
			}
			if (GUILayout.Button(allMaps[k].mapName, GUILayout.Height(50f)))
			{
				selectedMap = k;
			}
		}
		GUI.color = Color.white;
		GUILayout.EndScrollView();
		GUILayout.Space(10f);
		if (allMaps[selectedMap].mapPreview != null)
		{
			GUILayout.Label(allMaps[selectedMap].mapPreview, GUILayout.Width(230f), GUILayout.Height(160f));
		}
		GUILayout.EndHorizontal();
		GUILayout.FlexibleSpace();
		GUILayout.BeginHorizontal();
		if (GUILayout.Button(returntolobby, GUILayout.Width(160f), GUILayout.Height(50f)))
		{
			createRoom = false;
		}
		GUILayout.FlexibleSpace();
		if (GUILayout.Button(play, GUILayout.Width(130f), GUILayout.Height(50f)))
		{
			CheckPlayerNameAndRoom();
			PhotonNetwork.playerName = playerName;
			ExitGames.Client.Photon.Hashtable hashtable = new ExitGames.Client.Photon.Hashtable();
			hashtable["MapName"] = allMaps[selectedMap].mapName;
			hashtable["GameMode"] = gameMode;
			string[] propsToListInLobby = new string[2] { "MapName", "GameMode" };
			PlayerPrefs.SetString("gamemode", gameMode);
			PlayerPrefs.SetInt("custardamount", maxPlayers);
			if (!maxplayers2)
			{
				PhotonNetwork.CreateRoom(newRoomName, true, true, 6, hashtable, propsToListInLobby);
			}
			else
			{
				PhotonNetwork.CreateRoom(newRoomName, true, true, int.Parse(totplayers), hashtable, propsToListInLobby);
			}
		}
		GUILayout.EndHorizontal();
	}

	private void FadeScreen()
	{
		if (connectingToRoom)
		{
			fadeDir = 1;
			fadeValue += (float)(fadeDir * 15) * Time.deltaTime;
			fadeValue = Mathf.Clamp01(fadeValue);
			GUI.color = new Color(1f, 1f, 1f, fadeValue);
			GUI.DrawTexture(new Rect(0f, 0f, Screen.width, Screen.height), blackScreen);
			GUI.color = Color.white;
			GUI.Label(new Rect(Screen.width / 2 - 75, Screen.height / 2 - 15, 150f, 30f), loading);
		}
	}

	private IEnumerator LoadMap(string sceneName)
	{
		connectingToRoom = true;
		PhotonNetwork.isMessageQueueRunning = false;
		fadeDir = 1;
		yield return new WaitForSeconds(1f);
		Application.backgroundLoadingPriority = ThreadPriority.High;
		yield return Application.LoadLevelAsync(sceneName);
		Debug.Log("Loading complete");
	}

	private void CheckPlayerNameAndRoom()
	{
		string text = playerName.Replace("<", string.Empty);
		if (text == string.Empty)
		{
			playerName = "Player " + UnityEngine.Random.Range(111, 999);
		}
		string text2 = newRoomName.Replace("<", string.Empty);
		if (text2 == string.Empty)
		{
			newRoomName = "Room Name " + UnityEngine.Random.Range(111, 999);
		}
		PhotonNetwork.playerName = text;
	}

	private void OnJoinedRoom()
	{
		UnityEngine.MonoBehaviour.print("Joined room: " + newRoomName);
		connectingToRoom = true;
		PhotonNetwork.LoadLevel((string)PhotonNetwork.room.customProperties["MapName"]);
	}

	private void OnJoinedLobby()
	{
		UnityEngine.MonoBehaviour.print("Joined master server");
	}

	private void OnLeftRoom()
	{
		connectingToRoom = false;
	}

	private void OnPhotonJoinRoomFailed()
	{
		UnityEngine.MonoBehaviour.print("Failed on connecting to room");
		connectingToRoom = false;
	}

	private void OnPhotonCreateRoomFailed()
	{
		UnityEngine.MonoBehaviour.print("Failed on creating room");
		connectingToRoom = false;
	}

	private void OnPhotonPlayerConnected()
	{
		UnityEngine.MonoBehaviour.print("Player connected");
	}

	private void OnConnectedToPhoton()
	{
		UnityEngine.MonoBehaviour.print("We connected to Photon Cloud");
		if (PhotonNetwork.room != null)
		{
			PhotonNetwork.LeaveRoom();
		}
		connectingToRoom = false;
	}

	private void OnDisconnectedFromPhoton()
	{
		UnityEngine.MonoBehaviour.print("We disconencted from Photon Cloud");
	}
}
