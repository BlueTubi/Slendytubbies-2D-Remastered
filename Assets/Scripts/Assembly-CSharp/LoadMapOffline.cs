using UnityEngine;

public class LoadMapOffline : MonoBehaviour
{
	private void Start()
	{
		PhotonNetwork.CreateRoom("Offline", new RoomOptions
		{
			maxPlayers = 1
		}, null);
	}

	public void OnCreatedRoom()
	{
		string @string = CryptoPlayerPrefs.GetString("Map");
		PhotonNetwork.offlineMode = true;
		PhotonNetwork.LoadLevel(@string);
	}
}
