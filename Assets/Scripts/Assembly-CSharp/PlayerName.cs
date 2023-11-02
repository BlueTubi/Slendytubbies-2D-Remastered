using Photon;
using UnityEngine;

public class PlayerName : Photon.MonoBehaviour
{
	public string playerName;

	public Transform positionDisplay;

	public bool showname = true;

	private string tempname;

	public bool isadmin;

	private void Awake()
	{
		if (PhotonNetwork.offlineMode == true) {
			showname = false;
		}
		if (!positionDisplay)
		{
			positionDisplay = base.transform.Find("PlayerName");
		}
		if (!base.photonView.isMine)
		{
			return;
		}
		string @string = CryptoPlayerPrefs.GetString("ZWName");
		string string2 = CryptoPlayerPrefs.GetString("PlayerType");
		tempname = PhotonNetwork.playerName;
		base.photonView.RPC("SetName", PhotonTargets.AllBuffered, tempname);
	}

	private void FixedUpdate()
	{
		playerName = tempname;
	}

	private void OnGUI()
	{
		GUI.depth = 2;
		float num = 0f;
		if (!showname)
		{
			return;
		}
		if ((bool)Camera.main)
		{
			Vector3 vector = Camera.main.WorldToScreenPoint(positionDisplay.position);
			if (vector.z * 3f < 50f)
			{
				num = vector.z * 3f;
			}
			else
			{
				num = 50f;
			}
			if (vector.z > 0f)
			{
				GUI.Label(new Rect(vector.x - 30f, (float)Screen.height - vector.y - 1f - 21f, 200f, 30f), "<b>" + playerName + "</b>");
			}
		}
	}

	[RPC]
	private void SetName(string newname)
	{
		if (base.photonView.isMine)
		{
			newname = tempname;
		}
		base.gameObject.name = newname;
		tempname = newname;
	}
}
