using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(CNCameraRelativeSetup))]
public class SkeletonAnimator : MonoBehaviour
{
	private const string IDLE = "Idle";

	private const string WALK = "Walk";

	private const string RUN = "Run";

	private const string ATTACK = "Attack";

	private const string ATTACK_1 = "Attack1";

	private const float WALK_SPEED_MULTIPLIER = 0.6f;

	private const float RUN_SPEED_MULTIPLIER = 2f;

	private CharacterController charController;

	private CNCameraRelativeSetup cameraRelativeSetup;

	private CNJoystick joystick;

	private void Awake()
	{
		charController = GetComponent<CharacterController>();
		cameraRelativeSetup = GetComponent<CNCameraRelativeSetup>();
		joystick = cameraRelativeSetup.joystick;
		joystick.JoystickMovedEvent += AnimateMovement;
		joystick.FingerLiftedEvent += StoppedMoving;
	}

	private void Update()
	{
		Debug.Log(charController.velocity);
	}

	private void AnimateMovement(Vector3 relativeMovement)
	{
		float sqrMagnitude = relativeMovement.sqrMagnitude;
		if (sqrMagnitude > 0f)
		{
			if (sqrMagnitude >= 0.3f)
			{
				GetComponent<Animation>()["Walk"].speed = charController.velocity.magnitude / 2f;
				GetComponent<Animation>().CrossFade("Run");
			}
			else
			{
				GetComponent<Animation>()["Walk"].speed = charController.velocity.magnitude / 0.6f;
				GetComponent<Animation>().CrossFade("Walk");
			}
		}
	}

	private void StoppedMoving()
	{
		GetComponent<Animation>().CrossFade("Idle");
	}
}
