using UnityEngine;

public class CharacterCustomization : MonoBehaviour
{
	public GUISkin guiSkin;

	public PlayerModel pm;

	public PlayerModel pm2;

	public Transform mainmenu;

	public int curskin;

	public int curhat;

	public int totskin;

	public int tothat;

	protected int totCustard;

	private int isUnlocked = 1;

	private string back = "Back";

	private string charactercustomization = "Character Customization";

	private string totalcustards = "Total Custards";

	private string unlockmorecustards = "Unlock more hats by collecting custards!";

	private string furcolor = "Fur Color";

	private string hat = "Hat";

	private string custards = "Custards";

	private string cost = "Cost";

	private string unlock = "Unlock";

	private void Awake()
	{
		totskin = pm.skincolor.Length;
		tothat = pm.hat.Length;
		curskin = CryptoPlayerPrefs.GetInt("furcolor");
		curhat = CryptoPlayerPrefs.GetInt("hat");
		totCustard = CryptoPlayerPrefs.GetInt("totCustard");
		pm.curskincolor = curskin;
		pm.curhat = curhat;
		pm.ChangeNow();
		pm2.curskincolor = curskin;
		pm2.curhat = curhat;
		pm2.ChangeNow();
	}

	private void OnEnable()
	{
		CryptoPlayerPrefs.SetInt("hat0", 1);
	}

	private void OnGUI()
	{
		GUI.skin = guiSkin;
		if (GUI.Button(new Rect(10f, 10f, 120f, 60f), back))
		{
			mainmenu.gameObject.SetActive(true);
			base.transform.gameObject.SetActive(false);
			if (CryptoPlayerPrefs.GetInt("hat" + curhat) == 1)
			{
				CryptoPlayerPrefs.SetInt("hat", curhat);
			}
		}
		GUI.Box(new Rect(10f, Screen.height / 2 + 10, Screen.width - 20, Screen.height / 2 - 20), charactercustomization);
		GUI.Box(new Rect(Screen.width - 300, Screen.height / 2 - 46, 300f, 23f), totalcustards + ": " + totCustard);
		GUI.Box(new Rect(Screen.width - 340, Screen.height / 2 - 23, 340f, 23f), unlockmorecustards);
		GUI.Label(new Rect(Screen.width / 2 - 90, Screen.height / 2 + 75, 180f, 100f), furcolor + "\n" + (curskin + 1) + " / " + totskin);
		if (GUI.Button(new Rect(Screen.width / 6, Screen.height / 2 + 70, 60f, 60f), "<") && curskin > 0)
		{
			curskin--;
			pm.curskincolor = curskin;
			pm2.curskincolor = curskin;
			CryptoPlayerPrefs.SetInt("furcolor", curskin);
			pm.ChangeNow();
			pm2.ChangeNow();
		}
		if (GUI.Button(new Rect(Screen.width - Screen.width / 4, Screen.height / 2 + 70, 60f, 60f), ">") && curskin < totskin - 1)
		{
			curskin++;
			pm.curskincolor = curskin;
			pm2.curskincolor = curskin;
			CryptoPlayerPrefs.SetInt("furcolor", curskin);
			pm.ChangeNow();
			pm2.ChangeNow();
		}
		GUI.Label(new Rect(Screen.width / 2 - 50, Screen.height / 2 + 146, 100f, 100f), hat + "\n" + (curhat + 1) + " / " + tothat);
		if (GUI.Button(new Rect(Screen.width / 6, Screen.height / 2 + 140, 60f, 60f), "<") && curhat > 0)
		{
			curhat--;
			pm.curhat = curhat;
			pm2.curhat = curhat;
			isUnlocked = CryptoPlayerPrefs.GetInt("hat" + curhat);
			pm.ChangeNow();
			pm2.ChangeNow();
		}
		if (GUI.Button(new Rect(Screen.width - Screen.width / 4, Screen.height / 2 + 140, 60f, 60f), ">") && curhat < tothat - 1)
		{
			curhat++;
			pm.curhat = curhat;
			pm2.curhat = curhat;
			isUnlocked = CryptoPlayerPrefs.GetInt("hat" + curhat);
			pm.ChangeNow();
			pm2.ChangeNow();
		}
		if (isUnlocked == 0)
		{
			GUI.Label(new Rect(Screen.width / 6 - 75, Screen.height / 2 - 90, 300f, 30f), cost + ": 10 " + custards);
			if (GUI.Button(new Rect(Screen.width / 6 - 30, Screen.height / 2 - 60, 200f, 60f), unlock))
			{
				isUnlocked = 1;
				pm.curhat = curhat;
				pm2.curhat = curhat;
				CryptoPlayerPrefs.SetInt("hat" + curhat, 1);
				pm.ChangeNow();
				pm2.ChangeNow();
			}
		}
	}
}
