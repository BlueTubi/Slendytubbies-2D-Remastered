using System.Collections.Generic;
using SimpleAI;
using SimpleAI.Navigation;
using UnityEngine;

public class PathManagerComponent : MonoBehaviour
{
	public PathTerrainComponent m_pathTerrainComponent;

	public int m_maxNumberOfNodesPerPlanner = 100;

	public int m_maxNumberOfPlanners = 20;

	public int m_maxNumberOfCyclesPerFrame = 500;

	public int m_maxNumberOfCyclesPerPlanner = 50;

	private Pool<PathPlanner> m_pathPlannerPool;

	private Queue<PathRequest> m_requestPool;

	private LinkedList<PathRequest> m_activeRequests;

	private LinkedList<PathRequest> m_completedRequests;

	private IPathTerrain m_terrain;

	private bool m_bInitialized;

	public IPathTerrain PathTerrain
	{
		get
		{
			return m_pathTerrainComponent.PathTerrain;
		}
	}

	private void Awake()
	{
		m_bInitialized = false;
		if (m_maxNumberOfNodesPerPlanner == 0)
		{
			Debug.LogError(" 'Max Number Of Nodes Per Planner' must be set to a value greater than 0. Try 100.");
			return;
		}
		if (m_maxNumberOfPlanners == 0)
		{
			Debug.LogError(" 'Max Number Of Planners' must be set to a value greater than 0. Try 20.");
			return;
		}
		m_pathPlannerPool = new Pool<PathPlanner>(m_maxNumberOfPlanners);
		foreach (Pool<PathPlanner>.Node allNode in m_pathPlannerPool.AllNodes)
		{
			allNode.Item.Awake(m_maxNumberOfNodesPerPlanner);
		}
		m_requestPool = new Queue<PathRequest>(m_maxNumberOfPlanners);
		for (int i = 0; i < m_maxNumberOfPlanners; i++)
		{
			m_requestPool.Enqueue(new PathRequest());
		}
		m_activeRequests = new LinkedList<PathRequest>();
		m_completedRequests = new LinkedList<PathRequest>();
	}

	private void Start()
	{
		if (m_pathTerrainComponent == null)
		{
			Debug.LogError("Must give the PathManagerComponent a reference to the PathTerrainComponent. You can do this through the Unity Editor.");
			return;
		}
		m_terrain = m_pathTerrainComponent.PathTerrain;
		if (m_terrain == null)
		{
			Debug.LogError("PathTerrain is NULL in PathTerrainComponent. Make sure you have instantiated a path terrain inside the Awake functionof your PathTerrainComponent");
			return;
		}
		foreach (Pool<PathPlanner>.Node allNode in m_pathPlannerPool.AllNodes)
		{
			allNode.Item.Start(m_terrain);
		}
		m_bInitialized = true;
	}

	public void Update()
	{
		if (!m_bInitialized)
		{
			Debug.LogError("PathManagerComponent failed to initialized. Can't call Update.");
			return;
		}
		UpdateActiveRequests(Time.deltaTime);
		UpdateCompletedRequests(Time.deltaTime);
		int num = 0;
		LinkedList<PathRequest> linkedList = new LinkedList<PathRequest>();
		foreach (PathRequest activeRequest in m_activeRequests)
		{
			PathPlanner item = activeRequest.PathPlanner.Item;
			int num2 = item.Update(m_maxNumberOfCyclesPerPlanner);
			num += num2;
			if (item.HasPlanFailed())
			{
				OnRequestCompleted(activeRequest, false);
				linkedList.AddFirst(activeRequest);
			}
			else if (item.HasPlanSucceeded())
			{
				OnRequestCompleted(activeRequest, true);
				linkedList.AddFirst(activeRequest);
			}
			if (num >= m_maxNumberOfCyclesPerFrame)
			{
				break;
			}
		}
		foreach (PathRequest item2 in linkedList)
		{
			m_activeRequests.Remove(item2);
			if (item2.HasFailed())
			{
				RemoveRequest(item2);
			}
		}
	}

