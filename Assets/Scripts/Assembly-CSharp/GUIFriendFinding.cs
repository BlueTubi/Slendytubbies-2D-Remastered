using UnityEngine;

public class GUIFriendFinding : MonoBehaviour
{
	private string[] friendListOfSomeCommunity;

	public Rect GuiRect;

	private void Start()
	{
		PhotonNetwork.playerName = "usr" + Random.Range(0, 9);
		friendListOfSomeCommunity = FetchFriendsFromCommunity();
		GuiRect = new Rect(Screen.width / 4, 80f, Screen.width / 2, Screen.height - 100);
	}

	public static string[] FetchFriendsFromCommunity()
	{
		string[] array = new string[9];
		int num = 0;
		for (int i = 0; i < array.Length; i++)
		{
			string text = "usr" + num++;
			if (text.Equals(PhotonNetwork.playerName))
			{
				text = "usr" + num++;
			}
			array[i] = text;
		}
		return array;
	}

	public void OnUpdatedFriendList()
	{
		Debug.Log("OnUpdatedFriendList is called when the list PhotonNetwork.Friends is refreshed.");
	}

	public void OnGUI()
	{
		if (!PhotonNetwork.insideLobby)
		{
			return;
		}
		GUILayout.BeginArea(GuiRect);
		GUILayout.Label("Your (random) name: " + PhotonNetwork.playerName);
		GUILayout.Label("Your friends: " + string.Join(", ", friendListOfSomeCommunity));
		GUILayout.BeginHorizontal();
		if (GUILayout.Button("Find Friends"))
		{
			PhotonNetwork.FindFriends(friendListOfSomeCommunity);
		}
		if (GUILayout.Button("Create Room"))
		{
			PhotonNetwork.CreateRoom(null);
		}
		GUILayout.EndHorizontal();
		if (PhotonNetwork.Friends != null)
		{
			foreach (FriendInfo friend in PhotonNetwork.Friends)
			{
				GUILayout.BeginHorizontal();
				GUILayout.Label(friend.ToString());
				if (friend.IsInRoom && GUILayout.Button("join"))
				{
					PhotonNetwork.JoinRoom(friend.Room);
				}
				GUILayout.EndHorizontal();
			}
		}
		GUILayout.EndArea();
	}
}
