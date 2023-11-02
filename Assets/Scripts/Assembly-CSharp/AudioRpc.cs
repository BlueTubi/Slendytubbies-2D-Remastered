using Photon;
using UnityEngine;

public class AudioRpc : Photon.MonoBehaviour
{
	public AudioClip marco;

	public AudioClip polo;

	[RPC]
	private void Marco()
	{
		if (base.enabled)
		{
			Debug.Log("Marco");
			GetComponent<AudioSource>().clip = marco;
			GetComponent<AudioSource>().Play();
		}
	}

	[RPC]
	private void Polo()
	{
		if (base.enabled)
		{
			Debug.Log("Polo");
			GetComponent<AudioSource>().clip = polo;
			GetComponent<AudioSource>().Play();
		}
	}

	private void OnApplicationFocus(bool focus)
	{
		base.enabled = focus;
	}
}
