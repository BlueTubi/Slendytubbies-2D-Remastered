using Photon;
using UnityEngine;

public class Tutorial_2B_Spawnscript : Photon.MonoBehaviour
{
	public Transform playerPrefab;

	private void OnJoinedRoom()
	{
		Spawnplayer();
	}

	private void Spawnplayer()
	{
		Vector3 position = base.transform.position + new Vector3(Random.Range(-3, 3), 0f, Random.Range(-3, 3));
		PhotonNetwork.Instantiate(playerPrefab.name, position, base.transform.rotation, 0);
	}

	private void OnPhotonPlayerDisconnected(PhotonPlayer player)
	{
		Debug.Log("Clean up after player " + player);
	}

	private void OnDisconnectedFromPhoton()
	{
		Debug.Log("Clean up a bit after server quit");
		Application.LoadLevel(Application.loadedLevel);
	}
}
