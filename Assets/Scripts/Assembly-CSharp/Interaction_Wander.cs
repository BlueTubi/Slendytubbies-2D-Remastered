using SimpleAI.Navigation;
using UnityEngine;

[RequireComponent(typeof(NavigationAgentComponent))]
public class Interaction_Wander : MonoBehaviour
{
	public float m_replanInterval = 0.5f;

	private NavigationAgentComponent m_navigationAgent;

	private bool m_bNavRequestCompleted;

	private PathGrid m_pathGrid;

	private void Awake()
	{
		m_bNavRequestCompleted = true;
		m_navigationAgent = GetComponent<NavigationAgentComponent>();
	}

	private void Start()
	{
		if (m_navigationAgent.PathTerrain == null || !(m_navigationAgent.PathTerrain is PathGrid))
		{
			Debug.LogError("Interaction_Wander was built to work with a PathGrid terrain; can't use it on other terrain types.");
		}
		else
		{
			m_pathGrid = m_navigationAgent.PathTerrain as PathGrid;
		}
	}

	private void Update()
	{
		if (m_bNavRequestCompleted)
		{
			Vector3 targetPosition = ChooseRandomDestination();
			if (m_navigationAgent.MoveToPosition(targetPosition, m_replanInterval))
			{
				m_bNavRequestCompleted = false;
			}
		}
	}

	private Vector3 ChooseRandomDestination()
	{
		int index = Random.Range(0, m_pathGrid.NumberOfCells - 1);
		return m_pathGrid.GetCellCenter(index);
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
