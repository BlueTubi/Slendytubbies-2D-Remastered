using Photon;
using UnityEngine;

public class SmoothNPC : Photon.MonoBehaviour
{
	public int visual;

	private Vector3 correctPlayerPos = new Vector3(0f, -100f, 0f);

	private int correctvisual;

	private void Start()
	{
	}

	private void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if (stream.isWriting)
		{
			stream.SendNext(base.transform.position);
			stream.SendNext(visual);
		}
		else if (!base.photonView.isMine)
		{
			correctPlayerPos = (Vector3)stream.ReceiveNext();
			correctvisual = (int)stream.ReceiveNext();
		}
	}

	private void Update()
	{
		if (!base.photonView.isMine)
		{
			base.transform.position = Vector3.Lerp(base.transform.position, correctPlayerPos, Time.deltaTime * 8f);
			visual = correctvisual;
		}
	}
}
