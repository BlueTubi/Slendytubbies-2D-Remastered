using Photon;
using UnityEngine;

public class Tutorial_2B_Playerscript : Photon.MonoBehaviour
{
	private void Awake()
	{
		if (!base.photonView.isMine)
		{
			base.enabled = false;
		}
	}

	private void Update()
	{
		if (base.photonView.isMine)
		{
			Vector3 vector = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
			float num = 5f;
			base.transform.Translate(num * vector * Time.deltaTime);
		}
	}

	private void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if (stream.isWriting)
		{
			stream.SendNext(base.transform.position);
		}
		else
		{
			base.transform.position = (Vector3)stream.ReceiveNext();
		}
	}
}
