using Photon;
using UnityEngine;

[RequireComponent(typeof(PhotonView))]
public class TestGuiInstantiate : Photon.MonoBehaviour
{
	public string PrefabToInstantiate = "TestPrefab";

	public bool HideUI;

	public int GuiSpace;

	private GameObject lastInstantiateMine;

	private GameObject lastInstantiateScene;

	private int prefix;

	public void OnGUI()
	{
		if (HideUI)
		{
			return;
		}
		GUILayout.Space(GuiSpace);
		GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
		if (!PhotonNetwork.connected)
		{
			if (GUILayout.Button("Connect"))
			{
				PhotonNetwork.ConnectUsingSettings("1");
			}
		}
		else if (GUILayout.Button("Disconnect"))
		{
			PhotonNetwork.Disconnect();
		}
	}
}
