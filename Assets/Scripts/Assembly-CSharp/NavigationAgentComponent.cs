using SimpleAI.Navigation;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(SteeringAgentComponent))]
[RequireComponent(typeof(PathAgentComponent))]
public class NavigationAgentComponent : MonoBehaviour
{
	public bool m_usePathSmoothing = true;

	private PathAgentComponent m_pathAgent;

	private SteeringAgentComponent m_steeringAgent;

	public IPathTerrain PathTerrain
	{
		get
		{
			return m_pathAgent.PathManager.PathTerrain;
		}
	}

	private void Awake()
	{
		m_pathAgent = GetComponent<PathAgentComponent>();
		m_steeringAgent = GetComponent<SteeringAgentComponent>();
	}

	public bool MoveToPosition(Vector3 targetPosition, float replanInterval)
	{
		NavTargetPos target = new NavTargetPos(targetPosition, PathTerrain);
		PathPlanParams pathPlanParams = new PathPlanParams(base.transform.position, target, replanInterval);
		return m_pathAgent.RequestPath(pathPlanParams);
	}

	public bool MoveToGameObject(GameObject gameObject, float replanInterval)
	{
		NavTargetGameObject target = new NavTargetGameObject(gameObject, PathTerrain);
		PathPlanParams pathPlanParams = new PathPlanParams(base.transform.position, target, replanInterval);
		return m_pathAgent.RequestPath(pathPlanParams);
	}

	public void CancelActiveRequest()
	{
		m_steeringAgent.StopSteering();
		m_pathAgent.CancelActiveRequest();
	}

	private void OnPathRequestSucceeded(IPathRequestQuery request)
	{
		Vector3[] solutionPath = request.GetSolutionPath(m_pathAgent.PathManager.PathTerrain);
		Vector3[] path = solutionPath;
		if (m_usePathSmoothing)
		{
			Vector3[] aPortalLeftEndPts;
			Vector3[] aPortalRightEndPts;
			m_pathAgent.PathManager.PathTerrain.ComputePortalsForPathSmoothing(solutionPath, out aPortalLeftEndPts, out aPortalRightEndPts);
			path = PathSmoother.Smooth(solutionPath, request.GetStartPos(), request.GetGoalPos(), aPortalLeftEndPts, aPortalRightEndPts);
		}
		m_steeringAgent.SteerAlongPath(path, m_pathAgent.PathManager.PathTerrain);
	}

	private void OnPathRequestFailed()
	{
		m_steeringAgent.StopSteering();
		SendMessageUpwards("OnNavigationRequestFailed", SendMessageOptions.DontRequireReceiver);
	}

	private void OnSteeringRequestSucceeded()
	{
		SendMessageUpwards("OnNavigationRequestSucceeded", SendMessageOptions.DontRequireReceiver);
	}
}
