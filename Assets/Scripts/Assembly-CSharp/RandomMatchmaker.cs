using Photon;
using UnityEngine;

public class RandomMatchmaker : Photon.MonoBehaviour
{
	private PhotonView myPhotonView;

	private void Start()
	{
		PhotonNetwork.ConnectUsingSettings("0.1");
	}

	private void OnJoinedLobby()
	{
		Debug.Log("JoinRandom");
		PhotonNetwork.JoinRandomRoom();
	}

	private void OnPhotonRandomJoinFailed()
	{
		PhotonNetwork.CreateRoom(null);
	}

	private void OnJoinedRoom()
	{
		GameObject gameObject = PhotonNetwork.Instantiate("monsterprefab", Vector3.zero, Quaternion.identity, 0);
		gameObject.GetComponent<myThirdPersonController>().isControllable = true;
		myPhotonView = gameObject.GetComponent<PhotonView>();
	}

	private void OnGUI()
	{
		GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
		if (PhotonNetwork.connectionStateDetailed == PeerState.Joined)
		{
			bool flag = GameLogic.playerWhoIsIt == PhotonNetwork.player.ID;
			if (flag && GUILayout.Button("Marco!"))
			{
				myPhotonView.RPC("Marco", PhotonTargets.All);
			}
			if (!flag && GUILayout.Button("Polo!"))
			{
				myPhotonView.RPC("Polo", PhotonTargets.All);
			}
		}
	}
}