	public IPathRequestQuery RequestPathPlan(PathPlanParams pathPlanParams, IPathAgent agent)
	{
		if (!m_bInitialized)
		{
			Debug.LogError("PathManagerComponent isn't yet fully intialized. Wait until Start() has been called. Can't call RequestPathPlan.");
			return null;
		}
		if (m_requestPool.Count == 0)
		{
			Debug.Log("RequestPathPlan failed because it is already servicing the maximum number of requests: " + m_maxNumberOfPlanners);
			return null;
		}
		if (m_pathPlannerPool.AvailableCount == 0)
		{
			Debug.Log("RequestPathPlan failed because it is already servicing the maximum number of path requests: " + m_maxNumberOfPlanners);
			return null;
		}
		pathPlanParams.UpdateStartAndGoalPos(m_terrain.GetValidPathFloorPos(pathPlanParams.StartPos));
		if (m_activeRequests.Count > 0)
		{
			foreach (PathRequest activeRequest in m_activeRequests)
			{
				if (activeRequest.Agent.GetHashCode() == agent.GetHashCode())
				{
					return null;
				}
			}
		}
		if (m_activeRequests.Count > 0)
		{
			foreach (PathRequest completedRequest in m_completedRequests)
			{
				if (completedRequest.Agent.GetHashCode() == agent.GetHashCode())
				{
					return null;
				}
			}
		}
		Pool<PathPlanner>.Node pathPlanner = m_pathPlannerPool.Get();
		PathRequest pathRequest = m_requestPool.Dequeue();
		pathRequest.Set(pathPlanParams, pathPlanner, agent);
		m_activeRequests.AddFirst(pathRequest);
		int pathNodeIndex = m_terrain.GetPathNodeIndex(pathPlanParams.StartPos);
		int pathNodeIndex2 = m_terrain.GetPathNodeIndex(pathPlanParams.GoalPos);
		pathPlanner.Item.StartANewPlan(pathNodeIndex, pathNodeIndex2);
		return pathRequest;
	}

	public void RemoveRequest(IPathRequestQuery requestQuery)
	{
		if (!m_bInitialized)
		{
			Debug.LogError("PathManagerComponent isn't yet fully intialized. Wait until Start() has been called. Can't call ConsumeRequest.");
		}
		else if (requestQuery != null)
		{
			PathRequest pathRequest = GetPathRequest(requestQuery);
			if (pathRequest != null)
			{
				m_completedRequests.Remove(pathRequest);
				m_activeRequests.Remove(pathRequest);
				m_pathPlannerPool.Return(pathRequest.PathPlanner);
				m_requestPool.Enqueue(pathRequest);
			}
		}
	}

	private void OnRequestCompleted(PathRequest request, bool bSucceeded)
	{
		if (bSucceeded)
		{
			OnRequestSucceeded(request);
		}
		else
		{
			OnRequestFailed(request);
		}
	}

	private void OnRequestFailed(PathRequest request)
	{
		request.Agent.OnPathAgentRequestFailed();
	}

	private void OnRequestSucceeded(PathRequest request)
	{
		m_completedRequests.AddFirst(request);
		request.Agent.OnPathAgentRequestSucceeded(request);
	}

	private void UpdateActiveRequests(float deltaTimeInSeconds)
	{
		foreach (PathRequest activeRequest in m_activeRequests)
		{
			activeRequest.Update(deltaTimeInSeconds);
		}
	}

	private void UpdateCompletedRequests(float deltaTimeInSeconds)
	{
		LinkedList<PathRequest> linkedList = new LinkedList<PathRequest>();
		foreach (PathRequest completedRequest in m_completedRequests)
		{
			completedRequest.Update(deltaTimeInSeconds);
			if (completedRequest.IsReadyToReplan())
			{
				completedRequest.PlanParams.UpdateStartAndGoalPos(completedRequest.Agent.GetPathAgentFootPos());
				completedRequest.RestartReplanTimer();
				linkedList.AddFirst(completedRequest);
				m_activeRequests.AddFirst(completedRequest);
				int pathNodeIndex = m_terrain.GetPathNodeIndex(completedRequest.PlanParams.StartPos);
				int pathNodeIndex2 = m_terrain.GetPathNodeIndex(completedRequest.PlanParams.GoalPos);
				completedRequest.PathPlanner.Item.StartANewPlan(pathNodeIndex, pathNodeIndex2);
			}
		}
		foreach (PathRequest item in linkedList)
		{
			m_completedRequests.Remove(item);
		}
	}

	private PathRequest GetPathRequest(IPathRequestQuery requestQuery)
	{
		PathRequest result = null;
		if (requestQuery is PathRequest)
		{
			result = requestQuery as PathRequest;
		}
		return result;
	}
}
