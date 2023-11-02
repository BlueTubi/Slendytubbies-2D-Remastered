using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public bool isMobile;

	public GUISkin nullgui;

	public Transform model;

	public Transform flashlight;

	public float speed = 3f;

	public float walkspeed = 3f;

	public float runspeed = 6f;

	public bool waiting1;

	public bool moving;

	public float walkframespeed = 0.2f;

	public float xamount = 4f;

	public float yamount = 4f;

	public Texture2D uparrow;

	public Texture2D downarrow;

	public Texture2D leftarrow;

	public Texture2D rightarrow;

	public Texture2D runoff;

	public Texture2D runon;

	public bool isrunning;

	private float xcorrect;

	private float ycorrect;

	private float xtile;

	private float ytile;

	private Vector3 oldpos;

	private Vector3 pos;

	private void Awake()
	{
		oldpos = base.transform.position;
		xcorrect = 1f / xamount;
		ycorrect = 1f / yamount;
		model.GetComponent<Renderer>().material.mainTextureScale = new Vector2(xcorrect, ycorrect);
	}

	private void Start()
	{
	}

	private void Update()
	{
		if (!isMobile)
		{
			if (Input.GetKey(KeyCode.W))
			{
				base.transform.Translate(Vector3.forward * Time.deltaTime * speed, Space.World);
			}
			if (Input.GetKey(KeyCode.S))
			{
				base.transform.Translate(Vector3.back * Time.deltaTime * speed, Space.World);
			}
			if (Input.GetKey(KeyCode.A))
			{
				base.transform.Translate(Vector3.left * Time.deltaTime * speed, Space.World);
			}
			if (Input.GetKey(KeyCode.D))
			{
				base.transform.Translate(Vector3.right * Time.deltaTime * speed, Space.World);
			}
			if (Input.GetKey(KeyCode.LeftShift))
			{
				speed = runspeed;
				isrunning = true;
			}
			else
			{
				speed = walkspeed;
				isrunning = false;
			}
		}
		else if (isrunning)
		{
			speed = runspeed;
		}
		else
		{
			speed = walkspeed;
		}
		if (oldpos != base.transform.position)
		{
			if (!moving && !waiting1)
			{
				StartCoroutine("AnimateMovement");
			}
		}
		else if (moving)
		{
			moving = false;
			StopCoroutine("AnimateMovement");
			model.GetComponent<Renderer>().material.mainTextureOffset = new Vector2(0f, ytile);
		}
		pos = base.transform.position;
		if (oldpos.x > pos.x)
		{
			ytile = 1f / yamount + ycorrect;
			Vector3 localEulerAngles = new Vector3(0f, 180f, 0f);
			flashlight.localEulerAngles = localEulerAngles;
		}
		if (oldpos.x < pos.x)
		{
			ytile = 1f / yamount;
			Vector3 localEulerAngles2 = new Vector3(0f, 0f, 0f);
			flashlight.localEulerAngles = localEulerAngles2;
		}
		if (oldpos.z + 0.01f < pos.z)
		{
			ytile = 1f / yamount - ycorrect;
			Vector3 localEulerAngles3 = new Vector3(0f, -90f, 0f);
			flashlight.localEulerAngles = localEulerAngles3;
		}
		if (oldpos.z - 0.01f > pos.z)
		{
			ytile = 1f / yamount + ycorrect + ycorrect;
			Vector3 localEulerAngles4 = new Vector3(0f, 90f, 0f);
			flashlight.localEulerAngles = localEulerAngles4;
		}
	}

	private void LateUpdate()
	{
		oldpos = base.transform.position;
	}

	private IEnumerator AnimateMovement()
	{
		moving = true;
		if (xtile >= 1f - xcorrect)
		{
			xtile = 0f - xcorrect;
		}
		model.GetComponent<Renderer>().material.mainTextureOffset = new Vector2(xtile + xcorrect, ytile);
		xtile = model.GetComponent<Renderer>().material.mainTextureOffset.x;
		ytile = model.GetComponent<Renderer>().material.mainTextureOffset.y;
		yield return new WaitForSeconds(walkframespeed);
		moving = false;
	}

	private void OnGUI()
	{
		GUI.skin = nullgui;
		if (!isMobile)
		{
			return;
		}
		GUI.DrawTexture(new Rect(Screen.width - 150, Screen.height - 200, 50f, 50f), uparrow);
		if (GUI.RepeatButton(new Rect(Screen.width - 150, Screen.height - 200, 50f, 50f), string.Empty))
		{
			base.transform.Translate(Vector3.forward * Time.deltaTime * speed, Space.World);
		}
		GUI.DrawTexture(new Rect(Screen.width - 150, Screen.height - 100, 50f, 50f), downarrow);
		if (GUI.RepeatButton(new Rect(Screen.width - 150, Screen.height - 100, 50f, 50f), string.Empty))
		{
			base.transform.Translate(Vector3.back * Time.deltaTime * speed, Space.World);
		}
		GUI.DrawTexture(new Rect(Screen.width - 200, Screen.height - 150, 50f, 50f), leftarrow);
		if (GUI.RepeatButton(new Rect(Screen.width - 200, Screen.height - 150, 50f, 50f), string.Empty))
		{
			base.transform.Translate(Vector3.left * Time.deltaTime * speed, Space.World);
		}
		GUI.DrawTexture(new Rect(Screen.width - 100, Screen.height - 150, 50f, 50f), rightarrow);
		if (GUI.RepeatButton(new Rect(Screen.width - 100, Screen.height - 150, 50f, 50f), string.Empty))
		{
			base.transform.Translate(Vector3.right * Time.deltaTime * speed, Space.World);
		}
		if (isrunning)
		{
			GUI.DrawTexture(new Rect(Screen.width - 150, Screen.height - 150, 50f, 50f), runon);
			if (GUI.Button(new Rect(Screen.width - 150, Screen.height - 150, 50f, 50f), string.Empty))
			{
				isrunning = false;
			}
		}
		else
		{
			GUI.DrawTexture(new Rect(Screen.width - 150, Screen.height - 150, 50f, 50f), runoff);
			if (GUI.Button(new Rect(Screen.width - 150, Screen.height - 150, 50f, 50f), string.Empty))
			{
				isrunning = true;
			}
		}
	}
}
