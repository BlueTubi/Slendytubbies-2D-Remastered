using UnityEngine;

public class CN2DController : MonoBehaviour
{
	public CNJoystick movementJoystick;

	private Transform transformCache;

	private void Awake()
	{
		if (movementJoystick == null)
		{
			throw new UnassignedReferenceException("Please specify movement Joystick object");
		}
		movementJoystick.FingerTouchedEvent += StartMoving;
		movementJoystick.FingerLiftedEvent += StopMoving;
		movementJoystick.JoystickMovedEvent += Move;
		transformCache = base.transform;
	}

	protected virtual void Move(Vector3 relativeVector)
	{
		transformCache.position += relativeVector;
		FaceMovementDirection(relativeVector);
	}

	private void FaceMovementDirection(Vector3 direction)
	{
		if ((double)direction.sqrMagnitude > 0.1)
		{
			base.transform.up = direction;
		}
	}

	protected virtual void StopMoving()
	{
	}

	protected virtual void StartMoving()
	{
	}
}
