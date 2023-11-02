using UnityEngine;

[RequireComponent(typeof(NavigationAgentComponent))]
public class Interaction_Patrol : MonoBehaviour
{
	public GameObject[] m_patrolNodes;

	public float m_replanInterval = float.MaxValue;

	private NavigationAgentComponent m_navigationAgent;

	private bool m_bNavRequestCompleted;

	private int m_currentPatrolNodeGoalIndex;

	private void Awake()
	{
		m_currentPatrolNodeGoalIndex = 0;
		m_bNavRequestCompleted = true;
		m_navigationAgent = GetComponent<NavigationAgentComponent>();
	}

	private void Update()
	{
		if (m_patrolNodes == null || m_patrolNodes.Length == 0)
		{
			Debug.LogError("No patrol nodes are set");
		}
		else if (m_bNavRequestCompleted)
		{
			m_currentPatrolNodeGoalIndex = (m_currentPatrolNodeGoalIndex + 1) % m_patrolNodes.Length;
			Vector3 patrolNodePosition = GetPatrolNodePosition(m_currentPatrolNodeGoalIndex);
			m_navigationAgent.MoveToPosition(patrolNodePosition, m_replanInterval);
			m_bNavRequestCompleted = false;
		}
	}

	private Vector3 GetPatrolNodePosition(int index)
	{
		if (m_patrolNodes == null || m_patrolNodes.Length == 0)
		{
			Debug.LogError("No patrol nodes are set");
			return base.transform.position;
		}
		if (index < 0 || index >= m_patrolNodes.Length)
		{
			Debug.LogError("PatrolNode index out of bounds");
			return base.transform.position;
		}
		return m_patrolNodes[index].transform.position;
	}

	private void OnNavigationRequestSucceeded()
	{
		m_bNavRequestCompleted = true;
	}

	private void OnNavigationRequestFailed()
	{
		m_bNavRequestCompleted = true;
	}
}
