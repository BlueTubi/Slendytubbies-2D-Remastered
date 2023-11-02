using UnityEngine;

public class RandomObstacleSpawner : MonoBehaviour
{
	public PathGridComponent m_pathGridComponent;

	public GameObject m_obstacle;

	public float m_spawnInterval = 2f;

	public int m_spawnCount = 10;

	private float m_timeBeforeNextSpawn;

	private int m_numObstaclesSpawned;

	private void Awake()
	{
		m_timeBeforeNextSpawn = m_spawnInterval;
		m_numObstaclesSpawned = 0;
	}

	private void Start()
	{
	}

	private void Update()
	{
		if (m_numObstaclesSpawned >= m_spawnCount)
		{
			return;
		}
		m_timeBeforeNextSpawn -= Time.deltaTime;
		if (m_timeBeforeNextSpawn <= 0f)
		{
			Vector3 vector = ChooseRandomSpawnPosition();
			if (vector != Vector3.zero)
			{
				Object.Instantiate(m_obstacle, vector, Quaternion.identity);
				m_timeBeforeNextSpawn = m_spawnInterval;
				m_numObstaclesSpawned++;
			}
			else
			{
				m_numObstaclesSpawned = m_spawnCount;
			}
		}
	}

	private Vector3 ChooseRandomSpawnPosition()
	{
		PatrolNodeComponent[] array = (PatrolNodeComponent[])Object.FindObjectsOfType(typeof(PatrolNodeComponent));
		PathAgentComponent[] array2 = (PathAgentComponent[])Object.FindObjectsOfType(typeof(PathAgentComponent));
		Vector3 result = Vector3.zero;
		for (int i = 0; i < 100; i++)
		{
			int num = Random.Range(0, m_pathGridComponent.PathGrid.NumberOfCells - 1);
			if (m_pathGridComponent.PathGrid.IsBlocked(num))
			{
				continue;
			}
			bool flag = false;
			for (int j = 0; j < array.Length; j++)
			{
				Vector3 position = array[j].transform.position;
				int cellIndex = m_pathGridComponent.PathGrid.GetCellIndex(position);
				if (cellIndex == num)
				{
					flag = true;
					break;
				}
			}
			if (flag)
			{
				continue;
			}
			for (int k = 0; k < array2.Length; k++)
			{
				Vector3 position2 = array2[k].transform.position;
				int cellIndex2 = m_pathGridComponent.PathGrid.GetCellIndex(position2);
				if (cellIndex2 == num)
				{
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				result = m_pathGridComponent.PathGrid.GetCellCenter(num);
				break;
			}
		}
		return result;
	}
}
