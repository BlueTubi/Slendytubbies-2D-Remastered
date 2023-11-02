using UnityEngine;

public class KillPlayer : MonoBehaviour
{
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.transform.tag == "Player" && other.gameObject.GetComponent<PhotonView>().isMine)
		{
			PlayerHealth component = other.gameObject.GetComponent<PlayerHealth>();
			component.KillRequest();
		}
	}
}
