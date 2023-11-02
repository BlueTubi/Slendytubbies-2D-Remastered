using System;
using UnityEngine;

public class ClickDetector : MonoBehaviour
{
	private void Update()
	{
		if (PhotonNetwork.player.ID == GameLogic.playerWhoIsIt && Input.GetButton("Fire1"))
		{
			GameObject gameObject = RaycastObject(Input.mousePosition);
			if (gameObject != null && gameObject != base.gameObject && gameObject.name.Equals("monsterprefab(Clone)", StringComparison.OrdinalIgnoreCase))
			{
				PhotonView component = gameObject.transform.root.GetComponent<PhotonView>();
				GameLogic.TagPlayer(component.owner.ID);
			}
		}
	}

	private GameObject RaycastObject(Vector2 screenPos)
	{
		Camera main = Camera.main;
		RaycastHit hitInfo;
		if (Physics.Raycast(main.ScreenPointToRay(screenPos), out hitInfo, 200f))
		{
			return hitInfo.collider.gameObject;
		}
		return null;
	}
}
