using System.Collections;
using UnityEngine;

public class PlayerGraphics1 : MonoBehaviour
{
	public Transform model;

	public Transform flashlight;

	public bool moving;

	public bool waiting1;

	public float walkframespeed = 0.2f;

	public float framespeed = 0.2f;

	public float runframespeed = 0.05f;

	public float xamount = 4f;

	public float yamount = 4f;

	private float xcorrect;

	private float ycorrect;

	private float xtile;

	private float ytile;

	private Vector3 oldpos;

	private Vector3 pos;

	private Material mainbody;

	private Material face;

	private Material hat;

	public float hatpos;

	public Texture2D walkanimation;

	private void Awake()
	{
		oldpos = base.transform.position;
		xcorrect = 1f / xamount;
		ycorrect = 1f / yamount;
		mainbody = model.GetComponent<Renderer>().materials[0];
		face = model.GetComponent<Renderer>().materials[1];
		hat = model.GetComponent<Renderer>().materials[2];
		mainbody.mainTextureScale = new Vector2(xcorrect, ycorrect);
		face.mainTextureScale = new Vector2(xcorrect, ycorrect);
	}

	private void Start()
	{
	}

	private void Update()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		Plane plane = new Plane(Vector3.up, base.transform.position);
		float enter = 0f;
		if (plane.Raycast(ray, out enter))
		{
			Vector3 point = ray.GetPoint(enter);
			Quaternion to = Quaternion.LookRotation(point - base.transform.position);
			flashlight.rotation = Quaternion.Slerp(base.transform.rotation, to, 6f * Time.time);
		}
		base.transform.position = new Vector3(base.transform.position.x, 3f, base.transform.position.z);
		mainbody.mainTexture = walkanimation;
		framespeed = walkframespeed;
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
			mainbody.mainTextureOffset = new Vector2(0f, ytile);
			face.mainTextureOffset = new Vector2(0f, ytile);
		}
		Vector3 localEulerAngles = flashlight.localEulerAngles;
		pos = base.transform.position;
		if (localEulerAngles.y > 150f && localEulerAngles.y < 240f)
		{
			ytile = 1f / yamount + ycorrect + ycorrect;
			mainbody.mainTextureOffset = new Vector2(xtile, ytile);
			face.mainTextureOffset = new Vector2(xtile, ytile);
			hat.mainTextureOffset = new Vector2(0f, ytile + hatpos);
		}
		if (localEulerAngles.y > 240f && localEulerAngles.y < 330f)
		{
			ytile = 1f / yamount + ycorrect;
			mainbody.mainTextureOffset = new Vector2(xtile, ytile);
			face.mainTextureOffset = new Vector2(xtile, ytile);
			hat.mainTextureOffset = new Vector2(0f, ytile + hatpos);
		}
		if (localEulerAngles.y < 60f && localEulerAngles.y > -30f)
		{
			ytile = 1f / yamount - ycorrect;
			mainbody.mainTextureOffset = new Vector2(xtile, ytile);
			face.mainTextureOffset = new Vector2(xtile, ytile);
			hat.mainTextureOffset = new Vector2(0f, ytile + hatpos);
		}
		if (localEulerAngles.y > 60f && localEulerAngles.y < 150f)
		{
			ytile = 1f / yamount;
			mainbody.mainTextureOffset = new Vector2(xtile, ytile);
			face.mainTextureOffset = new Vector2(xtile, ytile);
			hat.mainTextureOffset = new Vector2(0f, ytile + hatpos);
		}
	}

	private void LateUpdate()
	{
	}

	private IEnumerator AnimateMovement()
	{
		moving = true;
		if (xtile >= 1f - xcorrect)
		{
			xtile = 0f - xcorrect;
		}
		if (hatpos == 0f)
		{
			hatpos = 0.005f;
		}
		else
		{
			hatpos = 0f;
		}
		mainbody.mainTextureOffset = new Vector2(xtile + xcorrect, ytile);
		face.mainTextureOffset = new Vector2(xtile + xcorrect, ytile);
		xtile = mainbody.mainTextureOffset.x;
		ytile = mainbody.mainTextureOffset.y;
		yield return new WaitForSeconds(framespeed);
		moving = false;
		oldpos = base.transform.position;
	}
}
