using Photon;
using UnityEngine;

public class ChangeSkin : Photon.MonoBehaviour
{
	public PlayerModel pm;

	public int furcolor;

	public int hat;

	private void Awake()
	{
		if (base.photonView.isMine)
		{
			furcolor = CryptoPlayerPrefs.GetInt("furcolor");
			hat = CryptoPlayerPrefs.GetInt("hat");
			base.photonView.RPC("SetSkin", PhotonTargets.AllBuffered, furcolor, hat);
		}
	}

	private void BeginChange()
	{
		pm.curskincolor = furcolor;
		pm.curhat = hat;
		pm.ChangeNow();
	}

	[RPC]
	private void SetSkin(int newcolor, int newhat)
	{
		if (base.photonView.isMine)
		{
			newcolor = furcolor;
			newhat = hat;
		}
		furcolor = newcolor;
		hat = newhat;
		BeginChange();
	}
}
