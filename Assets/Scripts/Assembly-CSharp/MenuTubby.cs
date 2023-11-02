using System.Collections;
using UnityEngine;

public class MenuTubby : MonoBehaviour
{
	public float speed;

	public bool ismoving;

	public Transform thelight;

	public float rot = 0.25f;

	public float pos1 = 0.25f;

	public float pos2 = 0.5f;

	public float pos3 = 0.75f;

	public float pos4;

	private void Awake()
	{
		CryptoPlayerPrefs.SetInt("hat0", 1);
		GetComponent<Renderer>().materials[0].mainTextureOffset = new Vector2(0f, rot);
		GetComponent<Renderer>().materials[1].mainTextureOffset = new Vector2(0f, rot);
		GetComponent<Renderer>().materials[2].mainTextureOffset = new Vector2(0f, 0.25f);
		int @int = CryptoPlayerPrefs.GetInt("furcolor");
		int int2 = CryptoPlayerPrefs.GetInt("hat");
		PlayerModel component = GetComponent<PlayerModel>();
		component.curskincolor = @int;
		component.curhat = int2;
		component.ChangeNow();
	}

	private void Update()
	{
		if (!ismoving)
		{
			StartCoroutine(MovingNow());
		}
	}

	private IEnumerator MovingNow()
	{
		ismoving = true;
		yield return new WaitForSeconds(speed);
		GetComponent<Renderer>().materials[0].mainTextureOffset = new Vector2(pos1, rot);
		GetComponent<Renderer>().materials[1].mainTextureOffset = new Vector2(pos1, rot);
		GetComponent<Renderer>().materials[2].mainTextureOffset = new Vector2(0f, 0.255f);
		if (thelight != null)
		{
			thelight.localPosition = new Vector3(0f, -0.14f, 0f);
		}
		yield return new WaitForSeconds(speed);
		GetComponent<Renderer>().materials[0].mainTextureOffset = new Vector2(pos2, rot);
		GetComponent<Renderer>().materials[1].mainTextureOffset = new Vector2(pos2, rot);
		GetComponent<Renderer>().materials[2].mainTextureOffset = new Vector2(0f, 0.25f);
		if (thelight != null)
		{
			thelight.localPosition = new Vector3(0f, -0.15f, 0f);
		}
		yield return new WaitForSeconds(speed);
		GetComponent<Renderer>().materials[0].mainTextureOffset = new Vector2(pos3, rot);
		GetComponent<Renderer>().materials[1].mainTextureOffset = new Vector2(pos3, rot);
		GetComponent<Renderer>().materials[2].mainTextureOffset = new Vector2(0f, 0.255f);
		if (thelight != null)
		{
			thelight.localPosition = new Vector3(-0.03f, -0.16f, 0f);
		}
		yield return new WaitForSeconds(speed);
		GetComponent<Renderer>().materials[0].mainTextureOffset = new Vector2(pos4, rot);
		GetComponent<Renderer>().materials[1].mainTextureOffset = new Vector2(pos4, rot);
		GetComponent<Renderer>().materials[2].mainTextureOffset = new Vector2(pos4, 0.25f);
		if (thelight != null)
		{
			thelight.localPosition = new Vector3(0f, -0.15f, 0f);
		}
		ismoving = false;
	}
}
