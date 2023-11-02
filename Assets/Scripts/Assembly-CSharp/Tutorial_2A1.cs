using Photon;
using UnityEngine;

public class Tutorial_2A1 : Photon.MonoBehaviour
{
	private void Update()
	{
		if (PhotonNetwork.isMasterClient)
		{
			Vector3 vector = new Vector3(-1f * Input.GetAxis("Vertical"), 0f, Input.GetAxis("Horizontal"));
			float num = 5f;
			base.transform.Translate(num * vector * Time.deltaTime);
		}
	}
}
