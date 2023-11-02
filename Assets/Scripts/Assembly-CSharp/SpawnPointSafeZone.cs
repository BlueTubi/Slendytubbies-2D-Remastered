using UnityEngine;

public class SpawnPointSafeZone : MonoBehaviour
{
	public Transform spawnpoint;

	private void Start()
	{
		spawnpoint = GameObject.FindWithTag("MonsterStartPoint").transform;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.transform.tag == "Monster")
		{
			other.gameObject.transform.position = spawnpoint.position;
		}
	}
}
