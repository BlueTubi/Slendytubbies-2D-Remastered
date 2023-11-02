using Photon;
using UnityEngine;

public class ThirdPersonNetwork : Photon.MonoBehaviour
{
	private ThirdPersonCamera cameraScript;

	private ThirdPersonController controllerScript;

	private Vector3 correctPlayerPos = Vector3.zero;

	private Quaternion correctPlayerRot = Quaternion.identity;

	private void Awake()
	{
		cameraScript = GetComponent<ThirdPersonCamera>();
		controllerScript = GetComponent<ThirdPersonController>();
		if (base.photonView.isMine)
		{
			cameraScript.enabled = true;
			controllerScript.enabled = true;
		}
		else
		{
			cameraScript.enabled = false;
			controllerScript.enabled = true;
			controllerScript.isControllable = false;
		}
		base.gameObject.name = base.gameObject.name + base.photonView.viewID;
	}

	private void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if (stream.isWriting)
		{
			stream.SendNext((int)controllerScript._characterState);
			stream.SendNext(base.transform.position);
			stream.SendNext(base.transform.rotation);
		}
		else
		{
			controllerScript._characterState = (CharacterState)(int)stream.ReceiveNext();
			correctPlayerPos = (Vector3)stream.ReceiveNext();
			correctPlayerRot = (Quaternion)stream.ReceiveNext();
		}
	}

	private void Update()
	{
		if (!base.photonView.isMine)
		{
			base.transform.position = Vector3.Lerp(base.transform.position, correctPlayerPos, Time.deltaTime * 5f);
			base.transform.rotation = Quaternion.Lerp(base.transform.rotation, correctPlayerRot, Time.deltaTime * 5f);
		}
	}
}
