using System;
using UnityEngine;

public class CNCameraFollow : MonoBehaviour
{
	private const float minDistance = 2f;

	private const float maxDistance = 10f;

	public Transform targetObject;

	public CNJoystick joystick;

	[Range(1f, 15f)]
	public float cameraDistance = 3f;

	[Range(1f, 100f)]
	public float rotateSpeed = 100f;

	[Range(1f, 5f)]
	public float distanceSpeed = 1f;

	[Range(0f, 360f)]
	public float cameraYAngle = 270f;

	private void Start()
	{
		if (targetObject == null)
		{
			throw new UnassignedReferenceException("Please, specify player target to follow");
		}
		if (joystick != null)
		{
			joystick.JoystickMovedEvent += ChangeAngle;
		}
	}

	private void LateUpdate()
	{
		SimpleCamera();
		base.transform.LookAt(targetObject);
	}

	private void ChangeAngle(Vector3 relativePosition)
	{
		cameraYAngle -= relativePosition.x * rotateSpeed * Time.deltaTime;
		if ((cameraDistance < 2f && relativePosition.y < 0f) || (cameraDistance > 10f && relativePosition.y > 0f) || (cameraDistance >= 2f && cameraDistance <= 10f))
		{
			cameraDistance -= relativePosition.y * distanceSpeed * Time.deltaTime;
		}
	}

	private void SimpleCamera()
	{
		Vector3 position = targetObject.position;
		position.x = targetObject.position.x + cameraDistance * Mathf.Sin(cameraYAngle * ((float)Math.PI / 180f));
		position.z = targetObject.position.z + cameraDistance * Mathf.Cos(cameraYAngle * ((float)Math.PI / 180f));
		position.y = targetObject.position.y + cameraDistance;
		base.transform.position = position;
	}
}
