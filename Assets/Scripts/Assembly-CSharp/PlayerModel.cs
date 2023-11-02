using UnityEngine;

public class PlayerModel : MonoBehaviour
{
	public Color[] skincolor;

	public Texture2D[] hat;

	public int curskincolor;

	public int curhat;

	public void ChangeNow()
	{
		if (Application.loadedLevelName == "MainMenu")
		{
			GetComponent<Renderer>().materials[0].color = skincolor[curskincolor];
			if (CryptoPlayerPrefs.GetInt("hat" + curhat) == 1)
			{
				GetComponent<Renderer>().materials[2].color = Color.white;
			}
			else
			{
				GetComponent<Renderer>().materials[2].color = Color.black;
			}
			GetComponent<Renderer>().materials[2].mainTexture = hat[curhat];
		}
		else
		{
			GetComponent<Renderer>().materials[0].color = skincolor[curskincolor];
			GetComponent<Renderer>().materials[2].color = Color.white;
			GetComponent<Renderer>().materials[2].mainTexture = hat[curhat];
		}
	}
}
