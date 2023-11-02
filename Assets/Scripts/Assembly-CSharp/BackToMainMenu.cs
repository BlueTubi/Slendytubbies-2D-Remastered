using Photon;
using UnityEngine;

public class BackToMainMenu : Photon.MonoBehaviour
{
	public static BackToMainMenu SP;

	private void Awake()
	{
		if (SP != null && SP != this)
		{
			Object.Destroy(this);
			return;
		}
		SP = this;
		Object.DontDestroyOnLoad(this);
	}

	private void OnGUI()
	{
		if (Application.loadedLevel >= 1 && GUI.Button(new Rect(Screen.width - 150, Screen.height - 20, 150f, 20f), "Back to Main Menu"))
		{
			QuitToMainMenu();
		}
	}

	private void QuitToMainMenu()
	{
		if (PhotonNetwork.connected)
		{
			PhotonNetwork.Disconnect();
		}
		Application.LoadLevel(0);
	}
}
