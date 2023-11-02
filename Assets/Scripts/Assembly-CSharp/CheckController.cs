using UnityEngine;

public class CheckController : MonoBehaviour
{
	public bool isMobile;

	private void Start()
	{
		if (GameObject.Find("ControllerType").GetComponent<ControllerType>().isMobile)
		{
			base.gameObject.GetComponent<PlayerMovementMobile>().enabled = true;
			base.gameObject.GetComponent<PlayerFlashlightMobile>().enabled = true;
			base.transform.Find("GUI Camera 1").gameObject.active = true;
		}
		else
		{
			base.gameObject.GetComponent<PlayerMovementPC>().enabled = true;
		}
	}
}
