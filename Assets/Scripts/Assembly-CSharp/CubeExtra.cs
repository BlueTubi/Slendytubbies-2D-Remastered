using Photon;
using UnityEngine;

[RequireComponent(typeof(PhotonView))]
public class CubeExtra : Photon.MonoBehaviour
{
	private Vector3 latestCorrectPos = Vector3.zero;

	private Vector3 lastMovement = Vector3.zero;

	private float lastTime;

	public void Awake()
	{
		if (base.photonView.isMine)
		{
			base.enabled = false;
		}
		latestCorrectPos = base.transform.position;
	}

	public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if (stream.isWriting)
		{
			Vector3 obj = base.transform.localPosition;
			Quaternion obj2 = base.transform.localRotation;
			stream.Serialize(ref obj);
			stream.Serialize(ref obj2);
			return;
		}
		Vector3 obj3 = Vector3.zero;
		Quaternion obj4 = Quaternion.identity;
		stream.Serialize(ref obj3);
		stream.Serialize(ref obj4);
		lastMovement = (obj3 - latestCorrectPos) / (Time.time - lastTime);
		lastTime = Time.time;
		latestCorrectPos = obj3;
		base.transform.position = latestCorrectPos;
	}

	public void Update()
	{
		base.transform.localPosition += lastMovement * Time.deltaTime;
	}
}
