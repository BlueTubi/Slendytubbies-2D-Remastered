using System.Collections;
using UnityEngine;

public class Login : MonoBehaviour
{
	public GUISkin guiSkin;

	private string user = "Username";

	private string password = "Password";

	public string[] accountInfo;

	public bool attempt;

	public bool isError;

	public bool noConnection;

	public GameObject title;

	private string back = "Back";

	private string login = "Login";

	private string username2 = "Username";

	private string password2 = "Password";

	private string tryagain = "Try Again";

	private string connecting = "Connecting To Server...";

	private string connect = "Connect";

	private string error1 = "Error: Incorrect Username / Password!";

	private string error2 = "Error: Could Not Connect To Server!";

	private void OnEnable()
	{
	}

	private void OnDisable()
	{
	}

	private void Awake()
	{
		if (PlayerPrefs.GetInt("language") == 1)
		{
			back = "Volver";
			login = "Iniciar Sesión";
			username2 = "Nombre de Usuario";
			password2 = "Contraseña";
			tryagain = "Intentar de Nuevo";
			connecting = "Conectando al Servidor...";
			connect = "Conectar";
			error1 = "Error: ¡Nombre de Usuario/Contraseña Incorrecta!";
			error2 = "Error: ¡No se pudo conectar al servidor!";
		}
	}

	private void OnGUI()
	{
		GUI.skin = guiSkin;
		if (GUI.Button(new Rect(10f, 10f, 120f, 60f), back))
		{
			base.enabled = false;
			GetComponent<MainMenu>().enabled = true;
		}
		GUI.Box(new Rect(Screen.width / 2 - 290, 200f, 580f, 250f), login);
		if (!noConnection)
		{
			if (!attempt)
			{
				GUI.Label(new Rect(Screen.width / 2 - 125, 250f, 250f, 20f), username2);
				user = GUI.TextField(new Rect(Screen.width / 2 - 125, 270f, 250f, 30f), user);
				GUI.Label(new Rect(Screen.width / 2 - 125, 320f, 250f, 20f), password2);
				password = GUI.PasswordField(new Rect(Screen.width / 2 - 125, 340f, 250f, 30f), password, "*"[0]);
				if (GUI.Button(new Rect(Screen.width / 2 - 125, 390f, 250f, 40f), connect))
				{
					StartCoroutine(loginNow());
				}
			}
			else if (!isError)
			{
				GUI.Label(new Rect(Screen.width / 2 - 200, 310f, 400f, 20f), connecting);
			}
			else
			{
				GUI.Label(new Rect(Screen.width / 2 - 200, 310f, 400f, 20f), error1);
				if (GUI.Button(new Rect(Screen.width / 2 - 125, 390f, 250f, 40f), tryagain))
				{
					noConnection = false;
					attempt = false;
					isError = false;
				}
			}
		}
		else
		{
			GUI.Label(new Rect(Screen.width / 2 - 200, 310f, 400f, 20f), error2);
			if (GUI.Button(new Rect(Screen.width / 2 - 125, 390f, 250f, 40f), tryagain))
			{
				noConnection = false;
				attempt = false;
				isError = false;
			}
		}
	}

	private IEnumerator loginNow()
	{
		attempt = true;
		WWWForm form = new WWWForm();
		string user2 = user;
		form.AddField("user", user2);
		form.AddField("password", password);
		WWW w = new WWW("http://zeoworks.com/getlogin.php", form);
		yield return w;
		if (w.error == null)
		{
			if (w.text == "0" || w.text == string.Empty)
			{
				isError = true;
				MonoBehaviour.print("wrong details..");
				yield break;
			}
			accountInfo = w.text.Split(","[0]);
			if (accountInfo[2] == string.Empty)
			{
				accountInfo[2] = "images/default_avatar.png";
			}
			if (accountInfo[2].Contains("./"))
			{
				accountInfo[2] = accountInfo[2].Replace("./", string.Empty);
			}
			CryptoPlayerPrefs.SetString("ZWName", accountInfo[1]);
			CryptoPlayerPrefs.SetString("PlayerType", accountInfo[3]);
			GetComponent<MainMenu>().haslogin = true;
			base.enabled = false;
			GetComponent<MainMenu>().enabled = true;
		}
		else
		{
			noConnection = true;
		}
	}
}
