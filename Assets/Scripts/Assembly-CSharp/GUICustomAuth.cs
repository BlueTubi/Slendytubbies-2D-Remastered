using UnityEngine;

public class GUICustomAuth : MonoBehaviour
{
	public Rect GuiRect;

	private bool authFailed;

	private string authName = "usr";

	private string authToken = "usr";

	private string authDebugMessage = string.Empty;

	private void Start()
	{
		GuiRect = new Rect(Screen.width / 4, 80f, Screen.width / 2, Screen.height - 100);
	}

	public void OnJoinedLobby()
	{
		base.enabled = false;
	}

	public void OnCustomAuthenticationFailed(string debugMessage)
	{
		authDebugMessage = debugMessage;
		authFailed = true;
	}

	private void OnGUI()
	{
		if (PhotonNetwork.connected)
		{
			GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
			return;
		}
		GUILayout.BeginArea(GuiRect);
		if (authFailed)
		{
			GUILayout.Label("Custom Authentication");
			GUILayout.Label("Failed. Debug Message (customizable in your service):");
			GUILayout.Label("'" + authDebugMessage + "'");
		}
		else
		{
			GUILayout.Label("Custom Authentication");
			GUILayout.Label("Photon Cloud can be setup to use an external service to verify players.");
			GUILayout.Label("By default, Photon Cloud allows anonymous connects. In the Dashboard, Custom Authentication can be made mandatory.");
			GUILayout.Label("Set PhotonNetwork.CustomAuthenticationValues before you call PhotonNetwork.ConnectUsingSetting().");
			GUILayout.Label("The demo service logs you in for: name == token.");
			GUILayout.Label("The script GUIFriendFinding will set a random username (independent from Custom Authentication).");
		}
		GUILayout.BeginHorizontal();
		authName = GUILayout.TextField(authName, GUILayout.Width(Screen.width / 4 - 5));
		GUILayout.FlexibleSpace();
		authToken = GUILayout.TextField(authToken, GUILayout.Width(Screen.width / 4 - 5));
		GUILayout.EndHorizontal();
		if (GUILayout.Button("Login with Custom Authentication"))
		{
			PhotonNetwork.AuthValues = new AuthenticationValues();
			PhotonNetwork.AuthValues.SetAuthParameters(authName, authToken);
			PhotonNetwork.ConnectUsingSettings("1.0");
			authFailed = false;
		}
		if (GUILayout.Button("Skip Custom Authentication"))
		{
			PhotonNetwork.AuthValues = null;
			PhotonNetwork.ConnectUsingSettings("1.0");
			authFailed = false;
		}
		GUILayout.Space(8f);
		if (GUILayout.Button("Open Dashboard (for Setup)"))
		{
			Application.OpenURL("https://cloud.exitgames.com/dashboard");
		}
		if (GUILayout.Button("Open Custom Auth doc page"))
		{
			Application.OpenURL("http://doc.exitgames.com/photon-cloud/CustomAuthentication");
		}
		GUILayout.EndArea();
	}
}
