using UnityEngine;

public class Rotator : MonoBehaviour
{
	private Transform myTrans;

	public int rotateSpeed = 20;

	private void Awake()
	{
		myTrans = base.transform;
	}

	private void Update()
	{
		myTrans.Rotate(new Vector3(Time.deltaTime * (float)rotateSpeed, 0f, 0f));
	}
}
