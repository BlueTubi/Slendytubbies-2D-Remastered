using System.Collections;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
	public GUISkin menuskin;

	public Transform charactercreation;

	public Transform title;

	private bool spmenu;

	public string[] mapnames;

	public Texture2D[] mapimages;

	public int selectedpremap;

	public Vector2 scrollPosition = Vector2.zero;

	public string[] custommaps;

	public bool iscustom;

	public string selectedmap;

	public Texture2D custommaptex;

	public int[] maxCustards;

	private int setMaxCustards;

	public bool haslogin;

	public bool isCredits;

	public bool isSettings;

	private bool isFullscreen;

	private string singleplayer = "Singleplayer";

	private string multiplayer = "Multiplayer";

	private string charactercustomization = "Character Customization";

	private string settings = "Settings";

	private string credits = "Credits";

	private string quit = "Quit";

	private string map = "Map";

	private string totalcustards = "Total Custards";

	private string startgame = "Start Game";

	private string originalmaps = "Original Maps";

	private string back = "Back";

	private string logintozeoworks = "Login To ZeoWorks Account";

	private string mainmenu = "Main Menu";

	private string custards = "Custards";

	private string logout = "Log Out";

	private string resolution = "Screen Resolution";

	private string quality = "Quality";

	private string createdby = "Created By: ";

	private string mappers = "Mappers: ";

	private string assistantmappers = "Assistant Mappers: ";

	private string website = "Website: ";

	private string high = "High";

	private string medium = "Medium";

	private string low = "Low";

	private string togglefullscreen = "Toggle Fullscreen";

	private string translation = "Spanish Translation By: ";

	private string thanks = "Special Thanks: ";

	private string specialthanks = "Special thanks goes to the Slendytubbies community that has helped motivate us to make you guys fun free horror games! :)";

    private string user = "Username";

    private void Awake()
	{
		Cursor.visible = true;
		string @string = CryptoPlayerPrefs.GetString("RoomPlayers");
		if (@string != string.Empty)
		{
			StartCoroutine(setPlayers());
		}
	}

	private IEnumerator setPlayers()
	{
		WWWForm form = new WWWForm();
		string id = CryptoPlayerPrefs.GetString("ServerID");
		form.AddField("id", id);
		int players = int.Parse(CryptoPlayerPrefs.GetString("RoomPlayers"));
		form.AddField("players", (players - 1).ToString());
		CryptoPlayerPrefs.SetString("RoomPlayers", string.Empty);
		yield return new WWW("http://zeoworks.com/s2d_update_server.php", form);
		MonoBehaviour.print("Success! Updated player count");
	}

	private void Start()
	{
		string @string = CryptoPlayerPrefs.GetString("ZWName");
		PlayerPrefs.SetString("gamemode", "Co-op");
		if (@string != string.Empty)
		{
			haslogin = true;
		}
		if (PhotonNetwork.connected)
		{
			PhotonNetwork.Disconnect();
		}
	}

	private void OnGUI()
	{
		GUI.skin = menuskin;
		if (!isSettings && !isCredits)
		{
			if (!spmenu)
			{
				title.gameObject.SetActive(true);
                user = GUI.TextField(new Rect(Screen.width - 230, 0f, 250f, 20f), user);

                if (GUI.Button(new Rect(Screen.width - 230, 20f, 230f, 30f), "Change username"))
				{
					CryptoPlayerPrefs.SetString("ZWName", user);
				}
				GUI.Box(new Rect(10f, Screen.height - 390, 300f, 270f), mainmenu);
				if (GUI.Button(new Rect(20f, Screen.height - 370, 270f, 40f), singleplayer))
				{
					PhotonNetwork.offlineMode = true;
					spmenu = true;
				}
				if (GUI.Button(new Rect(20f, Screen.height - 330, 270f, 40f), multiplayer))
				{
					PhotonNetwork.offlineMode = false;
					GetComponent<ServerList>().enabled = true;
					base.enabled = false;
				}
				if (GUI.Button(new Rect(20f, Screen.height - 290, 270f, 40f), charactercustomization))
				{
					charactercreation.gameObject.SetActive(true);
					base.transform.gameObject.SetActive(false);
				}
				if (GUI.Button(new Rect(20f, Screen.height - 250, 270f, 40f), settings))
				{
					isSettings = true;
				}
				if (GUI.Button(new Rect(20f, Screen.height - 210, 270f, 40f), credits))
				{
					isCredits = true;
				}
				if (GUI.Button(new Rect(20f, Screen.height - 170, 270f, 40f), quit))
				{
					Application.Quit();
				}
			}
			else
			{
				title.gameObject.SetActive(false);
				GUI.Box(new Rect(Screen.width / 2 - 320, Screen.height / 2 - 180, 300f, 380f), map);
				GUI.Box(new Rect(Screen.width / 2 - 310, Screen.height / 2 - 160, 260f, 340f), string.Empty);
				if (GUI.Button(new Rect(0f, 0f, 100f, 40f), back))
				{
					spmenu = false;
				}
				if (!iscustom)
				{
					GUI.DrawTexture(new Rect(Screen.width / 2 + 20, Screen.height / 2 - 180, 250f, 200f), mapimages[selectedpremap]);
					GUI.Label(new Rect(Screen.width / 2 + 20, Screen.height / 2 + 20, 250f, 25f), totalcustards);
					if (GUI.Button(new Rect(Screen.width / 2 + 20, Screen.height / 2 + 40, 250f, 40f), maxCustards[setMaxCustards] + " " + custards))
					{
						if (setMaxCustards < maxCustards.Length - 1)
						{
							setMaxCustards++;
						}
						else
						{
							setMaxCustards = 0;
						}
					}
					if (GUI.Button(new Rect(Screen.width / 2 + 20, Screen.height / 2 + 90, 250f, 70f), startgame))
					{
						PlayerPrefs.SetInt("custardamount", maxCustards[setMaxCustards]);
						PhotonNetwork.offlineMode = true;
						Application.LoadLevel("OfflineMode");
						CryptoPlayerPrefs.SetString("Map", selectedmap);
						iscustom = false;
					}
				}
				else
				{
					GUI.DrawTexture(new Rect(Screen.width / 2 + 20, Screen.height / 2 - 180, 250f, 200f), custommaptex);
					if (!GUI.Button(new Rect(Screen.width / 2 + 20, Screen.height / 2 + 80, 250f, 40f), startgame))
					{
					}
				}
				scrollPosition = GUI.BeginScrollView(new Rect(Screen.width / 2 - 305, Screen.height / 2 - 160, 280f, 340f), scrollPosition, new Rect(0f, 0f, 240f, mapnames.Length * 40 + custommaps.Length * 40));
				GUI.Label(new Rect(0f, 5f, 250f, 25f), originalmaps);
				for (int i = 0; i < mapnames.Length; i++)
				{
					if (GUI.Button(new Rect(0f, 30 + 40 * i, 250f, 40f), mapnames[i]))
					{
						iscustom = false;
						selectedpremap = i;
						selectedmap = mapnames[selectedpremap];
					}
				}
				GUI.EndScrollView();
			}
		}
		if (isSettings)
		{
			if (GUI.Button(new Rect(10f, 10f, 120f, 60f), back))
			{
				isSettings = false;
				isCredits = false;
			}
			GUI.Box(new Rect(Screen.width / 2 - 200, Screen.height / 2 - 200, 400f, 380f), settings);
			GUI.Label(new Rect(Screen.width / 2 - 190, Screen.height / 2 - 183, 150f, 23f), quality);
			if (GUI.Button(new Rect(Screen.width / 2 - 190, Screen.height / 2 - 160, 150f, 80f), high))
			{
				QualitySettings.SetQualityLevel(2);
			}
			if (GUI.Button(new Rect(Screen.width / 2 - 190, Screen.height / 2 - 80, 150f, 80f), medium))
			{
				QualitySettings.SetQualityLevel(1);
			}
			if (GUI.Button(new Rect(Screen.width / 2 - 190, Screen.height / 2, 150f, 80f), low))
			{
				QualitySettings.SetQualityLevel(0);
			}
			if (!Application.isMobilePlatform)
			{
				isFullscreen = Screen.fullScreen;
				GUI.Label(new Rect(Screen.width / 2 + 30, Screen.height / 2 - 183, 150f, 23f), resolution);
				if (GUI.Button(new Rect(Screen.width / 2 + 30, Screen.height / 2 - 160, 150f, 20f), "800x600"))
				{
					Screen.SetResolution(800, 600, isFullscreen);
				}
				if (GUI.Button(new Rect(Screen.width / 2 + 30, Screen.height / 2 - 140, 150f, 20f), "1024x768"))
				{
					Screen.SetResolution(1024, 768, isFullscreen);
				}
				if (GUI.Button(new Rect(Screen.width / 2 + 30, Screen.height / 2 - 120, 150f, 20f), "1280x600"))
				{
					Screen.SetResolution(1280, 600, isFullscreen);
				}
				if (GUI.Button(new Rect(Screen.width / 2 + 30, Screen.height / 2 - 100, 150f, 20f), "1280x720"))
				{
					Screen.SetResolution(1280, 720, isFullscreen);
				}
				if (GUI.Button(new Rect(Screen.width / 2 + 30, Screen.height / 2 - 80, 150f, 20f), "1280x768"))
				{
					Screen.SetResolution(1280, 768, isFullscreen);
				}
				if (GUI.Button(new Rect(Screen.width / 2 + 30, Screen.height / 2 - 60, 150f, 20f), "1360x768"))
				{
					Screen.SetResolution(1360, 768, isFullscreen);
				}
				if (GUI.Button(new Rect(Screen.width / 2 + 30, Screen.height / 2 - 40, 150f, 20f), "1920x1080"))
				{
					Screen.SetResolution(1920, 1080, isFullscreen);
				}
				if (GUI.Button(new Rect(Screen.width / 2 - 120, Screen.height / 2 + 120, 300f, 40f), togglefullscreen))
				{
					Screen.fullScreen = !Screen.fullScreen;
				}
			}
		}
		if (isCredits)
		{
			if (GUI.Button(new Rect(10f, 10f, 120f, 60f), back))
			{
				isSettings = false;
				isCredits = false;
			}
			GUI.Box(new Rect(Screen.width / 2 - 200, Screen.height / 2 - 200, 400f, 410f), credits);
			GUI.skin = null;
			GUI.Label(new Rect(Screen.width / 2 - 190, Screen.height / 2 - 170, 300f, 370f), createdby + "\n<color=red>Sean Toman</color>\n\n" + mappers + "\n<color=red>Aythadis</color>\n<color=red>Sean Toman</color>\n<color=red>Alex</color>\n\n" + assistantmappers + "\n<color=red>Santiago Porro (Santikun)</color>\n<color=red>Yash Mahmud</color>\n\n" + translation + "\n<color=red>Santiago Porro (Santikun)</color>\n\n" + website + "\n<color=red>www.zeoworks.com</color>\n<color=red>www.zeogames.net</color>\n\n" + thanks + "\n<color=red>" + specialthanks + "</color>");
		}
	}
}
