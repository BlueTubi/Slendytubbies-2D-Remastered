using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CNCameraRelativeSetup : MonoBehaviour
{
	public CNJoystick joystick;

	public float runSpeed = 6f;

	private CharacterController characterController;

	private Camera mainCamera;

	private float gravity;

	private Vector3 totalMove;

	private bool tweakedLastFrame;

	private void Awake()
	{
		joystick.JoystickMovedEvent += JoystickMovedEventHandler;
		joystick.FingerLiftedEvent += StopMoving;
		characterController = GetComponent<CharacterController>();
		mainCamera = Camera.main;
		gravity = 0f - Physics.gravity.y;
		totalMove = Vector3.zero;
		tweakedLastFrame = false;
	}

	private void StopMoving()
	{
		totalMove = Vector3.zero;
	}

	private void Update()
	{
		if (!tweakedLastFrame)
		{
			totalMove = Vector3.zero;
		}
		if (!characterController.isGrounded)
		{
			totalMove.y = (Vector3.down * gravity).y;
		}
		characterController.Move(totalMove * Time.deltaTime);
		tweakedLastFrame = false;
	}

	private void JoystickMovedEventHandler(Vector3 dragVector)
	{
		dragVector.z = dragVector.y;
		dragVector.y = 0f;
		Vector3 direction = mainCamera.transform.TransformDirection(dragVector);
		direction.y = 0f;
		totalMove.x = direction.x * runSpeed;
		totalMove.z = direction.z * runSpeed;
		FaceMovementDirection(direction);
		tweakedLastFrame = true;
	}

	private void FaceMovementDirection(Vector3 direction)
	{
		if ((double)direction.sqrMagnitude > 0.1)
		{
			base.transform.forward = direction;
		}
	}
}
