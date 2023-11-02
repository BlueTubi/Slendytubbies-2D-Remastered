using UnityEngine;

public class PlayerMovementMobile : MonoBehaviour
{
	public CNJoystick joystick;

	public float walkspeed = 6f;

	public float speed = 6f;

	public float runspeed = 12f;

	private CharacterController characterController;

	private Camera mainCamera;

	private float gravity;

	private Vector3 totalMove;

	public bool isrunning;

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

	private void Start()
	{
	}

	private void Update()
	{
		if (!tweakedLastFrame)
		{
			totalMove = Vector3.zero;
		}
		if (!characterController.isGrounded)
		{
		}
		characterController.Move(totalMove * Time.deltaTime);
		tweakedLastFrame = false;
	}

	private void JoystickMovedEventHandler(Vector3 dragVector)
	{
		dragVector.z = dragVector.y;
		dragVector.y = 0f;
		totalMove.x = dragVector.x * speed;
		totalMove.z = dragVector.z * speed;
		tweakedLastFrame = true;
	}

	private void OnGUI()
	{
		if (!isrunning)
		{
			speed = walkspeed;
			if (GUI.Button(new Rect(Screen.width - 140, 80f, 140f, 70f), "Run Mode: Off"))
			{
				isrunning = true;
			}
		}
		else
		{
			speed = runspeed;
			if (GUI.Button(new Rect(Screen.width - 140, 80f, 140f, 70f), "Run Mode: On"))
			{
				isrunning = false;
			}
		}
	}
}
