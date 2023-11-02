using UnityEngine;

[RequireComponent(typeof(NavigationAgentComponent))]
public class Interaction_Chase : MonoBehaviour
{
	public GameObject m_chaseObject;

	public float m_replanInterval = 0.5f;

	private NavigationAgentComponent m_navigationAgent;

	private bool m_bNavRequestCompleted;

	private void Awake()
	{
		m_bNavRequestCompleted = true;
		m_navigationAgent = GetComponent<NavigationAgentComponent>();
	}

	private void Update()
	{
		if (m_bNavRequestCompleted && m_navigationAgent.MoveToGameObject(m_chaseObject, m_replanInterval))
		{
			m_bNavRequestCompleted = false;
		}
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
