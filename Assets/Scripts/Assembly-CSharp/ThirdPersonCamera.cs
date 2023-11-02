using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
	public Transform cameraTransform;

	private Transform _target;

	public float distance = 7f;

	public float height = 3f;

	public float angularSmoothLag = 0.3f;

	public float angularMaxSpeed = 15f;

	public float heightSmoothLag = 0.3f;

	public float snapSmoothLag = 0.2f;

	public float snapMaxSpeed = 720f;

	public float clampHeadPositionScreenSpace = 0.75f;

	public float lockCameraTimeout = 0.2f;

	private Vector3 headOffset = Vector3.zero;

	private Vector3 centerOffset = Vector3.zero;

	private float heightVelocity;

	private float angleVelocity;

	private bool snap;

	private ThirdPersonController controller;

	private float targetHeight = 100000f;

	private void OnEnable()
	{
		if (!cameraTransform && (bool)Camera.main)
		{
			cameraTransform = Camera.main.transform;
		}
		if (!cameraTransform)
		{
			Debug.Log("Please assign a camera to the ThirdPersonCamera script.");
			base.enabled = false;
		}
		_target = base.transform;
		if ((bool)_target)
		{
			controller = _target.GetComponent<ThirdPersonController>();
		}
		if ((bool)controller)
		{
			CharacterController characterController = (CharacterController)_target.GetComponent<Collider>();
			centerOffset = characterController.bounds.center - _target.position;
			headOffset = centerOffset;
			headOffset.y = characterController.bounds.max.y - _target.position.y;
		}
		else
		{
			Debug.Log("Please assign a target to the camera that has a ThirdPersonController script attached.");
		}
		Cut(_target, centerOffset);
	}

	private void DebugDrawStuff()
	{
		Debug.DrawLine(_target.position, _target.position + headOffset);
	}

	private float AngleDistance(float a, float b)
	{
		a = Mathf.Repeat(a, 360f);
		b = Mathf.Repeat(b, 360f);
		return Mathf.Abs(b - a);
	}

	private void Apply(Transform dummyTarget, Vector3 dummyCenter)
	{
		if (!controller)
		{
			return;
		}
		Vector3 vector = _target.position + centerOffset;
		Vector3 headPos = _target.position + headOffset;
		float y = _target.eulerAngles.y;
		float y2 = cameraTransform.eulerAngles.y;
		float num = y;
		if (Input.GetButton("Fire2"))
		{
			snap = true;
		}
		if (snap)
		{
			if (AngleDistance(y2, y) < 3f)
			{
				snap = false;
			}
			y2 = Mathf.SmoothDampAngle(y2, num, ref angleVelocity, snapSmoothLag, snapMaxSpeed);
		}
		else
		{
			if (controller.GetLockCameraTimer() < lockCameraTimeout)
			{
				num = y2;
			}
			if (AngleDistance(y2, num) > 160f && controller.IsMovingBackwards())
			{
				num += 180f;
			}
			y2 = Mathf.SmoothDampAngle(y2, num, ref angleVelocity, angularSmoothLag, angularMaxSpeed);
		}
		if (controller.IsJumping())
		{
			float num2 = vector.y + height;
			if (num2 < targetHeight || num2 - targetHeight > 5f)
			{
				targetHeight = vector.y + height;
			}
		}
		else
		{
			targetHeight = vector.y + height;
		}
		float y3 = cameraTransform.position.y;
		y3 = Mathf.SmoothDamp(y3, targetHeight, ref heightVelocity, heightSmoothLag);
		Quaternion quaternion = Quaternion.Euler(0f, y2, 0f);
		cameraTransform.position = vector;
		cameraTransform.position += quaternion * Vector3.back * distance;
		cameraTransform.position = new Vector3(cameraTransform.position.x, y3, cameraTransform.position.z);
		SetUpRotation(vector, headPos);
	}

	private void LateUpdate()
	{
		Apply(base.transform, Vector3.zero);
	}

	private void Cut(Transform dummyTarget, Vector3 dummyCenter)
	{
		float num = heightSmoothLag;
		float num2 = snapMaxSpeed;
		float num3 = snapSmoothLag;
		snapMaxSpeed = 10000f;
		snapSmoothLag = 0.001f;
		heightSmoothLag = 0.001f;
		snap = true;
		Apply(base.transform, Vector3.zero);
		heightSmoothLag = num;
		snapMaxSpeed = num2;
		snapSmoothLag = num3;
	}

	private void SetUpRotation(Vector3 centerPos, Vector3 headPos)
	{
		Vector3 position = cameraTransform.position;
		Vector3 vector = centerPos - position;
		Quaternion quaternion = Quaternion.LookRotation(new Vector3(vector.x, 0f, vector.z));
		Vector3 forward = Vector3.forward * distance + Vector3.down * height;
		cameraTransform.rotation = quaternion * Quaternion.LookRotation(forward);
		Ray ray = cameraTransform.GetComponent<Camera>().ViewportPointToRay(new Vector3(0.5f, 0.5f, 1f));
		Ray ray2 = cameraTransform.GetComponent<Camera>().ViewportPointToRay(new Vector3(0.5f, clampHeadPositionScreenSpace, 1f));
		Vector3 point = ray.GetPoint(distance);
		Vector3 point2 = ray2.GetPoint(distance);
		float num = Vector3.Angle(ray.direction, ray2.direction);
		float num2 = num / (point.y - point2.y);
		float num3 = num2 * (point.y - centerPos.y);
		if (num3 < num)
		{
			num3 = 0f;
			return;
		}
		num3 -= num;
		cameraTransform.rotation *= Quaternion.Euler(0f - num3, 0f, 0f);
	}

	private Vector3 GetCenterOffset()
	{
		return centerOffset;
	}
}
