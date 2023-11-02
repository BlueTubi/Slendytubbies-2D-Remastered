using UnityEngine;

public class FindPlayers : MonoBehaviour
{
	public string searchTag = "Player";

	public GameObject[] taggedGameObjects;

	public float curdis;

	public float newdis;

	private bool hasspotted;

	public float scanFrequency = 1f;

	public Transform target;

	public Transform rotateobject;

	private void Start()
	{
		InvokeRepeating("ScanForTarget", 0f, scanFrequency);
	}

	private void Update()
	{
		if (target == null)
		{
			ScanForTarget();
		}
		else if (rotateobject != null)
		{
			rotateobject.LookAt(target);
		}
	}

	private void ScanForTarget()
	{
		target = GetNearestTaggedObject().transform;
	}

	private GameObject GetNearestTaggedObject()
	{
		float num = float.PositiveInfinity;
		taggedGameObjects = GameObject.FindGameObjectsWithTag(searchTag);
		GameObject result = null;
		GameObject[] array = taggedGameObjects;
		foreach (GameObject gameObject in array)
		{
			Vector3 position = gameObject.transform.position;
			float sqrMagnitude = (position - base.transform.position).sqrMagnitude;
			if (sqrMagnitude < num)
			{
				result = gameObject;
				num = sqrMagnitude;
			}
		}
		return result;
	}
}
