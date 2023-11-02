using System.Collections.Generic;
using Photon;
using UnityEngine;

public class PlayerHandler : Photon.MonoBehaviour
{
	public List<GameObject> remoteObjectsToDeactivate;

	public List<UnityEngine.MonoBehaviour> remoteScriptsToDeactivate;

	public List<GameObject> localObjectsToDeactivate;

	public List<UnityEngine.MonoBehaviour> localScriptsToDeactivate;

	public Transform flashlight;

	public int togglelight;

	public int run;

	public PlayerMovementPC pmpc;

	public PlayerMovementMobile pmm;

	public MeshRenderer model;

	private bool ismobile;

	private int templight;

	private CharacterController cc;

	private Vector3 correctPlayerPos = new Vector3(0f, -100f, 0f);

	private Quaternion correctPlayerRot = Quaternion.identity;

	private int correctLight;

	private int toggleRun;

	private void Start()
	{
		cc = GetComponent<CharacterController>();
		if (!base.photonView.isMine)
		{
			for (int i = 0; i < remoteObjectsToDeactivate.Count; i++)
			{
				Object.Destroy(remoteObjectsToDeactivate[i]);
			}
			for (int j = 0; j < remoteScriptsToDeactivate.Count; j++)
			{
				Object.Destroy(remoteScriptsToDeactivate[j]);
			}
			if (base.gameObject.tag != "Monster")
			{
				cc.enabled = false;
				base.gameObject.AddComponent<BoxCollider>();
				base.gameObject.GetComponent<BoxCollider>().size = new Vector3(0.5f, 10f, 0.2f);
				base.gameObject.AddComponent<Rigidbody>();
				base.gameObject.GetComponent<Rigidbody>().useGravity = false;
				base.gameObject.GetComponent<Rigidbody>().isKinematic = true;
			}
		}
		else
		{
			if (GameObject.Find("ControllerType").GetComponent<ControllerType>().isMobile)
			{
				ismobile = true;
			}
			for (int k = 0; k < localObjectsToDeactivate.Count; k++)
			{
				Object.Destroy(localObjectsToDeactivate[k]);
			}
			for (int l = 0; l < localScriptsToDeactivate.Count; l++)
			{
				Object.Destroy(localScriptsToDeactivate[l]);
			}
		}
	}

	private void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if (stream.isWriting)
		{
			stream.SendNext(base.transform.position);
			stream.SendNext(flashlight.rotation);
			stream.SendNext(togglelight);
			stream.SendNext(run);
		}
		else
		{
			correctPlayerPos = (Vector3)stream.ReceiveNext();
			correctPlayerRot = (Quaternion)stream.ReceiveNext();
			correctLight = (int)stream.ReceiveNext();
			toggleRun = (int)stream.ReceiveNext();
		}
	}

	private void Update()
	{
		if (!base.photonView.isMine)
		{
			base.transform.position = Vector3.Lerp(base.transform.position, correctPlayerPos, Time.deltaTime * 8f);
			flashlight.rotation = Quaternion.Lerp(flashlight.rotation, correctPlayerRot, Time.deltaTime * 8f);
			togglelight = correctLight;
			run = toggleRun;
		}
		else if (!ismobile)
		{
			if (pmpc.isrunning)
			{
				run = 2;
			}
			else
			{
				run = 0;
			}
		}
		else if (pmm.isrunning)
		{
			run = 2;
		}
		else
		{
			run = 0;
		}
		if (base.gameObject.tag != "Monster" && togglelight != templight)
		{
			if (togglelight == 0)
			{
				model.GetComponent<Renderer>().materials[0].shader = Shader.Find("Transparent/VertexLit");
				model.GetComponent<Renderer>().materials[1].shader = Shader.Find("Transparent/VertexLit");
				model.GetComponent<Renderer>().materials[2].shader = Shader.Find("Transparent/VertexLit");
				templight = 0;
			}
			if (togglelight == 1)
			{
				model.GetComponent<Renderer>().materials[0].shader = Shader.Find("AlphaSelfIllum");
				model.GetComponent<Renderer>().materials[1].shader = Shader.Find("AlphaSelfIllum");
				model.GetComponent<Renderer>().materials[2].shader = Shader.Find("AlphaSelfIllum");
				templight = 1;
			}
		}
	}
}
