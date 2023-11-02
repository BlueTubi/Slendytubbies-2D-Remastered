using Photon;
using UnityEngine;

public class ToggleFlashlight : Photon.MonoBehaviour
{
	public int temp;

	public PlayerHandler ph;

	private void Update()
	{
		if (base.photonView.isMine)
		{
			if (Input.GetKeyDown(KeyCode.Space))
			{
				if (ph.togglelight == 0)
				{
					ph.togglelight = 1;
				}
				else
				{
					ph.togglelight = 0;
				}
			}
			if (temp == 0 && ph.togglelight == 1)
			{
				base.transform.Find("Flashlight/Light").GetComponent<Light>().enabled = true;
				temp = 1;
			}
			if (temp == 1 && ph.togglelight == 0)
			{
				base.transform.Find("Flashlight/Light").GetComponent<Light>().enabled = false;
				temp = 0;
			}
		}
		else
		{
			if (temp == 0 && ph.togglelight == 1)
			{
				base.transform.Find("Flashlight/MPLight").GetComponent<Light>().enabled = true;
				temp = 1;
			}
			if (temp == 1 && ph.togglelight == 0)
			{
				base.transform.Find("Flashlight/MPLight").GetComponent<Light>().enabled = false;
				temp = 0;
			}
		}
	}
}
