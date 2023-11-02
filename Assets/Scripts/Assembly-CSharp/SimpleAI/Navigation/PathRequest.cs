using System;
using System.Collections.Generic;
using SimpleAI.Planning;
using UnityEngine;

namespace SimpleAI.Navigation
{
	public class PathRequest : IComparable<PathRequest>, IPathRequestQuery
	{
		private int m_priority;

		private PathPlanParams m_pathPlanParams;

		private Pool<PathPlanner>.Node m_pathPlanner;

		private float m_replanTimeRemaining;

		private IPathAgent m_agent;

		public int Priority
		{
			get
			{
				return m_priority;
			}
		}

		public Pool<PathPlanner>.Node PathPlanner
		{
			get
			{
				return m_pathPlanner;
			}
		}

		public PathPlanParams PlanParams
		{
			get
			{
				return m_pathPlanParams;
			}
		}

		public IPathAgent Agent
		{
			get
			{
				return m_agent;
			}
		}

		public PathRequest()
		{
			m_priority = 0;
			m_replanTimeRemaining = 0f;
		}

		public void Set(PathPlanParams planParams, Pool<PathPlanner>.Node pathPlanner, IPathAgent agent)
		{
			m_pathPlanParams = planParams;
			m_pathPlanner = pathPlanner;
			m_replanTimeRemaining = planParams.ReplanInterval;
			m_agent = agent;
		}

		public void Update(float deltaTimeInSeconds)
		{
			m_replanTimeRemaining -= Convert.ToSingle(deltaTimeInSeconds);
			m_replanTimeRemaining = Math.Max(m_replanTimeRemaining, 0f);
		}

		public bool IsReadyToReplan()
		{
			return m_replanTimeRemaining <= 0f;
		}

		public void RestartReplanTimer()
		{
			m_replanTimeRemaining = m_pathPlanParams.ReplanInterval;
		}

		public int CompareTo(PathRequest other)
		{
			if (m_priority > other.Priority)
			{
				return -1;
			}
			if (m_priority < other.Priority)
			{
				return 1;
			}
			return 0;
		}

		public LinkedList<Node> GetSolutionPath()
		{
			return m_pathPlanner.Item.Solution;
		}

		public Vector3[] GetSolutionPath(IPathTerrain terrain)
		{
			if (!HasSuccessfullyCompleted())
			{
				return null;
			}
			LinkedList<Node> solutionPath = GetSolutionPath();
			if (solutionPath == null || solutionPath.Count == 0)
			{
				return null;
			}
			Vector3[] array = new Vector3[solutionPath.Count];
			int num = 0;
			foreach (Node item in solutionPath)
			{
				Vector3 pathNodePos = terrain.GetPathNodePos(item.Index);
				array[num] = pathNodePos;
				num++;
			}
			array[0] = GetStartPos();
			int num2 = Mathf.Clamp(num, 0, solutionPath.Count - 1);
			array[num2] = GetGoalPos();
			return array;
		}

		public Vector3 GetStartPos()
		{
			return m_pathPlanParams.StartPos;
		}

		public Vector3 GetGoalPos()
		{
			return m_pathPlanParams.GoalPos;
		}

		public IPathAgent GetPathAgent()
		{
			return Agent;
		}

		public bool HasCompleted()
		{
			return m_pathPlanner.Item.HasPlanCompleted();
		}

		public bool HasSuccessfullyCompleted()
		{
			return m_pathPlanner.Item.HasPlanSucceeded();
		}

		public bool HasFailed()
		{
			return m_pathPlanner.Item.HasPlanFailed();
		}
	}
}
