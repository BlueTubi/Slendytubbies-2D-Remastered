using Photon;
using UnityEngine;

public class CustardsRemaining : Photon.MonoBehaviour
{
	public GameObject textobj;

	public bool canCollect;

	private void Start()
	{
		textobj = GameObject.Find("CustardsRemaining");
	}

	private void Update()
	{
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.GetComponent<PhotonView>() != null && other.transform.tag == "Player" && other.GetComponent<PhotonView>().isMine)
		{
			canCollect = true;
		}
		if (other.transform.tag == "Player" && base.photonView.isMine)
		{
			base.photonView.RPC("CollectNow", PhotonTargets.AllBuffered);
		}
	}

	[RPC]
	private void CollectNow()
	{
		if (canCollect)
		{
			int @int = CryptoPlayerPrefs.GetInt("totCustard");
			CryptoPlayerPrefs.SetInt("totCustard", @int + 1);
		}
		textobj.GetComponent<CustardCounter>().showcounter = true;
		if (base.photonView.isMine)
		{
			PhotonNetwork.Destroy(base.gameObject);
		}
	}
}
