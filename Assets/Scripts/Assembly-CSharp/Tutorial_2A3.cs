using Photon;
using UnityEngine;

public class Tutorial_2A3 : Photon.MonoBehaviour
{
	private Vector3 lastPosition;

	private void Update()
	{
		if (PhotonNetwork.isMasterClient)
		{
			Vector3 vector = new Vector3(-1f * Input.GetAxis("Vertical"), 0f, Input.GetAxis("Horizontal"));
			float num = 5f;
			base.transform.Translate(num * vector * Time.deltaTime);
			if (Vector3.Distance(base.transform.position, lastPosition) >= 0.05f)
			{
				lastPosition = base.transform.position;
				base.photonView.RPC("SetPosition", PhotonTargets.Others, base.transform.position);
			}
		}
	}

	[RPC]
	private void SetPosition(Vector3 newPos)
	{
		base.transform.position = newPos;
	}
}
