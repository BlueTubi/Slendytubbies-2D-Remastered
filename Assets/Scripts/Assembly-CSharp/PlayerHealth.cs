using Photon;
using UnityEngine;

public class PlayerHealth : Photon.MonoBehaviour
{
	public Transform model;

	public Transform spawnpoint;

	private float walkspeed = 6f;

	private float runspeed = 12f;

	public Texture2D blood;

	public bool isdead;

	public GUISkin guiSkin;

	private string youaredead = "You Are Dead...";

	private string respawn = "Respawn";

	private string returntomainmenu = "Return To Main Menu";

	private void Awake()
	{
		if (PlayerPrefs.GetInt("language") == 1)
		{
			youaredead = "Estás Muerto...";
			respawn = "Reaparecer";
			returntomainmenu = "Volver al Menú Principal";
		}
	}

	private void Start()
	{
		spawnpoint = GameObject.FindWithTag("PlayerStartPoint").transform;
		if (!base.photonView.isMine)
		{
		}
	}

	private void KillNow()
	{
		model.localEulerAngles = new Vector3(90f, 90f, 0f);
		if (base.photonView.isMine)
		{
			Cursor.visible = true;
		}
		base.transform.tag = "Untagged";
		if (base.photonView.isMine)
		{
			isdead = true;
			GetComponent<PlayerMovementPC>().WalkSpeed(0f);
			GetComponent<PlayerMovementPC>().RunSpeed(0f);
			GetComponent<PlayerMovementMobile>().walkspeed = 0f;
			GetComponent<PlayerMovementMobile>().runspeed = 0f;
		}
	}

	private void Respawn()
	{
		model.localEulerAngles = new Vector3(90f, 0f, 0f);
		if (base.photonView.isMine)
		{
			Cursor.visible = false;
		}
		base.transform.tag = "Player";
		if (base.photonView.isMine)
		{
			isdead = false;
			GetComponent<PlayerMovementPC>().WalkSpeed(walkspeed);
			GetComponent<PlayerMovementPC>().RunSpeed(runspeed);
			GetComponent<PlayerMovementMobile>().walkspeed = walkspeed;
			GetComponent<PlayerMovementMobile>().runspeed = runspeed;
			base.transform.position = spawnpoint.position;
		}
	}

	public void KillRequest()
	{
		base.photonView.RPC("KillPlayer", PhotonTargets.All);
	}

	public void RespawnRequest()
	{
		base.photonView.RPC("RespawnPlayer", PhotonTargets.All);
	}

	private void OnGUI()
	{
		if (isdead)
		{
			GUI.DrawTexture(new Rect(0f, 0f, Screen.width, Screen.height), blood);
			GUI.Label(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 30, 100f, 30f), "<b>" + youaredead + "</b>");
			GUI.skin = guiSkin;
			if (!PhotonNetwork.offlineMode && GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2, 200f, 60f), respawn))
			{
				RespawnRequest();
			}
			if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 + 60, 200f, 60f), returntomainmenu))
			{
				PhotonNetwork.LeaveRoom();
				Application.LoadLevel("MainMenu");
			}
		}
	}

	[RPC]
	private void KillPlayer()
	{
		KillNow();
	}

	[RPC]
	private void RespawnPlayer()
	{
		Respawn();
	}
}
