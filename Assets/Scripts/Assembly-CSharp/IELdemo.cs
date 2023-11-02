using UnityEngine;

public class IELdemo : MonoBehaviour
{
	public Transform[] cubes;

	public void Awake()
	{
		if (!PhotonNetwork.connected)
		{
			PhotonNetwork.autoJoinLobby = false;
			PhotonNetwork.ConnectUsingSettings("1");
		}
	}

	public void OnConnectedToMaster()
	{
		PhotonNetwork.JoinRandomRoom();
	}

	public void OnPhotonRandomJoinFailed()
	{
		PhotonNetwork.CreateRoom(null, new RoomOptions
		{
			maxPlayers = 4
		}, null);
	}

	public void OnJoinedRoom()
	{
	}

	public void OnCreatedRoom()
	{
		Application.LoadLevel(Application.loadedLevel);
	}

	public void Update()
	{
		if (PhotonNetwork.isMasterClient)
		{
			float x = Input.GetAxis("Horizontal") * Time.deltaTime * 15f;
			Transform[] array = cubes;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].position += new Vector3(x, 0f, 0f);
			}
		}
	}

	public void OnGUI()
	{
		GUILayout.Space(10f);
		if (PhotonNetwork.isMasterClient)
		{
			GUILayout.Label("Move the cubes with the left and right keys. Run another client to check movement (smoothing) behaviour.");
			GUILayout.Label("Ping: " + PhotonNetwork.GetPing());
		}
		else if (PhotonNetwork.isNonMasterClientInRoom)
		{
			GUILayout.Label("Check how smooth the movement is");
			GUILayout.Label("Ping: " + PhotonNetwork.GetPing());
		}
		else
		{
			GUILayout.Label("Not connected..." + PhotonNetwork.connectionStateDetailed);
		}
	}
}
