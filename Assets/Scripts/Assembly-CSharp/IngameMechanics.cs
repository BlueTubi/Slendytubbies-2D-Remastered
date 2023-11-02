using System;
using System.Collections;
using GoogleMobileAds.Api;
using Photon;
using UnityEngine;

public class IngameMechanics : Photon.MonoBehaviour
{
	public GameObject[] custardpos;

	public Transform custard;

	public bool showobjective;

	public Transform startpoint;

	public bool gamestart;

	public Transform NPC1;

	public Transform NPC2;

	public Transform versuspoint;

	public GameObject myplayer;

	public GameObject mymonster;

	public int maxcustards = 10;

	public int custardsleft;

	public string npcplayer = "playerTank";

	private string objective;

	public bool isversus;

	public bool atbeginning;

	private bool hasRemoved;

	private BannerView bannerView;

	private void Awake()
	{
		if (base.photonView.isMine)
		{
			maxcustards = PlayerPrefs.GetInt("custardamount");
			atbeginning = true;
		}
		else
		{
			maxcustards = GameObject.FindGameObjectsWithTag("custard").Length;
		}
		if (PhotonNetwork.isMasterClient)
		{
			if (PlayerPrefs.GetString("gamemode") == "Versus")
			{
				mymonster = PhotonNetwork.Instantiate(npcplayer, versuspoint.position, versuspoint.rotation, 0, null);
				base.photonView.RPC("IsVersus", PhotonTargets.AllBuffered);
			}
			else
			{
				myplayer = PhotonNetwork.Instantiate("player", startpoint.position, startpoint.rotation, 0, null);
			}
		}
		else
		{
			myplayer = PhotonNetwork.Instantiate("player", startpoint.position, startpoint.rotation, 0, null);
		}
	}

	private void Start()
	{
		Cursor.visible = false;
	}

	private void OnGUI()
	{
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.P))
		{
			Cursor.visible = !Cursor.visible;
		}
		if (PhotonNetwork.isMasterClient && isversus && mymonster == null)
		{
			PhotonNetwork.Destroy(myplayer.gameObject);
			mymonster = PhotonNetwork.Instantiate(npcplayer, versuspoint.position, versuspoint.rotation, 0, null);
		}
		if (base.photonView.isMine && atbeginning)
		{
			if (custardsleft < maxcustards)
			{
				custardpos = GameObject.FindGameObjectsWithTag("custardPOS");
				GameObject[] array = custardpos;
				foreach (GameObject gameObject in array)
				{
					if (custardsleft < maxcustards)
					{
						int num = UnityEngine.Random.Range(1, 3);
						if (num == 1)
						{
							PhotonNetwork.InstantiateSceneObject("custard", gameObject.transform.position, gameObject.transform.rotation, 0, null);
							gameObject.transform.tag = "Untagged";
							custardsleft++;
						}
					}
				}
			}
			if (custardsleft == maxcustards && !showobjective)
			{
				StartCoroutine(ShowObjectiveNow());
			}
		}
		else if (!showobjective)
		{
			maxcustards = GameObject.FindGameObjectsWithTag("custard").Length;
			StartCoroutine(ShowObjectiveNow());
		}
		if (gamestart && GameObject.FindGameObjectsWithTag("custard").Length == 0)
		{
			PhotonNetwork.LeaveRoom();
			Application.LoadLevel("Victory");
		}
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			PhotonNetwork.LeaveRoom();
			Application.LoadLevel("MainMenu");
		}
		if (isversus && !hasRemoved)
		{
			NPC1.gameObject.SetActive(false);
			if (NPC2 != null)
			{
				NPC2.gameObject.SetActive(false);
				UnityEngine.Object.Destroy(NPC2.GetComponent<FindPlayers>());
			}
			UnityEngine.Object.Destroy(NPC1.GetComponent<FindPlayers>());
			hasRemoved = true;
		}
		if (mymonster != null)
		{
			RenderSettings.ambientLight = Color.red;
		}
	}

	private IEnumerator ShowObjectiveNow()
	{
		showobjective = true;
		if (PlayerPrefs.GetInt("language") == 1)
		{
			objective = "Recoge todas las tubipapillas";
		}
		else
		{
			objective = "Collect All Teletubby Custards";
		}
		base.gameObject.GetComponent<GUIText>().text = objective;
		base.gameObject.GetComponent<GUIText>().enabled = true;
		yield return new WaitForSeconds(0.5f);
		if (PlayerPrefs.GetInt("language") == 1)
		{
			objective = "Recoge todas las tubipapillas";
		}
		else
		{
			objective = "Collect All Teletubby Custards";
		}
		yield return new WaitForSeconds(4.5f);
		base.gameObject.GetComponent<GUIText>().enabled = false;
		gamestart = true;
	}

	private void RequestBanner()
	{
		string adUnitId = "unexpected_platform";
		bannerView = new BannerView(adUnitId, AdSize.SmartBanner, AdPosition.Top);
		bannerView.AdLoaded += HandleAdLoaded;
		bannerView.AdFailedToLoad += HandleAdFailedToLoad;
		bannerView.AdOpened += HandleAdOpened;
		bannerView.AdClosing += HandleAdClosing;
		bannerView.AdClosed += HandleAdClosed;
		bannerView.AdLeftApplication += HandleAdLeftApplication;
		bannerView.LoadAd(createAdRequest());
	}

	private AdRequest createAdRequest()
	{
		return new AdRequest.Builder().AddTestDevice("SIMULATOR").AddTestDevice("0123456789ABCDEF0123456789ABCDEF").AddKeyword("game")
			.SetGender(Gender.Male)
			.SetBirthday(new DateTime(1985, 1, 1))
			.TagForChildDirectedTreatment(false)
			.AddExtra("color_bg", "9B30FF")
			.Build();
	}

	[RPC]
	private void IsVersus()
	{
		isversus = true;
	}

	public void HandleAdLoaded(object sender, EventArgs args)
	{
		UnityEngine.MonoBehaviour.print("HandleAdLoaded event received.");
	}

	public void HandleAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
	{
		UnityEngine.MonoBehaviour.print("HandleFailedToReceiveAd event received with message: " + args.Message);
	}

	public void HandleAdOpened(object sender, EventArgs args)
	{
		UnityEngine.MonoBehaviour.print("HandleAdOpened event received");
	}

	private void HandleAdClosing(object sender, EventArgs args)
	{
		UnityEngine.MonoBehaviour.print("HandleAdClosing event received");
	}

	public void HandleAdClosed(object sender, EventArgs args)
	{
		UnityEngine.MonoBehaviour.print("HandleAdClosed event received");
	}

	public void HandleAdLeftApplication(object sender, EventArgs args)
	{
		UnityEngine.MonoBehaviour.print("HandleAdLeftApplication event received");
	}
}
