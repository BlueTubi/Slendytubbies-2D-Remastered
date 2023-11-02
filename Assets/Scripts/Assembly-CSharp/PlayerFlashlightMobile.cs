using UnityEngine;

public class PlayerFlashlightMobile : MonoBehaviour
{
	public CNJoystick joystick;

	public Transform flashlight;

	public Transform tempvalues;

	private void Awake()
	{
		joystick.JoystickMovedEvent += JoystickMovedEventHandler;
	}

	private void Update()
	{
		flashlight.LookAt(tempvalues);
	}

	private void JoystickMovedEventHandler(Vector3 dragVector)
	{
		dragVector.z = dragVector.y;
		dragVector.y = -2f;
		tempvalues.localPosition = dragVector;
		if (base.gameObject.tag == "Monster")
		{
			Vector3 position = base.transform.position;
			position.y = GameObject.FindWithTag("PlayerStartPoint").transform.position.y;
			base.transform.position = position;
		}
	}

	private void FaceMovementDirection(Vector3 direction)
	{
		if ((double)direction.sqrMagnitude > 0.1)
		{
			flashlight.forward = direction;
		}
	}
}
