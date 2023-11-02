using UnityEngine;

public class AIDetect : MonoBehaviour
{
	public Transform target;

	public bool CanFollowTarget;

	public Transform flashlight;

	private void Awake()
	{
		target = GameObject.FindWithTag("Monster").transform;
	}

	private void Update()
	{
		if ((bool)target)
		{
			Vector3 forward = flashlight.forward;
			Vector3 from = target.position - base.transform.position;
			float num = Vector3.Angle(from, forward);
			if (num < 45f)
			{
				CanFollowTarget = false;
			}
			else
			{
				CanFollowTarget = true;
			}
		}
	}
}
