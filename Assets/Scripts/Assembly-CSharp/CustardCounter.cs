using System.Collections;
using UnityEngine;

public class CustardCounter : MonoBehaviour
{
	public bool showcounter;

	public int remainingamount;

	public bool isshowing;

	private void Start()
	{
	}

	private void Update()
	{
		if (showcounter)
		{
			remainingamount = GameObject.FindGameObjectsWithTag("custard").Length;
			if (PlayerPrefs.GetInt("language") == 1)
			{
				base.gameObject.GetComponent<GUIText>().text = remainingamount + " Tubipapillas Restantes";
			}
			else
			{
				base.gameObject.GetComponent<GUIText>().text = remainingamount + " Custards Remaining";
			}
			if (!isshowing)
			{
				StartCoroutine(ShowNow());
			}
		}
		if (Input.GetKeyDown(KeyCode.C))
		{
			showcounter = true;
		}
	}

	private IEnumerator ShowNow()
	{
		isshowing = true;
		base.gameObject.GetComponent<GUIText>().enabled = true;
		yield return new WaitForSeconds(5f);
		base.gameObject.GetComponent<GUIText>().enabled = false;
		showcounter = false;
		isshowing = false;
	}
}
