using UnityEngine;

public class LanguageSelect : MonoBehaviour
{
	public Texture2D title;

	public Texture2D english;

	public Texture2D spanish;

	public GUISkin guiSkin;

	private string notice = "Notice: This game was downloaded from ZeoGames.net/ZeoWorks.com.\nPlease do not re-upload, If you downloaded the game from any other website, please report it to us on our Facebook page (link on the menu).";

	private string continue1 = "Continue";

	private bool hasSelected;

	private void Awake()
	{
		if (PlayerPrefs.GetInt("language") == 1)
		{
			notice = "Aviso: Este juego ha sido descargado de ZeoGames.net/ZeoWorks.com.\nPor favor no re-subir. Si descargaste este juego de otro sitio web, por favor reportenlo en nuestra página de Facebook (link en el menu).";
			continue1 = "Continuar";
		}
	}

	private void OnGUI()
	{
		GUI.skin = guiSkin;
		if (!hasSelected)
		{
			GUI.DrawTexture(new Rect(Screen.width / 2 - 200, 10f, 400f, 150f), title);
			if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 90, 200f, 75f), english))
			{
				if (!hasSelected)
				{
					SetLanguageEnglish();
				}
				notice = "Notice: This game was downloaded from ZeoGames.net/ZeoWorks.com.\nPlease do not re-upload, If you downloaded the game from any other website, please report it to us on our Facebook page (link on the menu).";
				continue1 = "Continue";
				hasSelected = true;
			}
			if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 + 10, 200f, 75f), spanish))
			{
				if (!hasSelected)
				{
					SetLanguageSpanish();
				}
				notice = "Aviso: Este juego ha sido descargado de ZeoGames.net/ZeoWorks.com.\nPor favor no re-subir. Si descargaste este juego de otro sitio web, por favor reportenlo en nuestra página de Facebook (link en el menu).";
				continue1 = "Continuar";
				hasSelected = true;
			}
		}
		else
		{
			GUI.Label(new Rect(Screen.width / 2 - 200, Screen.height / 2 - 75, 400f, 75f), notice);
			if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 + 10, 200f, 60f), continue1))
			{
				Application.LoadLevel(1);
			}
		}
	}

	private void SetLanguageEnglish()
	{
		if (!Application.isMobilePlatform)
		{
			PlayerPrefs.SetInt("language", 0);
		}
	}

	private void SetLanguageSpanish()
	{
		if (!Application.isMobilePlatform)
		{
			PlayerPrefs.SetInt("language", 1);
		}
	}
}
