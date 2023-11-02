using System.Collections;
using Photon;
using UnityEngine;

public class PlayerGraphics : Photon.MonoBehaviour
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

	public Texture2D runanimation;

	public PlayerHandler ph;

	public AudioSource footstepObj;

	public AudioClip[] footstepSounds;

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
		if ((bool)GameObject.Find("Footsteps_Grass"))
		{
			footstepObj.clip = footstepSounds[0];
		}
		if ((bool)GameObject.Find("Footsteps_Metal"))
		{
			footstepObj.clip = footstepSounds[1];
		}
		if ((bool)GameObject.Find("Footsteps_Rock"))
		{
			footstepObj.clip = footstepSounds[2];
		}
	}

	private void Start()
	{
	}

	private void Update()
	{
		base.transform.position = new Vector3(base.transform.position.x, 3f, base.transform.position.z);
		if (ph.run < 2)
		{
			mainbody.mainTexture = walkanimation;
			framespeed = walkframespeed;
		}
		else
		{
			mainbody.mainTexture = runanimation;
			framespeed = runframespeed;
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
			mainbody.mainTextureOffset = new Vector2(0f, ytile);
			face.mainTextureOffset = new Vector2(0f, ytile);
			StartCoroutine(StopFootsteps());
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

	private IEnumerator StopFootsteps()
	{
		yield return new WaitForSeconds(0.1f);
		if (oldpos == base.transform.position && footstepObj.isPlaying && footstepObj.clip != null)
		{
			footstepObj.Stop();
		}
	}

	private IEnumerator AnimateMovement()
	{
		moving = true;
		if (!footstepObj.isPlaying && footstepObj.clip != null)
		{
			footstepObj.Play();
		}
		if (ph.run > 1)
		{
			footstepObj.pitch = 1.4f;
		}
		else
		{
			footstepObj.pitch = 1f;
		}
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
		StartCoroutine(StopFootsteps());
	}
}
