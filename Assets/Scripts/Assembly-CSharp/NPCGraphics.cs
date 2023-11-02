using System.Collections;
using UnityEngine;

public class NPCGraphics : MonoBehaviour
{
	public Transform model;

	public float walkframespeed = 0.2f;

	public float xamount = 4f;

	public float yamount = 4f;

	public bool waiting1;

	public bool moving;

	public Transform rotateobject;

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

	private void Update()
	{
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
		if (rotateobject == null)
		{
			pos = base.transform.position;
			if (oldpos.x > pos.x)
			{
				ytile = 1f / yamount + ycorrect;
			}
			if (oldpos.x < pos.x)
			{
				ytile = 1f / yamount;
			}
			if (oldpos.z + 0.01f < pos.z)
			{
				ytile = 1f / yamount - ycorrect;
			}
			if (oldpos.z - 0.01f > pos.z)
			{
				ytile = 1f / yamount + ycorrect + ycorrect;
			}
		}
		else
		{
			Vector3 localEulerAngles = rotateobject.localEulerAngles;
			if (localEulerAngles.y > 150f && localEulerAngles.y < 240f)
			{
				ytile = 1f / yamount + ycorrect + ycorrect;
			}
			if (localEulerAngles.y > 240f && localEulerAngles.y < 330f)
			{
				ytile = 1f / yamount + ycorrect;
			}
			if (localEulerAngles.y < 60f && localEulerAngles.y > -30f)
			{
				ytile = 1f / yamount - ycorrect;
			}
			if (localEulerAngles.y > 60f && localEulerAngles.y < 150f)
			{
				ytile = 1f / yamount;
			}
		}
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
		oldpos = base.transform.position;
		moving = false;
	}
}
