using SimpleAI.Navigation;
using UnityEngine;

public class PathAgentComponent : MonoBehaviour, IPathAgent
{
	public PathManagerComponent m_pathManager;

	public Color m_debugPathColor = Color.yellow;

	public Color m_debugGoalColor = Color.red;

	public bool m_debugShowPath;

	private IPathRequestQuery m_query;

	private bool m_bInitialized;

	public IPathRequestQuery PathRequestQuery
	{
		get
		{
			return m_query;
		}
	}

	public PathManagerComponent PathManager
	{
		get
		{
			return m_pathManager;
		}
	}

	public bool ShowPath
	{
		get
		{
			return m_debugShowPath;
		}
		set
		{
			m_debugShowPath = value;
		}
	}

	private void Awake()
	{
		m_bInitialized = false;
		m_query = null;
		if (m_pathManager == null)
		{
			m_pathManager = GameObject.Find("PathManager").GetComponent<PathManagerComponent>();
		}
		else
		{
			m_bInitialized = true;
		}
	}

	private void OnDrawGizmos()
	{
		if (m_debugShowPath && m_bInitialized && HasActiveRequest() && PathRequestQuery.HasSuccessfullyCompleted())
		{
			Gizmos.color = m_debugPathColor;
			Vector3[] solutionPath = PathRequestQuery.GetSolutionPath(PathManager.PathTerrain);
			for (int i = 1; i < solutionPath.Length; i++)
			{
				Vector3 from = solutionPath[i - 1];
				Vector3 to = solutionPath[i];
				Gizmos.DrawLine(from, to);
			}
		}
	}

	public bool RequestPath(PathPlanParams pathPlanParams)
	{
		if (!m_bInitialized)
		{
			return false;
		}
		m_pathManager.RemoveRequest(m_query);
		m_query = m_pathManager.RequestPathPlan(pathPlanParams, this);
		if (m_query == null)
		{
			return false;
		}
		return true;
	}

	public bool HasActiveRequest()
	{
		return m_query != null;
	}

	public void CancelActiveRequest()
	{
		if (m_query != null)
		{
			m_pathManager.RemoveRequest(m_query);
			m_query = null;
		}
	}

	public Vector3 GetPathAgentFootPos()
	{
		return m_pathManager.PathTerrain.GetValidPathFloorPos(base.transform.position);
	}

	public void OnPathAgentRequestSucceeded(IPathRequestQuery request)
	{
		SendMessageUpwards("OnPathRequestSucceeded", request, SendMessageOptions.DontRequireReceiver);
	}

	public void OnPathAgentRequestFailed()
	{
		m_query = null;
		SendMessageUpwards("OnPathRequestFailed", SendMessageOptions.DontRequireReceiver);
	}
}
