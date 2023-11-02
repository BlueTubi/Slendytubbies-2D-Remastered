using Photon;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(PhotonView))]
public class OnAwakePhysicsSettings : Photon.MonoBehaviour
{
	private void Awake()
	{
		if (!base.photonView.isMine)
		{
			Rigidbody component = GetComponent<Rigidbody>();
			if (component != null)
			{
				component.isKinematic = true;
			}
		}
	}
}
