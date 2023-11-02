using System.Collections;
using UnityEngine;

public class VictoryScreen : MonoBehaviour
{
	public GUISkin guiskin1;

	public GUISkin guiskin2;

	public Color[] colors;

	public bool changed;

	private string congradulations = "Congratulations!";

	private string youcollected = "You collected all teletubby custards!";

	private string mainmenu = "Main Menu";

	private void Awake()
	{
		if (PlayerPrefs.GetInt("language") == 1)
		{
			congradulations = "¡Felicidades!";
			mainmenu = "Menú Principal";
			youcollected = "¡Conseguiste todas las tubipapillas!";
		}
	}

	private void Start()
	{
		Cursor.visible = true;
	}

	private void Update()
	{
		if (!changed)
		{
			StartCoroutine(changeColor());
			changed = true;
		}
	}

	private void OnGUI()
	{
		GUI.skin = guiskin1;
		GUI.Box(new Rect(Screen.width / 2 - 200, Screen.height / 2 - 21, 400f, 42f), congradulations + "\n" + youcollected);
		if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 + 31, 200f, 60f), mainmenu))
		{
			Application.LoadLevel("MainMenu");
		}
	}

	private IEnumerator changeColor()
	{
		Color[] array = colors;
		foreach (Color col in array)
		{
			yield return new WaitForSeconds(0.75f);
			RenderSettings.ambientLight = col;
		}
		changed = false;
	}
}
