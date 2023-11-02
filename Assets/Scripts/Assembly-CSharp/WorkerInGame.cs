using Photon;
using UnityEngine;

public class WorkerInGame : Photon.MonoBehaviour
{
	public Transform playerPrefab;

	public void Awake()
	{
		if (!PhotonNetwork.connected)
		{
			Application.LoadLevel(WorkerMenu.SceneNameMenu);
		}
		else
		{
			PhotonNetwork.Instantiate(playerPrefab.name, base.transform.position, Quaternion.identity, 0);
		}
	}

	public void OnGUI()
	{
		if (GUILayout.Button("Return to Lobby"))
		{
			PhotonNetwork.LeaveRoom();
		}
	}

	public void OnMasterClientSwitched(PhotonPlayer player)
	{
		Debug.Log("OnMasterClientSwitched: " + player);
		InRoomChat component = GetComponent<InRoomChat>();
		if (component != null)
		{
			string newLine = ((!player.isLocal) ? (player.name + " is Master Client now.") : "You are Master Client now.");
			component.AddLine(newLine);
		}
	}

	public void OnLeftRoom()
	{
		Debug.Log("OnLeftRoom (local)");
		Application.LoadLevel(WorkerMenu.SceneNameMenu);
	}

	public void OnDisconnectedFromPhoton()
	{
		Debug.Log("OnDisconnectedFromPhoton");
		Application.LoadLevel(WorkerMenu.SceneNameMenu);
	}

	public void OnPhotonInstantiate(PhotonMessageInfo info)
	{
		Debug.Log("OnPhotonInstantiate " + info.sender);
	}

	public void OnPhotonPlayerConnected(PhotonPlayer player)
	{
		Debug.Log("OnPhotonPlayerConnected: " + player);
	}

	public void OnPhotonPlayerDisconnected(PhotonPlayer player)
	{
		Debug.Log("OnPlayerDisconneced: " + player);
	}

	public void OnFailedToConnectToPhoton()
	{
		Debug.Log("OnFailedToConnectToPhoton");
		Application.LoadLevel(WorkerMenu.SceneNameMenu);
	}
}
