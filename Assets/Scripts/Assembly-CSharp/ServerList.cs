using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerList : MonoBehaviour
{
	[Serializable]
	public class serverInfo
	{
		public string serverID;

		public string curPlayers = "-";

		public string maxPlayers = "20";
	}

	public List<serverInfo> Servers;

	public string[] rawInfo;

	public GUISkin guiSkin;

	public Vector2 scrollPosition = Vector2.zero;

	private string selectaserver = "Please Select A Server";

	private string back = "Back";

	private string server = "Server #";

	private string players = "Players: ";

	private IEnumerator Start()
	{
		WWW www = new WWW("http://www.zeoworks.com/s2d_servers.php");
		yield return www;
		rawInfo = www.text.Split(","[0]);
		for (int i = 0; i < Servers.Count; i++)
		{
			Servers[i].curPlayers = rawInfo[i];
		}
	}

	private void Awake()
	{
		CryptoPlayerPrefs.SetString("AppID", Servers[0].serverID);
		if (PlayerPrefs.GetInt("language") == 1)
		{
			selectaserver = "Por favor seleccione un servidor";
			server = "Servidor #";
			players = "Jugadores: ";
		}
	}

	private void OnGUI()
	{
		GUI.skin = guiSkin;
		if (GUI.Button(new Rect(10f, 10f, 120f, 60f), back))
		{
			base.enabled = false;
			GetComponent<MainMenu>().enabled = true;
		}
		GUI.Box(new Rect(Screen.width / 2 - 200, Screen.height / 2 - 200, 400f, 400f), selectaserver);
		scrollPosition = GUI.BeginScrollView(new Rect(Screen.width / 2 - 195, Screen.height / 2 - 170, 390f, 350f), scrollPosition, new Rect(0f, 0f, 355f, Servers.Count * 80 + 40));
		for (int i = 0; i < Servers.Count; i++)
		{
			if (GUI.Button(new Rect(10f, 20 + 80 * i, 350f, 80f), server + (i + 1)))
			{
				CryptoPlayerPrefs.SetString("ServerID", i.ToString());
				CryptoPlayerPrefs.SetString("AppID", Servers[i].serverID);
				Application.LoadLevel("Lobby");
			}
			GUI.Label(new Rect(250f, 65 + 80 * i, 100f, 25f), players + Servers[i].curPlayers + "/" + Servers[i].maxPlayers);
		}
		GUI.EndScrollView();
	}
}
