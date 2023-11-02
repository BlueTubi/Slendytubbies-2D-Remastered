using ExitGames.Client.Photon;
using Photon;
using UnityEngine;

public class Connect1A : Photon.MonoBehaviour
{
	private void Awake()
	{
		PhotonNetwork.ConnectUsingSettings("1.0");
	}

	private void OnGUI()
	{
		if (!PhotonNetwork.connected && !PhotonNetwork.connecting)
		{
			GUILayout.Label("Connection status: Disconnected");
			GUILayout.BeginVertical();
			if (GUILayout.Button("Connect"))
			{
				PhotonNetwork.ConnectUsingSettings("1.0");
			}
			GUILayout.EndVertical();
		}
		else if (PhotonNetwork.connected)
		{
			GUILayout.Label("Connection status: Connected");
			GUILayout.Label("Ping to server: " + PhotonNetwork.GetPing());
			if (GUILayout.Button("Disconnect"))
			{
				PhotonNetwork.Disconnect();
			}
		}
		else
		{
			GUILayout.Label("Connection status: " + PhotonNetwork.connectionState);
		}
	}

	private void OnConnectedToPhoton()
	{
		Debug.Log("This client has connected to a server");
	}

	private void OnDisconnectedFromPhoton()
	{
		Debug.Log("This client has disconnected from the server");
	}

	private void OnFailedToConnectToPhoton(StatusCode status)
	{
		Debug.Log("Failed to connect to Photon: " + status);
	}

	private void OnCreatedRoom()
	{
		Debug.Log("We have created a room.");
	}

	private void OnJoinedRoom()
	{
		Debug.Log("We have joined a room.");
	}

	private void OnLeftRoom()
	{
		Debug.Log("This client has left a game room.");
	}

	private void OnPhotonCreateRoomFailed()
	{
		Debug.Log("A CreateRoom call failed, most likely the room name is already in use.");
	}

	private void OnPhotonJoinRoomFailed()
	{
		Debug.Log("A JoinRoom call failed, most likely the room name does not exist or is full.");
	}

	private void OnPhotonRandomJoinFailed()
	{
		Debug.Log("A JoinRandom room call failed, most likely there are no rooms available.");
	}

	private void OnJoinedLobby()
	{
		Debug.Log("We joined the lobby.");
	}

	private void OnLeftLobby()
	{
		Debug.Log("We left the lobby.");
	}

	private void OnReceivedRoomList()
	{
		Debug.Log("We received a new room list, total rooms: " + PhotonNetwork.GetRoomList().Length);
	}

	private void OnReceivedRoomListUpdate()
	{
		Debug.Log("We received a room list update, total rooms now: " + PhotonNetwork.GetRoomList().Length);
	}

	private void OnPhotonPlayerConnected(PhotonPlayer player)
	{
		Debug.Log("Player connected: " + player);
	}

	private void OnPhotonPlayerDisconnected(PhotonPlayer player)
	{
		Debug.Log("Player disconnected: " + player);
	}

	private void OnMasterClientSwitched(PhotonPlayer newMaster)
	{
		Debug.Log("The old masterclient left, we have a new masterclient: " + newMaster);
	}

	private void OnPhotonInstantiate(PhotonMessageInfo info)
	{
		Debug.Log("New object instantiated by " + info.sender);
	}

	private void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
	}
}
